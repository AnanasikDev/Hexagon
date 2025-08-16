#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using StateID = System.Int32;

namespace Hexagon.StateMachine
{
    public class Transition
    {
        public StateMachine _machine = null!;

        public StateID _from;
        public StateID _to;
        public ConditionDelegate _condition;
        public float _duration = 0;

        public event Action? onFinishedEvent = null;

        public State _From { get { return _machine._enum2state[_from]; } }
        public State _To { get { return _machine._enum2state[_to]; } }

        public delegate bool ConditionDelegate(State state, ExternalMachineEvent? @event);

        public void Init(StateMachine machine)
        {
            _machine = machine;
        }

        public Transition(StateID from, StateID to, ConditionDelegate? specificCondition = null, float duration = 0)
        {
            this._from = from;
            this._to = to;
            if (specificCondition == null)
            {
                specificCondition = (State state, ExternalMachineEvent? @event) => true;
            }
            _condition = specificCondition;
            this._duration = duration;
        }

        public virtual async Task<bool> Progress()
        {
            if (_duration <= 0)
            {
                return true;
            }
            await Task.Delay(TimeSpan.FromSeconds(_duration));
            return true;
        }

        public virtual void Begin()
        {
            _From.Weight = 1;
            _To.Weight = 0;
        }

        public virtual void Finish()
        {
            _From.Weight = 0;
            _To.Weight = 1;
            onFinishedEvent?.Invoke();
        }
    }

    public class Transition<TStateEnum> : Transition where TStateEnum : Enum
    {
        public Transition(TStateEnum from, TStateEnum to, ConditionDelegate? specificCondition = null, float delay = 0) : base(StateMachine.EnumToID(from), StateMachine.EnumToID(to), specificCondition, delay)
        {
        }

        public static TransitionFactory<TStateEnum> From(params TStateEnum[] froms)
        {
            var data = new TransitionFactory<TStateEnum>
            {
                froms = new List<TStateEnum>(froms)
            };
            return data;
        }
    }

    public sealed class TransitionFactory<TStateEnum> : 
        AbstractTransitionFactory<TransitionFactory<TStateEnum>, TStateEnum> 
            where TStateEnum : Enum
    {
        private float duration;

        public TransitionFactory<TStateEnum> Durate(float seconds)
        {
            duration = seconds;
            return this;
        }

        public override TransitionGroup If(Transition.ConditionDelegate? condition = null)
        {
            List<Transition> transitions = new List<Transition>();
            foreach (TStateEnum from in this.froms)
            {
                foreach (TStateEnum to in this.tos)
                {
                    transitions.Add(new Transition(
                        from: StateMachine.EnumToID(from),
                        to: StateMachine.EnumToID(to),
                        specificCondition: condition,
                        duration: this.duration
                    ));
                }
            }
            TransitionGroup result = new TransitionGroup(many: transitions);
            return result;
        }
    }
}
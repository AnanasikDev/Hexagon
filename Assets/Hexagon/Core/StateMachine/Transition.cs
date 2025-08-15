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

        public delegate bool ConditionDelegate(State state, FSM_Event @event);

        public static TransitionInput CreateOne<TStateEnum>(TStateEnum from, TStateEnum to, ConditionDelegate? specificCondition = null, float duration = 0) where TStateEnum : Enum
        {
            return CreateOne(StateMachine.EnumToID(from), StateMachine.EnumToID(to), specificCondition, duration);
        }

        internal static TransitionInput CreateOne(StateID from, StateID to, ConditionDelegate? specificCondition = null, float duration = 0)
        {
            Transition transition = new Transition(
                from: from,
                to: to,
                specificCondition: specificCondition,
                duration: duration
            );
            TransitionInput result = new TransitionInput(one: transition);
            return result;
        }

        public static TransitionInput CreateMany<TStateEnum>(IEnumerable<TStateEnum> froms, IEnumerable<TStateEnum> tos, ConditionDelegate? specificCondition = null, float duration = 0) where TStateEnum : Enum
        {
            List<Transition> transitions = new List<Transition>();
            foreach (TStateEnum from in froms)
            {
                foreach (TStateEnum to in tos)
                {
                    transitions.AddRange(CreateOne(from, to, specificCondition, duration)._transitions);
                }
            }
            TransitionInput result = new TransitionInput(many: transitions);
            return result;
        }

        public static TransitionInput CreateMany(IEnumerable<StateID> froms, IEnumerable<StateID> tos, ConditionDelegate? specificCondition = null, float duration = 0)
        {
            List<Transition> transitions = new List<Transition>();
            foreach (StateID from in froms)
            {
                foreach (StateID to in tos)
                {
                    transitions.AddRange(CreateOne(from, to, specificCondition, duration)._transitions);
                }
            }
            TransitionInput result = new TransitionInput(many: transitions);
            return result;
        }

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
                specificCondition = (State state, FSM_Event @event) => true;
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

        public struct Data<TStateEnum> where TStateEnum : Enum
        {
            internal List<TStateEnum> froms;
            internal List<TStateEnum> tos;

            public Data<TStateEnum> To(params TStateEnum[] tos)
            {
                this.tos = new List<TStateEnum>(tos);
                return this;
            }

            public TransitionInput If(ConditionDelegate? condition = null, float duration = 0)
            {
                return Transition.CreateMany(
                    froms: this.froms,
                    tos: this.tos,
                    specificCondition: condition,
                    duration: duration
                );
            }
        }
    }

    public readonly struct TransitionInput
    {
        public readonly List<Transition> _transitions;

        public TransitionInput(List<Transition> many)
        {
            _transitions = many;
        }

        public TransitionInput(Transition one)
        {
            _transitions = new List<Transition> { one };
        }
    }

    public class Transition<TStateEnum> : Transition where TStateEnum : Enum
    {
        public Transition(int from, int to, ConditionDelegate? specificCondition = null, float delay = 0) : base(from, to, specificCondition, delay)
        {
        }

        public static Data<TStateEnum> From(params TStateEnum[] froms)
        {
            var data = new Data<TStateEnum>
            {
                froms = new List<TStateEnum>(froms)
            };
            return data;
        }
    }

    public class BlendTransition : Transition
    {
        public delegate float BlendingDelegate(float time);

        public BlendingDelegate _blendingFunction;

        protected BlendingDelegate _linearProgressFunction;

        protected float _startTime = 0;

        public static TransitionInput CreateOne<TStateEnum>(TStateEnum from, TStateEnum to, ConditionDelegate? specificCondition = null, float duration = 0, BlendingDelegate? blendingFunction = null) where TStateEnum : Enum
        {
            return CreateOne(StateMachine.EnumToID(from), StateMachine.EnumToID(to), specificCondition, duration, blendingFunction);
        }

        internal static TransitionInput CreateOne(StateID from, StateID to, ConditionDelegate? specificCondition = null, float duration = 0, BlendingDelegate? blendingFunction = null)
        {
            BlendTransition transition = new BlendTransition(
                from: from,
                to: to,
                specificCondition: specificCondition,
                duration: duration,
                blendingFunction: blendingFunction
            );
            TransitionInput result = new TransitionInput(one: transition);
            return result;
        }

        public static TransitionInput CreateMany<TStateEnum>(IEnumerable<TStateEnum> froms, IEnumerable<TStateEnum> tos, ConditionDelegate? specificCondition = null, float duration = 0, BlendingDelegate? blendingFunction = null) where TStateEnum : Enum
        {
            List<Transition> transitions = new List<Transition>();
            foreach (TStateEnum from in froms)
            {
                foreach (TStateEnum to in tos)
                {
                    transitions.AddRange(CreateOne(from, to, specificCondition, duration, blendingFunction)._transitions);
                }
            }
            TransitionInput result = new TransitionInput(many: transitions);
            return result;
        }

        public static TransitionInput CreateMany(IEnumerable<StateID> froms, IEnumerable<StateID> tos, ConditionDelegate? specificCondition = null, float duration = 0, BlendingDelegate? blendingFunction = null)
        {
            List<Transition> transitions = new List<Transition>();
            foreach (StateID from in froms)
            {
                foreach (StateID to in tos)
                {
                    transitions.AddRange(CreateOne(from, to, specificCondition, duration, blendingFunction)._transitions);
                }
            }
            TransitionInput result = new TransitionInput(many: transitions);
            return result;
        }

        public BlendTransition(int from, int to, ConditionDelegate? specificCondition = null, float duration = 0, BlendingDelegate? blendingFunction = null) : base(from, to, specificCondition, duration)
        {
            _linearProgressFunction = time => (time - _startTime) / _duration;
            if (blendingFunction == null)
            {
                blendingFunction = time => time;
            }
            _blendingFunction = blendingFunction;
        }

        public override void Begin()
        {
            _startTime = _machine.GetCurrentTimeFunction();
            _From.Weight = 1;
            _To.Weight = 0;
            base.Begin();
        }

        public override async Task<bool> Progress()
        {
            float progress = 0;
            if (_duration < 0.01f)
            {
                return true;
            }

            float time = _linearProgressFunction(_machine.GetCurrentTimeFunction());
            progress = _blendingFunction(time);
            await Task.Yield();
            _machine._enum2state[_to].Weight = progress;
            _machine._enum2state[_from].Weight = 1.0f - progress;
            _machine._currentState.OnUpdate();
            _machine._targetState?.OnUpdate();

            if (time >= 1.0f)
            {
                return true;
            }

            return false;
        }
    }
}
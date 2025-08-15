#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using StateID = System.Int32;

namespace Hexagon.StateMachine
{
    public class BlendTransition : Transition
    {
        public delegate float BlendingDelegate(float time);

        public BlendingDelegate _blendingFunction;

        protected BlendingDelegate _linearProgressFunction;

        protected float _startTime = 0;

        public static TransitionGroup CreateOne<TStateEnum>(TStateEnum from, TStateEnum to, ConditionDelegate? specificCondition = null, float duration = 0, BlendingDelegate? blendingFunction = null) where TStateEnum : Enum
        {
            return CreateOne(StateMachine.EnumToID(from), StateMachine.EnumToID(to), specificCondition, duration, blendingFunction);
        }

        internal static TransitionGroup CreateOne(StateID from, StateID to, ConditionDelegate? specificCondition = null, float duration = 0, BlendingDelegate? blendingFunction = null)
        {
            BlendTransition transition = new BlendTransition(
                from: from,
                to: to,
                specificCondition: specificCondition,
                duration: duration,
                blendingFunction: blendingFunction
            );
            TransitionGroup result = new TransitionGroup(one: transition);
            return result;
        }

        public static TransitionGroup CreateMany<TStateEnum>(IEnumerable<TStateEnum> froms, IEnumerable<TStateEnum> tos, ConditionDelegate? specificCondition = null, float duration = 0, BlendingDelegate? blendingFunction = null) where TStateEnum : Enum
        {
            List<Transition> transitions = new List<Transition>();
            foreach (TStateEnum from in froms)
            {
                foreach (TStateEnum to in tos)
                {
                    transitions.AddRange(CreateOne(from, to, specificCondition, duration, blendingFunction)._transitions);
                }
            }
            TransitionGroup result = new TransitionGroup(many: transitions);
            return result;
        }

        public static TransitionGroup CreateMany(IEnumerable<StateID> froms, IEnumerable<StateID> tos, ConditionDelegate? specificCondition = null, float duration = 0, BlendingDelegate? blendingFunction = null)
        {
            List<Transition> transitions = new List<Transition>();
            foreach (StateID from in froms)
            {
                foreach (StateID to in tos)
                {
                    transitions.AddRange(CreateOne(from, to, specificCondition, duration, blendingFunction)._transitions);
                }
            }
            TransitionGroup result = new TransitionGroup(many: transitions);
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

    public class BlendTransition<TStateEnum> : BlendTransition where TStateEnum : Enum
    {
        public BlendTransition(int from, int to, ConditionDelegate? specificCondition = null, float duration = 0, BlendingDelegate? blendingFunction = null) : base(from, to, specificCondition, duration, blendingFunction)
        {
        }

        public static BlendTransitionFactory<TStateEnum> From(params TStateEnum[] froms)
        {
            var data = new BlendTransitionFactory<TStateEnum>
            {
                froms = new List<TStateEnum>(froms)
            };
            return data;
        }
    }

    public sealed class BlendTransitionFactory<TStateEnum> : 
        AbstractTransitionFactory<BlendTransitionFactory<TStateEnum>, TStateEnum>
            where TStateEnum : Enum
    {
        BlendTransition.BlendingDelegate? function;
        private float duration;

        public BlendTransitionFactory<TStateEnum> Blend(BlendTransition.BlendingDelegate function)
        {
            this.function = function;
            return this;
        }

        public BlendTransitionFactory<TStateEnum> Durate(float seconds)
        {
            duration = seconds;
            return this;
        }

        public override TransitionGroup If(Transition.ConditionDelegate? condition = null)
        {
            return BlendTransition.CreateMany(
                froms: this.froms,
                tos: this.tos,
                specificCondition: condition,
                duration: duration,
                blendingFunction: this.function
            );
        }
    }
}
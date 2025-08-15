#nullable enable

using System;
using System.Collections.Generic;

namespace Hexagon.StateMachine
{
    public abstract class AbstractTransitionFactory<TFactory, TStateEnum> 
        where TFactory : AbstractTransitionFactory<TFactory, TStateEnum>
        where TStateEnum : Enum
    {
        internal List<TStateEnum> froms = null!;
        internal List<TStateEnum> tos = null!;

        public virtual TFactory From(params TStateEnum[] fromStates)
        {
            this.froms = new List<TStateEnum>(fromStates);
            return (TFactory)this;
        }

        public virtual TFactory To(params TStateEnum[] toStates)
        {
            this.tos = new List<TStateEnum>(toStates);
            return (TFactory)this;
        }

        public abstract TransitionGroup If(Transition.ConditionDelegate? condition = null);
    }
}
#nullable enable

using System;
using System.Collections.Generic;

namespace Hexagon.StateMachine
{
    /// <summary>
    /// Serves as a base class for creating transition factories that generate transitions between states in a state machine with specified parameters. Helps providing a fluent pipeline interface for defining transitions.
    /// </summary>
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
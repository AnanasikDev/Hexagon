using System.Collections.Generic;

namespace Hexagon.StateMachine
{
    public readonly struct TransitionGroup
    {
        public readonly List<Transition> _transitions;

        public TransitionGroup(List<Transition> many)
        {
            _transitions = many;
        }

        public TransitionGroup(Transition one)
        {
            _transitions = new List<Transition> { one };
        }
    }
}
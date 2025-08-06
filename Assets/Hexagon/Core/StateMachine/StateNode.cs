using System;
using System.Collections.Generic;

using StateID = System.Int32;

public class StateNode
{
    public StateID _state;
    public List<Transition> _transitions;

    public static StateNode Create<TStateEnum, TParent>(TStateEnum state, List<Transition> transitions, Func<TParent> getParent) where TStateEnum : System.Enum where TParent : class
    {
        return new StateNode(StateMachine<TParent>.Get(state), transitions);
    }

    public StateNode(StateID state, List<Transition> transitions)
    {
        _state = state;
        _transitions = transitions;
    }
}
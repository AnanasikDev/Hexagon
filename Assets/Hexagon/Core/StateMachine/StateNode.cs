using System;
using System.Collections.Generic;

using StateID = System.Int32;

public class StateNode
{
    public StateID _state;
    public List<Transition> _transitions;

    public static StateNode Create<TStateEnum>(TStateEnum state, List<Transition> transitions) where TStateEnum  : Enum
    {
        return new StateNode(StateMachine.GetID(state), transitions);
    }

    public StateNode(StateID state, List<Transition> transitions)
    {
        _state = state;
        _transitions = transitions;
    }
}
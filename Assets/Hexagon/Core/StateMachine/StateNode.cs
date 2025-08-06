using System;
using System.Collections.Generic;

using StateID = System.Int32;

public class StateNode<TParent> where TParent : class
{
    public StateID _state;
    public List<Transition> _transitions;
    public TParent _parent { get; init; }

    public static StateNode<TParent> Create<TStateEnum>(TStateEnum state, List<Transition> transitions, TParent parent) where TStateEnum  : Enum
    {
        return new StateNode<TParent>(StateMachine<TParent>.Get(state), transitions, parent);
    }

    public StateNode(StateID state, List<Transition> transitions, TParent parent)
    {
        _state = state;
        _transitions = transitions;
        _parent = parent;
    }
}
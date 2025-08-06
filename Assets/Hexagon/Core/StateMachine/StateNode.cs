using System.Collections.Generic;

public class StateNode<TStateEnum> where TStateEnum : System.Enum
{
    public TStateEnum _state;
    public List<Transition<TStateEnum>> _transitions;

    public StateNode(TStateEnum state, List<Transition<TStateEnum>> transitions)
    {
        _state = state;
        _transitions = transitions;
    }
}
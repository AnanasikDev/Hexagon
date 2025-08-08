#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using StateID = System.Int32;

[Serializable]
public class StateMachine
{
    public State _currentState { get; protected set; } = null!;
    public State? _targetState { get; protected set; } = null;
    public State _previousState { get; protected set; } = null!;
    public StateID _currentStateID { get; protected set; } = 0;
    protected Transition? _currentTransition = null;

    public readonly Dictionary<StateID, State> _enum2state = new();
    public readonly List<StateNode> _nodes = new();

    public readonly Dictionary<StateID, List<Transition>> _stateTree = new();

    public bool _isTransitioning { get; protected set; } = false;
    public bool _isLocked { get; protected set; } = false;


    public delegate float GetCurrentTimeDelegate();
    public GetCurrentTimeDelegate GetCurrentTimeFunction = () => UnityEngine.Time.time;

    public virtual void Init<TStateEnum>(Dictionary<TStateEnum, State> enum2state, List<StateNode> nodes) where TStateEnum : Enum
    {
        foreach (var pair in enum2state)
        {
            _enum2state.Add(GetID(pair.Key), pair.Value);
            pair.Value._type = GetID(pair.Key);
            pair.Value.Init(this);

            if (pair.Value == null)
            {
                throw new InvalidOperationException($"State {pair.Key} cannot be null.");
            }
        }

        _nodes.Clear();
        _nodes.AddRange(nodes);

        foreach (StateNode node in nodes)
        {
            _stateTree[node._state] = node._transitions;
            foreach (Transition transition in node._transitions)
            {
                transition.Init(this);
            }
        }

        if (_enum2state.Count == 0) 
        {
            throw new InvalidOperationException("StateMachine must have at least one state defined.");
        }

        _currentState = _enum2state[0];
        _previousState = _currentState;
        _currentState.OnEnter();
    }

    public virtual async void Update()
    {
        if (_isTransitioning && _currentTransition != null)
        {
            bool finished = await _currentTransition.Progress();

            if (!finished) return;

            FinishTransition();
        }

        (StateID state, Transition? transition) = GetNextState();

        if (transition != null)
        {
            BeginTransition(state, transition);
        }

        _currentState.OnUpdate();
    }

    public virtual void FixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }

    public virtual (StateID, Transition?) GetNextState(Func<Transition, bool>? extraCondition = null)
    {
        StateID result = _currentState._type;

        if (_isLocked || _isTransitioning) return (result, null);

        Transition? transition = GetTransitionFromCurrent(extraCondition);
        if (transition == null) return (result, null);

        result = transition._to;
        return (result, transition);
    }

    protected virtual Transition? GetTransitionFromCurrent(Func<Transition, bool>? extraCondition = null)
    {
        foreach (Transition transition in _stateTree[_currentState._type])
        {
            if (transition._condition(_currentState) &&
                (extraCondition?.Invoke(transition) ?? true) &&
                _enum2state[transition._from].IsPossibleChangeFrom() &&
                _enum2state[transition._to].IsPossibleChangeTo())
            {
                return transition;
            }
        }

        return null;
    }

    protected virtual void BeginTransition(StateID newState, Transition transition)
    {
        _isTransitioning = true;
        _targetState = _enum2state[newState];
        _currentState.OnExit();
        _targetState.Weight = 0;
        _currentState.Weight = 1;

        _currentTransition = transition;
        _currentTransition.Begin();
    }

    public virtual State FinishTransition()
    {
        _currentState.OnEnter();
        _targetState!.Weight = 1;
        _currentState.Weight = 0;

        _previousState = _currentState;
        _currentState = _enum2state[_targetState!._type];

        _currentTransition!.Finish();

        _targetState = null;
        _currentTransition = null;
        _isTransitioning = false;

        OnStateChanged(_previousState, _currentState);
        return _currentState;
    }

    public virtual void Die()
    {
        _currentState?.OnExit();
    }

    protected virtual void OnStateChanged(State from, State to) { }

    public static StateID GetID<TEnumState>(TEnumState value) where TEnumState : Enum
    {
        return Convert.ToInt32(value);
    }
}

[Serializable]
public class StateMachine<TParent> : StateMachine where TParent : class
{
    public TParent Parent { get; init; }

    public StateMachine(TParent parent)
    {
        Parent = parent;
        if (Parent == null)
        {
            throw new ArgumentNullException(nameof(parent), "Parent of a StateMachine<TParent> cannot be null.");
        }
    }
}
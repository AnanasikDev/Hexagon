#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using StateID = System.Int32;

[Serializable]
public class StateMachine
{
    public State _currentState { get; protected set; } = null!;
    public State _previousState { get; protected set; } = null!;
    public StateID _currentStateID { get; protected set; } = 0;

    public readonly Dictionary<StateID, State> _enum2state = new();
    public readonly List<StateNode> _nodes = new();

    public readonly Dictionary<StateID, List<Transition>> _stateTree = new();

    public bool _isTransitioning { get; protected set; } = false;
    public bool _isLocked { get; protected set; } = false;

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
        if (_isTransitioning) return;

        StateID newState = await GetNextState();

        if (newState != _currentState._type)
        {
            ForceNewState(newState);
        }

        _currentState.OnUpdate();
    }

    public virtual void FixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }

    public virtual async Task<StateID> GetNextState(Func<Transition, bool>? extraCondition = null)
    {
        StateID result = _currentState._type;

        if (_isLocked || _isTransitioning) return result;

        Transition? transition = GetTransitionFromCurrent(extraCondition);
        if (transition == null) return result;

        result = transition._to;
        if (transition._delay == 0)
        {
            return result;
        }

        _isTransitioning = true;
        await transition.Start();
        _isTransitioning = false;

        return result;
    }

    public virtual async Task<bool> IsAvailableTo(StateID targetState)
    {
        StateID result = await GetNextState(transition => transition._to == targetState);
        return result != _currentState._type;
    }

    public async Task<bool> TryMoveTo(StateID targetState)
    {
        if (!await IsAvailableTo(targetState)) return false;

        ForceNewState(targetState);
        return true;
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

    public State ForceNewState<TStateEnum>(TStateEnum newState) where TStateEnum : Enum
    {
        return ForceNewState(GetID(newState));
    }

    public virtual State ForceNewState(StateID newState)
    {
        _previousState = _currentState;
        _currentState.OnExit();
        _currentState = _enum2state[newState];
        _currentState.OnEnter();
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

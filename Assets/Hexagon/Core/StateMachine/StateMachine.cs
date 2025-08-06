using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using StateID = System.Int32;

public class StateMachine
{
    public TParent _parent { get; init; }

    public State _currentState = null;
    public StateID _currentStateID;

    public Dictionary<StateID, State> _enum2state = new();
    public List<StateNode> _nodes = new();

    private Dictionary<StateID, List<Transition>> _stateTree = new();

    public bool _isTransitioning = false;
    public bool _isLocked = false;

    public StateMachine()
    {
    }

    public TParent GetParent<TParent>(IParentProvider<TParent> provider) where TParent : class
    {
        return provider.GetParent();
    }

    public static StateID Get<TEnumState>(TEnumState value) where TEnumState : Enum
    {
        return Convert.ToInt32(value);
    }

    public void Init(Dictionary<StateID, State<>> enum2state, List<StateNode> nodes)
    {
        this._enum2state = enum2state;
        this._nodes = nodes;

        _stateTree = new();

        foreach (StateNode node in nodes)
        {

        }

        _currentState = enum2state[0];
        _currentState.OnEnter();
        OnStateChanged();
    }

    public async void Update()
    {
        if (!_isTransitioning) return;

        StateID newState = await GetNextState();

        if (!Enum.Equals(newState, _currentState.type))
        {
            _currentState.OnExit();
            Debug.Log($"Transition from {_currentState.type} to {newState}");
            _currentState = _enum2state[newState];
            _currentState.OnEnter();
            OnStateChanged();
        }

        _currentState.OnUpdate();
    }

    public void FixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }

    public async Task<StateID> GetNextState(Func<Transition, bool> extraCondition = null)
    {
        StateID result = _currentState.type;

        if (_isLocked || _isTransitioning) return result;

        Transition transition = GetTransitionFromCurrent(extraCondition);
        if (transition == null) return result;

        result = transition.to;
        if (transition.finished)
        {
            transition.finished = false;
            return result;
        }

        _isTransitioning = true;
        await transition.Start();
        _isTransitioning = false;

        return result;
    }

    protected virtual void OnStateChanged()
    {
    }

    public async Task<bool> SuggestTransitionTo(StateID targetState)
    {
        if (_isTransitioning) return false;

        Transition transition = GetTransitionFromCurrent
            (
                transition => Enum.Equals(transition.to, targetState)
            );

        if (transition == null) return false;

        if (transition.finished)
        {
            transition.finished = false;
            return true;
        }

        _isTransitioning = true;
        await transition.Start();
        _isTransitioning = false;

        return true;
    }

    protected virtual Transition GetTransitionFromCurrent(Func<Transition, bool> extraCondition = null)
    {
        foreach (Transition transition in _stateTree[_currentState.type])
        {
            if (transition.Condition(_currentState) &&
                (extraCondition?.Invoke(transition) ?? true) &&
                _enum2state[transition.from].IsPossibleChangeFrom() &&
                _enum2state[transition.to].IsPossibleChangeTo())
            {
                return transition;
            }
        }

        return null;
    }

    public State ForceNewState<TStateEnum>(TStateEnum newState) where TStateEnum : Enum
    {
        return ForceNewState(Get(newState));
    }
    public virtual State ForceNewState(StateID newState)
    {
        _currentState.OnExit();
        _currentState = _enum2state[newState];
        _currentState.OnEnter();
        OnStateChanged();
        return _currentState;
    }

    public virtual void Die()
    {
        _currentState.OnExit();
    }
}

public interface IParentProvider<TParent> where TParent : class
{
    TParent GetParent();
}

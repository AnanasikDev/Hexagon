using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using StateID = System.Int32;

[Serializable]
public abstract class StateMachine
{
    public State _currentState = null;
    public StateID _currentStateID;

    public Dictionary<StateID, State> _enum2state = new();
    public List<StateNode> _nodes = new();

    public Dictionary<StateID, List<Transition>> _stateTree = new();

    public bool _isTransitioning = false;
    public bool _isLocked = false;

    public abstract void Init<TStateEnum>(Dictionary<TStateEnum, State> enum2state, List<StateNode> nodes) where TStateEnum : Enum;

    public static StateID Get<TEnumState>(TEnumState value) where TEnumState : Enum
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
    }

    public override void Init<TStateEnum>(Dictionary<TStateEnum, State> enum2state, List<StateNode> nodes)
    {
        foreach (var pair in enum2state)
        {
            _enum2state.Add(Get(pair.Key), pair.Value);
            pair.Value._type = Get(pair.Key);
            pair.Value.Init(this);
        }

        this._nodes = nodes;

        foreach (StateNode node in nodes)
        {
            _stateTree[node._state] = node._transitions;
        }

        _currentState = _enum2state[0];
        _currentState.OnEnter();
        OnStateChanged();
    }

    public async void Update()
    {
        if (_isTransitioning) return;

        StateID newState = await GetNextState();

        if (!Enum.Equals(newState, _currentState._type))
        {
            _currentState.OnExit();
            Debug.Log($"Transition from {_currentState._type} to {newState}");
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
        StateID result = _currentState._type;

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
        foreach (Transition transition in _stateTree[_currentState._type])
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

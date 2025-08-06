using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StateMachine<TStateEnum> where TStateEnum : System.Enum
{
    public State<TStateEnum> _currentState = null;
    public TStateEnum currentStateEnum;

    public Dictionary<TStateEnum, State<TStateEnum>> _enum2state = new();
    public List<StateNode<TStateEnum>> nodes = new();

    private Dictionary<TStateEnum, List<Transition<TStateEnum>>> stateTree = new();

    public bool _isTransitioning = false;
    public bool isLocked = false;

    public void Init(Dictionary<TStateEnum, State<TStateEnum>> enum2state, List<StateNode<TStateEnum>> nodes)
    {
        this._enum2state = enum2state;
        this.nodes = nodes;

        stateTree = new();

        foreach (StateNode<TStateEnum> node in nodes)
        {

        }

        _currentState = enum2state[(TStateEnum)(object)0];
        _currentState.OnEnter();
        OnStateChanged();
    }

    public async void Update()
    {
        if (!_isTransitioning) return;

        TStateEnum newState = await GetNextState();

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

    public async Task<TStateEnum> GetNextState(Func<Transition<TStateEnum>, bool> extraCondition = null)
    {
        TStateEnum result = _currentState.type;

        if (isLocked || _isTransitioning) return result;

        Transition<TStateEnum> transition = GetTransitionFromCurrent(extraCondition);
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

    public async Task<bool> SuggestTransitionTo(TStateEnum state)
    {
        if (_isTransitioning) return false;

        Transition<TStateEnum> transition = GetTransitionFromCurrent
            (
                transition => Enum.Equals(transition.to, state)
            );

        if (transition == null) return false;

        if (transition.finished)
        {
            state = transition.to;
            transition.finished = false;
            return true;
        }

        _isTransitioning = true;
        await transition.Start();
        _isTransitioning = false;

        return true;
    }

    protected virtual Transition<TStateEnum> GetTransitionFromCurrent(Func<Transition<TStateEnum>, bool> extraCondition = null)
    {
        foreach (Transition<TStateEnum> transition in stateTree[_currentState.type])
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

    public virtual State<TStateEnum> ForceNewState(TStateEnum newState)
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

    protected virtual TStateEnum GetDefaultStateEnum()
    {
        return (TStateEnum)(object)0;
    }
}

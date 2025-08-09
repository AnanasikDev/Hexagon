using System;
using System.Threading.Tasks;
using UnityEngine;
using StateID = System.Int32;

#nullable enable

public class Transition
{
    public StateMachine _machine = null!;

    public StateID _from;
    public StateID _to;
    public Func<State, bool> _condition;
    public float _duration = 0;

    public event Action? onFinishedEvent = null;

    public State _From { get { return _machine._enum2state[_from]; } }
    public State _To { get { return _machine._enum2state[_to]; } }

    public static Transition Create<TStateEnum>(TStateEnum from, TStateEnum to, Func<State, bool>? specificCondition = null, float delay = 0) where TStateEnum : Enum
    {
        return new Transition(
            from: StateMachine.GetID(from),
            to: StateMachine.GetID(to),
            specificCondition: specificCondition,
            delay: delay
        );
    }

    public void Init(StateMachine machine)
    {
        _machine = machine;
    }

    public Transition(StateID from, StateID to, Func<State, bool>? specificCondition = null, float delay = 0)
    {
        this._from = from;
        this._to = to;
        if (specificCondition == null)
        {
            specificCondition = state => true;
        }
        _condition = specificCondition;
        this._duration = delay;
    }

    public virtual async Task<bool> Progress()
    {
        if (_duration <= 0)
        {
            return true;
        }
        await Task.Delay(TimeSpan.FromSeconds(_duration));
        return true;
    }

    public virtual void Begin()
    {
        _From.Weight = 1;
        _To.Weight = 0;
    }

    public virtual void Finish()
    {
        _From.Weight = 0;
        _To.Weight = 1;
        onFinishedEvent?.Invoke();
    }
}

public class BlendTransition : Transition
{
    public delegate float BlendingDelegate(float time);

    public BlendingDelegate _blendingFunction;

    protected BlendingDelegate _linearProgressFunction;

    protected float _startTime = 0;

    public static BlendTransition Create<TStateEnum>(TStateEnum from, TStateEnum to, Func<State, bool>? specificCondition = null, float duration = 0, BlendingDelegate? blendingFunction = null) where TStateEnum : Enum
    {
        return new BlendTransition(
            from: StateMachine.GetID(from),
            to: StateMachine.GetID(to),
            specificCondition: specificCondition,
            duration: duration,
            blendingFunction: blendingFunction
        );
    }

    public BlendTransition(int from, int to, Func<State, bool>? specificCondition = null, float duration = 0, BlendingDelegate? blendingFunction = null) : base(from, to, specificCondition, duration)
    {
        _linearProgressFunction = time => (time - _startTime) / _duration;
        if (blendingFunction == null)
        {
            blendingFunction = time => time;
        }
        _blendingFunction = blendingFunction;
    }

    public override void Begin()
    {
        _startTime = _machine.GetCurrentTimeFunction();
        _From.Weight = 1;
        _To.Weight = 0;
        base.Begin();
    }

    public override async Task<bool> Progress()
    {
        float progress = 0;
        if (_duration < 0.01f)
        {
            return true;
        }

        float time = _linearProgressFunction(_machine.GetCurrentTimeFunction());
        progress = _blendingFunction(time);
        await Task.Yield();
        _machine._enum2state[_to].Weight = progress;
        _machine._enum2state[_from].Weight = 1.0f - progress;
        _machine._currentState.OnUpdate();
        _machine._targetState?.OnUpdate();

        if (time >= 1.0f)
        {
            return true;
        }

        return false;
    }
}
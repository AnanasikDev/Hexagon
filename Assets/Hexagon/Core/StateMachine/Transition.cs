using System;
using System.Threading.Tasks;
using UnityEngine;
using StateID = System.Int32;

public class Transition
{
    public StateMachine _machine;

    public StateID _from;
    public StateID _to;
    public Func<State, bool> _condition;
    public float _delay = 0;

    public event Action onFinishedEvent;

    public State _From { get { return _machine._enum2state[_from]; } }
    public State _To { get { return _machine._enum2state[_to]; } }

    public static Transition Create<TStateEnum>(TStateEnum from, TStateEnum to, Func<State, bool> specificCondition = null, float delay = 0) where TStateEnum : Enum
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

    public Transition(StateID from, StateID to, Func<State, bool> specificCondition = null, float delay = 0)
    {
        this._from = from;
        this._to = to;
        if (specificCondition == null)
        {
            specificCondition = state => true;
        }
        _condition = specificCondition;
        this._delay = delay;
    }

    public virtual async Task<bool> Progress()
    {
        if (_delay <= 0)
        {
            return true;
        }
        await Task.Delay(TimeSpan.FromSeconds(_delay));
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
    protected delegate float BlendingDelegate(float time);

    protected BlendingDelegate _blendingFunction;

    protected float _startTime = 0;

    public BlendTransition(int from, int to, Func<State, bool> specificCondition = null, float delay = 0) : base(from, to, specificCondition, delay)
    {
    }

    public static BlendTransition As(Transition transition)
    {
        return new BlendTransition(transition._from, transition._to, transition._condition, transition._delay);
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
        if (_delay < 0.01f)
        {
            return true;
        }

        progress = (_machine.GetCurrentTimeFunction() - _startTime) / _delay;
        await Task.Yield();
        _machine._enum2state[_to].Weight = progress;
        _machine._enum2state[_from].Weight = 1.0f - progress;
        _machine._currentState.OnUpdate();
        _machine._targetState?.OnUpdate();

        if (progress >= 1.0f)
        {
            return true;
        }

        return false;
    }
}
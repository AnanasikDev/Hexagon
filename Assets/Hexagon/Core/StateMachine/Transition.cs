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
    public bool _finished = false;

    public event Action onFinishedEvent;

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

    public virtual async Task Progress()
    {
        await Task.Delay(TimeSpan.FromSeconds(_delay));
    }

    public virtual async Task Start()
    {
        if (_delay == 0)
        {
            ForceFinish();
            return;
        }

        _finished = false;
        await Progress();

        ForceFinish();
    }

    public virtual void OnFinished()
    {
    }

    public virtual void ForceFinish()
    {
        _finished = true;
        OnFinished();
        onFinishedEvent?.Invoke();
    }
}

public class BlendTransition : Transition
{
    protected delegate float BlendingDelegate(float time);

    protected BlendingDelegate _blendingFunction;

    protected float _startTime = 0;

    protected BlendStateMachine _blendMachine;

    public BlendTransition(int from, int to, Func<State, bool> specificCondition = null, float delay = 0) : base(from, to, specificCondition, delay)
    {
    }

    public override Task Start()
    {
        _blendMachine = _machine as BlendStateMachine;
        _startTime = _blendMachine.GetCurrentTimeFunction();
        return base.Start();
    }

    public override async Task Progress()
    {
        float progress = 0;
        if (_delay < 0.01f)
        {
            throw new InvalidOperationException($"{nameof(_delay)} Delay has to be larger than 0.01");
        }

        while (progress < 1)
        {
            progress = (_blendMachine.GetCurrentTimeFunction() - _startTime) / _delay;
            Debug.Log("Progress");
            await Task.Yield();
            _machine._enum2state[_to].Weight = progress;
            _machine._enum2state[_from].Weight = 1.0f - progress;
        }
    }
}
using System;
using System.Threading.Tasks;

using StateID = System.Int32;

public class Transition
{
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

    public virtual Task Progress()
    {
        return Task.Delay(TimeSpan.FromSeconds(_delay));
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

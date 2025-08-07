using System;
using System.Threading.Tasks;

using StateID = System.Int32;

public class Transition
{
    public StateID from;
    public StateID to;
    public Func<State, bool> Condition;
    public float delay = 0;
    public bool finished = false;

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
        this.from = from;
        this.to = to;
        if (specificCondition == null)
        {
            specificCondition = state => true;
        }
        Condition = specificCondition;
        this.delay = delay;
    }

    public async Task<bool> Start()
    {
        if (delay == 0)
        {
            finished = true;
            onFinishedEvent?.Invoke();
            return true;
        }

        finished = false;
        await Task.Delay(TimeSpan.FromSeconds(delay));
        finished = true;

        onFinishedEvent?.Invoke();
        return false;
    }
}

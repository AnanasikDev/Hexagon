using System;
using System.Threading.Tasks;

using StateID = System.Int32;

public class Transition<TParent> where TParent : class
{
    public StateID from;
    public StateID to;
    public Func<State<TParent>, bool> Condition;
    public float delay = 0;
    public bool finished = false;

    public event Action onFinishedEvent;

    public static Transition<TParent> Create<TStateEnum>(TStateEnum from, TStateEnum to, Func<State<TParent>, bool> specificCondition = null, float delay = 0) where TStateEnum : Enum
    {
        return new Transition<TParent>(
            from: StateMachine<TParent>.Get(from),
            to: StateMachine<TParent>.Get(to),
            specificCondition: specificCondition,
            delay: delay
        );
    }

    public Transition(StateID from, StateID to, Func<State<TParent>, bool> specificCondition = null, float delay = 0)
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

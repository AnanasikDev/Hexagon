using System;
using System.Threading.Tasks;

public class Transition<TStateEnum> where TStateEnum : System.Enum
{
    public TStateEnum from;
    public TStateEnum to;
    public Func<State<TStateEnum>, bool> Condition;
    public float delay = 0;
    public bool finished = false;

    public event Action onFinishedEvent;

    public Transition(TStateEnum from, TStateEnum to, Func<State<TStateEnum>, bool> specificCondition = null, float delay = 0)
    {
        this.from = from;
        this.to = to;
        if (specificCondition == null)
        {
            specificCondition = (State<TStateEnum> state) => true;
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

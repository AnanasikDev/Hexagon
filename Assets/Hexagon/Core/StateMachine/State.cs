public abstract class State
{
    public StateMachine _machine;

    public int _type;
    private float _startTime = 0;
    public float ActiveTime { get { return UnityEngine.Time.time - _startTime; } }
    public float Weight { get; set; } = 1.0f;

    public virtual void Init(StateMachine machine)
    {
        _machine = machine;
        Init();
    }

    public abstract void Init();

    /// <summary>
    /// Called when state becomes the target state of the State Machine. Weight becomes 0. Transition has started.
    /// </summary>
    public virtual void OnTransitionToStarted() { }

    /// <summary>
    /// Called when state becomes the current state of the State Machine. Weight becomes 1. Transition has ended.
    /// </summary>
    public virtual void OnTransitionToFinished()
    {
        _startTime = UnityEngine.Time.time;
    }

    /// <summary>
    /// Called when state starts to lose weight to the target state of the State Machine. Weight becomes 1. Transition has started.
    /// </summary>
    public abstract void OnTransitionFromStarted();

    /// <summary>
    /// Called when state becomes the previous state of the State Machine. Weight becomes 0. Transition has ended.
    /// </summary>
    public virtual void OnTransitionFromFinished() { }
    

    public abstract void OnUpdate();
    public virtual void OnFixedUpdate() { }

    public abstract bool IsPossibleChangeFrom();
    public abstract bool IsPossibleChangeTo();

    public virtual void DrawGizmos() { }

    public bool IsMachine<TParent>() where TParent : class
    {
        return _machine is StateMachine<TParent>;
    }

    public StateMachine<TParent> GetMachine<TParent>() where TParent : class
    {
        return _machine as StateMachine<TParent>;
    }
}

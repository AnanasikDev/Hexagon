public abstract class State
{
    public StateMachine _machine;

    public int _type;
    private float _startTime = 0;
    public float ActiveTime { get { return UnityEngine.Time.time - _startTime; } }

    public virtual void Init(StateMachine machine)
    {
        _machine = machine;
    }

    public virtual void OnEnter()
    {
        _startTime = UnityEngine.Time.time;
    }
    public abstract void OnExit();
    public abstract void OnUpdate();
    public virtual void OnFixedUpdate() { }

    public abstract bool IsPossibleChangeFrom();
    public abstract bool IsPossibleChangeTo();

    public virtual void DrawGizmos() { }
}

public abstract class State<TParent> : State where TParent : class
{
    public TParent _parent;

    public override void Init(StateMachine machine)
    {
        base.Init(machine);
        _parent = (machine as StateMachine<TParent>).Parent;
    }
}
public abstract class State<TParent> where TParent : class
{
    public StateMachine _machine;

    public int _type;
    private float _startTime = 0;
    public float ActiveTime { get { return UnityEngine.Time.time - _startTime; } }

    public virtual void Init(StateMachine<TParent> machine)
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

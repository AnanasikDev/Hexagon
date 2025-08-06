public abstract class State<TStateEnum> where TStateEnum : System.Enum
{
    public TStateEnum type;
    private float startTime = 0;
    public float activeTime { get { return UnityEngine.Time.time - startTime; } }

    public State()
    {
    }

    public virtual void OnEnter()
    {
        startTime = UnityEngine.Time.time;
    }
    public abstract void OnExit();
    public abstract void OnUpdate();
    public virtual void OnFixedUpdate() { }

    public abstract bool IsPossibleChangeFrom();
    public abstract bool IsPossibleChangeTo();

    public virtual void DrawGizmos() { }
}

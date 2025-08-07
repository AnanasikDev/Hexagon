using System;

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

    public bool IsMachine<TParent>() where TParent : class
    {
        return _machine is StateMachine<TParent>;
    }

    public StateMachine<TParent> GetMachine<TParent>() where TParent : class
    {
        return _machine as StateMachine<TParent>;
    }
}

public abstract class State<TParent> : State where TParent : class
{
    public TParent _parent;

    public override void Init(StateMachine machine)
    {
        base.Init(machine);
        if (machine is not StateMachine<TParent>)
        {
            throw new InvalidOperationException($"StateMachine must be of type StateMachine<{typeof(TParent).Name}>");
        }
        _parent = GetMachine<TParent>().Parent;
    }
}
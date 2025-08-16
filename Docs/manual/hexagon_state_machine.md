@page hexagon_state_machine State machine notes

## State Machine

Universal state machine implementaion, independent from Unity and can be used in any C# project. Please note that Hexagon is designed specifically for Unity and for simpler usage within this environment it includes minor hooks to the Unity API, such as default current time function:

```csharp
public GetCurrentTimeDelegate GetCurrentTimeFunction = () => UnityEngine.Time.time;
```

It can be changed easily if needed.

To initialize a state machine it needs a mapping for enum value and state instances:

```csharp
stateMachine.Init(
    enum2state:
    new Dictionary<MyMachineState, State>()
    {
        { MyMachineState.Idle, new IdleState() },
        { MyMachineState.Running, new RunningState() }
    }
);
```

Apart from that it needs a list of transitions which is defined via `AddTransitions` and transition factories (more about it later):

```csharp
stateMachine.AddTransitions(
    Transition<MyMachineState>
        .From(MyMachineState.Idle)
        .To(MyMachineState.Running)
        .If((state, @event) =>
        {
            if (state.ActiveTime > 2) return true;
            return false;
        })
);
```

Alternatively, transitions may be created directly using generic classes' constructors, like so:

```csharp
stateMachine.AddTransitions(
    new List<Transition>() 
    { 
        new Transition<MyMachineState>(MyMachineState.Running, MyMachineState.Idle, null, 1) 
    }
);
```

Transition class can be inherited to create new behaviours. By default `BlendTransition` is included with Hexagon, which allows for smooth transitions by easing out old states and easing in new ones.

In order to write your own `Transition` implementation you need 3 classes:
1. Basic non-generic class with all the implementation.
2. Generic class with templated type for the enum representing all possible states of the state machine. This class allows for more fluent factory syntax and constructors.
3. Sealed factory class which derives from `AbstractTransitionFactory`. This class is the pipelined factory syntax provider.

At the last iteration transition factories return `TransitionGroup`s which are lists of transitions. For simple scenarios they will have only one transition per group, but in cases where identical transitions are needed for multiple source or destination states, groups may contain more items.

A good example of this is a situation when all modes should have a transition to the default mode:

```csharp
Transition<PlayerMode>
    .From(PlayerMode.Building, PlayerMode.Demolishing, PlayerMode.Connecting)
    .To(PlayerMode.Default)
    .If((State state, ExternalMachineEvent @event) =>
    {
        if (@event is EME_EnterDefault) return true;
        if (key_AnyToDefault == KeyCode.None) return true;
        return Input.GetKeyDown(key_AnyToDefault);
    })
```

In this case input key press event can even be moved out of the transition condition to the place where `EME_EnterDefault` is pushed, so that it may be pushed by key presses or other inputs.

When `From` or `To` are populated with multiple state IDs, the factory will generate multiple transitions. Each transition is still kept as a one-to-one unit.

---

States are aware of their owning state machine instance. The `StateMachine` class itself is independent of any owning object however, and for binding them together there is a templated `StateMachine<TParent>` class, deriving from the non-generic one. Within a state implementation getting state machine's owning object is as simple as this:

```csharp
machine = GetMachine<StateMachineExample>();
```

which is the same as writing:

```csharp
machine = _machine as StateMachine<StateMachineExample>;
```

Generic state machine is what you want to use if you need binding to the owning object.

---

State machine has an internal event queue which is checked at every transition attempt. The last event is checked by the transition condition. When current transition is chosen, last event is popped from the queue.

Events are classes deriving from `ExternalMachineEvent`. Extra members can be introduced if needed.

Events are pushed to a state machine using `PushEvent` method:

```csharp
private void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        stateMachine.PushEvent(new Event_Stop());
    }

    stateMachine.Update();
}
```

Transition condition can then check last event:

```csharp
...
.If((state, @event) =>
{
    if (@event is Event_Stop || ...) return true;
    return false;
}
```

or

```csharp
...
.If((state, @event) =>
{
    if (@event is Event_Stop eventStop && eventStop.SomeEventCondition() || ...) return true;
    return false;
}
```

---

Complete example


```csharp
using Hexagon;
using Hexagon.StateMachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class StateMachineExample : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    enum MyMachineState
    {
        Idle,
        Running
    }

    [SerializeField] private StateMachine<StateMachineExample> stateMachine;

    private void Start()
    {
        stateMachine = new (this);
        stateMachine.Init(
            enum2state:
            new Dictionary<MyMachineState, State>()
            {
                { MyMachineState.Idle, new IdleState() },
                { MyMachineState.Running, new RunningState() }
            }
        );
        stateMachine.AddTransitions(
            Transition<MyMachineState>
                .From(MyMachineState.Idle)
                .To(MyMachineState.Running)
                .If((state, @event) =>
                {
                    if (state.ActiveTime > 2) return true;
                    return false;
                }),

            BlendTransition<MyMachineState>
                .From(MyMachineState.Running)
                .To(MyMachineState.Idle)
                .Durate(0.3f)
                .Blend(time => HexEasing.EaseInBack(0, 1, time))
                .If((state, @event) =>
                {
                    if (@event is Event_Stop || state.ActiveTime > 6) return true;
                    return false;
                })
        );
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.PushEvent(new Event_Stop());
        }

        stateMachine.Update();
    }
}

class IdleState : State
{
    private StateMachine<StateMachineExample> machine;

    public override void Init()
    {
        machine = GetMachine<StateMachineExample>();
        Assert.IsNotNull(machine);
    }

    public override void OnTransitionToStarted()
    {
        
    }

    public override void OnTransitionToFinished()
    {
        base.OnTransitionToFinished();
        Debug.Log("Entered Idle State");
    }

    public override void OnTransitionFromStarted()
    {
        Debug.Log("Exited Idle State");
    }

    public override void OnUpdate()
    {
        machine.Parent.transform.Translate(Vector3.up * Time.deltaTime * Weight);
        machine.Parent.meshRenderer.material.color = Color.Lerp(machine.Parent.meshRenderer.material.color, Color.red, Weight);
    }
    public override bool IsPossibleChangeFrom() => true;
    public override bool IsPossibleChangeTo() => true;
}

class RunningState : State
{
    private StateMachine<StateMachineExample> machine;

    public override void Init()
    {
        machine = GetMachine<StateMachineExample>();
        Assert.IsNotNull(machine);
    }

    public override void OnTransitionToStarted()
    {
    }

    public override void OnTransitionToFinished()
    {
        base.OnTransitionToFinished();
        Debug.Log("Entered Running State");
    }
    public override void OnTransitionFromStarted()
    {
        Debug.Log("Exited Running State");
    }

    public override void OnUpdate()
    {
        machine.Parent.transform.Translate(Vector3.forward * Time.deltaTime * Weight);
        machine.Parent.meshRenderer.material.color = Color.Lerp(machine.Parent.meshRenderer.material.color, Color.green, Weight);
    }
    public override bool IsPossibleChangeFrom() => true;
    public override bool IsPossibleChangeTo() => true;
}

class Event_Stop : ExternalMachineEvent
{
}
```
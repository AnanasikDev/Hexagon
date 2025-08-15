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
            BlendTransition.CreateOne(
                from: MyMachineState.Idle,
                to: MyMachineState.Running,
                specificCondition: (state, @event) => state.ActiveTime > 2
            ),

            BlendTransition.CreateOne(
                from: MyMachineState.Running,
                to: MyMachineState.Idle,
                specificCondition: (state, @event) => @event is FSM_Stop || state.ActiveTime > 5,
                duration: 0.8f,
                blendingFunction: time => HexEasing.EaseInOutQuad(0, 1, time)
            )
        );
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.PushEvent(new FSM_Stop());
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
        // Idle logic here
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

class FSM_Stop : FSM_Event
{
}
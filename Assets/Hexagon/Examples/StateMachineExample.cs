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
            },

            nodes:
            new List<StateNode>()
            {
                StateNode.Create(MyMachineState.Idle, new List<Transition>()
                {
                    BlendTransition.Create(MyMachineState.Idle, MyMachineState.Running, state => state.ActiveTime > 3, 1f, time => HexEasing.EaseOutSineD(0, 1, time)),
                }),

                StateNode.Create(MyMachineState.Running, new List<Transition>()
                { 
                    BlendTransition.Create(MyMachineState.Running, MyMachineState.Idle, state => state.ActiveTime > 2, 1f, time => HexEasing.EaseInQuadD(0, 1, time))
                })
            }
        );
    }
    private void Update()
    {
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
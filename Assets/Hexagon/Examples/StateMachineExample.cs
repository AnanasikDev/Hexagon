using System.Collections.Generic;
using UnityEngine;

public class StateMachineExample : MonoBehaviour
{
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
                    BlendTransition.As(
                        Transition.Create(MyMachineState.Idle, MyMachineState.Running, state => state.ActiveTime > 3, 3)),
                }),

                StateNode.Create(MyMachineState.Running, new List<Transition>()
                { 
                    BlendTransition.As(
                        Transition.Create(MyMachineState.Running, MyMachineState.Idle, state => state.ActiveTime > 2, 3)),
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
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Entered Idle State");
    }
    public override void OnExit()
    {
        Debug.Log("Exited Idle State");
    }
    public override void OnUpdate()
    {
        // Idle logic here
        GetMachine<StateMachineExample>().Parent.transform.Translate(Vector3.up * Time.deltaTime * Weight);
    }
    public override bool IsPossibleChangeFrom() => true;
    public override bool IsPossibleChangeTo() => true;
}

class RunningState : State
{
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Entered Running State");
    }
    public override void OnExit()
    {
        Debug.Log("Exited Running State");
    }
    public override void OnUpdate()
    {
        GetMachine<StateMachineExample>().Parent.transform.Translate(Vector3.forward * Time.deltaTime * Weight);
    }
    public override bool IsPossibleChangeFrom() => true;
    public override bool IsPossibleChangeTo() => true;
}
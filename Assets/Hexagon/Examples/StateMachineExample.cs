using System.Collections.Generic;
using UnityEngine;

public class StateMachineExample : MonoBehaviour
{
    enum MyMachineState
    {
        Idle,
        Running
    }

    private StateMachine<StateMachineExample> stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine<StateMachineExample>(this);
        stateMachine.Init(
            enum2state:
            new Dictionary<MyMachineState, State<StateMachineBehaviour>>()
            {
                { MyMachineState.Idle, new IdleState() },
                { MyMachineState.Running, new RunningState() }
            },

            nodes:
            new List<StateNode>()
            {
                StateNode.Create(MyMachineState.Idle, new List<Transition>()
                {
                    Transition.Create(MyMachineState.Idle, MyMachineState.Running, state => state.activeTime > 3),
                },
                
                () => this),

                StateNode.Create(MyMachineState.Running, new List<Transition>()
                {
                    Transition.Create(MyMachineState.Running, MyMachineState.Idle, state => state.activeTime > 2),
                })
            }
        );
    }
    private void Update()
    {
        stateMachine.Update();
    }
}

class IdleState<StateMachineExample> : State<StateMachineBehaviour>
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
        _machine._parent.
        myobject.Move(2);
    }
    public override bool IsPossibleChangeFrom() => true;
    public override bool IsPossibleChangeTo() => true;
}
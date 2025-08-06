using System.Collections.Generic;
using UnityEngine;

public class StateMachineExample : MonoBehaviour
{
    enum MyMachineState
    {
        Idle,
        Running
    }

    private StateMachine<MyMachineState> stateMachine;
    private void Start()
    {
        stateMachine = new StateMachine<MyMachineState>();
        stateMachine.Init(
            enum2state:
            new Dictionary<MyMachineState, State<MyMachineState>>()
            {
                { MyMachineState.Idle, new IdleState() },
                { MyMachineState.Running, new RunningState() }
            },

            nodes:
            new List<StateNode<MyMachineState>>()
            {
                new StateNode<MyMachineState>(MyMachineState.Idle, new List<Transition<MyMachineState>>()
                {
                    new Transition<MyMachineState>(MyMachineState.Idle, MyMachineState.Running, state => true)
                }),
                new StateNode<MyMachineState>(MyMachineState.Running, new List<Transition<MyMachineState>>()
                {
                    new Transition<MyMachineState>(MyMachineState.Running, MyMachineState.Idle, state => true)
                })
            }
        );
    }
    private void Update()
    {
        stateMachine.Update();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    AIController controller;
    StateMachine stateMachine;

    public IdleState(StateMachine stateMachine, AIController controller)
    {
        this.controller = controller;
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {

    }

    public override void Update()
    {
        if (controller.canSeePlayer)
            stateMachine.ChangeState(controller.pursuit);
        else
            stateMachine.ChangeState(controller.patrol);
    }

    public override void Exit()
    {
        
    }


}

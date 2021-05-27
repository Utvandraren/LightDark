using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitState : State
{
    AIController controller;
    StateMachine stateMachine;

    public PursuitState(StateMachine stateMachine, AIController controller)
    {
        this.controller = controller;
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        controller.navAgent.isStopped = false;
        controller.navAgent.speed = controller.runSpeed;
        controller.navAgent.angularSpeed = controller.runAngularSpeed;

        controller.animator.SetBool("seePlayer", true);
    }

    public override void Update()
    {
        controller.navAgent.destination = controller.startTarget.position;

        
            

        if (!controller.canSeePlayer)
            stateMachine.ChangeState(controller.idle);
    }

    public override void Exit()
    {
        controller.navAgent.isStopped = true;
        controller.navAgent.speed = controller.walkSpeed;
        controller.navAgent.angularSpeed = controller.walkAngularSpeed;
        controller.animator.SetBool("seePlayer", false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToState : State
{
    AIController controller;
    StateMachine stateMachine;

    public GoToState(StateMachine stateMachine, AIController controller)
    {
        this.controller = controller;
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        controller.navAgent.isStopped = false;
        controller.navAgent.speed = controller.runSpeed;
        controller.navAgent.angularSpeed = controller.runAngularSpeed;
        controller.navAgent.destination = controller.startTarget.position;
        controller.animator.SetBool("noticedSomething", true);

        
    }

    public override void Update()
    {
        if (hasArrived())
            stateMachine.ChangeState(controller.idle);

        //if (!controller.canSeePlayer)
        //    stateMachine.ChangeState(controller.idle);
    }

    public override void Exit()
    {
        controller.navAgent.isStopped = true;
        controller.navAgent.speed = controller.walkSpeed;
        controller.navAgent.angularSpeed = controller.walkAngularSpeed;
        controller.animator.SetBool("noticedSomething", false);

    }

    bool hasArrived()
    {
        float dist = controller.navAgent.remainingDistance;
        if (dist != Mathf.Infinity && controller.navAgent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && controller.navAgent.remainingDistance == 0)
            return true;

        return false;
    }
}

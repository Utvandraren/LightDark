using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    AIController controller;
    StateMachine stateMachine;

    public PatrolState(StateMachine stateMachine, AIController controller)
    {
        this.controller = controller;
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        controller.navAgent.isStopped = false;
        controller.index = controller.index == controller.waypoints.Length - 1 ? 0 : controller.index + 1;
    }

    public override void Update()
    {
        controller.navAgent.destination = controller.waypoints[controller.index].position;

        if (hasArrived())
            stateMachine.ChangeState(controller.idle);

        if (controller.canSeePlayer)
            stateMachine.ChangeState(controller.pursuit);
    }

    public override void Exit()
    {
        controller.navAgent.isStopped = true;

    }

    bool hasArrived()
    {
        float dist = controller.navAgent.remainingDistance;
        if (dist != Mathf.Infinity && controller.navAgent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && controller.navAgent.remainingDistance == 0)
            return true;

        return false;
    }
}

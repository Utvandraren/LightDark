using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 0f;
    public Transform[] waypoints;
    Transform startTarget;
    NavMeshAgent navAgent;
    Animator animator;
    float walkSpeed;
    bool seekTarget = false;
    int index;
   

    // Start is called before the first frame update
    void Start()
    {
        startTarget = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        walkSpeed = navAgent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (seekTarget)
            MoveTowardsTarget(startTarget);
    }

    public void StartSeekingTarget()
    {
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;
        seekTarget = true;
        animator.SetBool("seePlayer", true);
    }

    public void StopMovingToTarget()
    {
        navAgent.isStopped = true;
        navAgent.speed = walkSpeed;
        seekTarget = false;
        animator.SetBool("seePlayer", false);
    }

    void MoveTowardsTarget(Transform target)
    {
        navAgent.destination = target.position;        
    }

    public void ChangeTarget(Transform newTarget)
    {
        startTarget = newTarget;
    }

    public void StartWander()
    {

    }

    public void Patrol()
    {

    }
}

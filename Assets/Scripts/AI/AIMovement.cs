using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 0f;
    public Transform[] waypoints;
    Transform player;
    NavMeshAgent navAgent;
    Animator animator;
    float walkSpeed;
    [SerializeField] bool seekTarget = false;
    int index;
   

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        TryGetComponent(out animator);
        //animator = GetComponent<Animator>();
        walkSpeed = navAgent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (seekTarget)
            MoveTowardsTarget(player.position);
    }

    public void StartSeekingTarget()
    {
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;
        seekTarget = true;
        if (animator)
            animator.SetBool("seePlayer", true);
    }

    public void StopMovingToTarget()
    {
        navAgent.isStopped = true;
        navAgent.speed = walkSpeed;
        seekTarget = false;
        if (animator)
            animator.SetBool("seePlayer", false);
    }

    public void MoveTowardsTarget(Vector3 target)
    {
        navAgent.SetDestination(target);

    }

    public void MoveAwayFromTarget()
    {
        //StopMovingToTarget();
        navAgent.speed = runSpeed;
        seekTarget = false;
        
        Vector3 newPos = Random.insideUnitCircle * 100f;
        //navAgent.destination = -target.position;
        navAgent.SetDestination(newPos);
        StartCoroutine("MoveAwayCooldown");
    }

    

    IEnumerator MoveAwayCooldown()
    {
        yield return new WaitForSeconds(3f);
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;
        MoveTowardsTarget(player.position);

    }

    public void ChangeTarget(Transform newTarget)
    {
        player = newTarget;
    }

    public void StartWander()
    {

    }

    public void Patrol()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIController : MonoBehaviour
{
    Transform playerTransform;

    public float walkSpeed;
    public float runSpeed;
    [HideInInspector] public float walkAngularSpeed;
    [HideInInspector] public float runAngularSpeed;

    public Transform[] waypoints;

    [HideInInspector]
    public bool canSeePlayer = false;
    [HideInInspector]
    public Transform startTarget;
    [HideInInspector]
    public NavMeshAgent navAgent;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public int index;

    StateMachine stateMachine;
    [HideInInspector]
    public IdleState idle;
    [HideInInspector]
    public PursuitState pursuit;
    [HideInInspector]
    public PatrolState patrol;
    [HideInInspector]
    public GoToState goTo;


    // Start is called before the first frame update
    void Start()
    {
        index = Random.Range(0, waypoints.Length);

        startTarget = GameObject.FindGameObjectWithTag("Player").transform;
        playerTransform = startTarget;
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        walkSpeed = navAgent.speed;
        walkAngularSpeed = navAgent.angularSpeed;
        runAngularSpeed = walkAngularSpeed * 2f;

        stateMachine = new StateMachine();
        idle = new IdleState(stateMachine, this);
        pursuit = new PursuitState(stateMachine, this);
        patrol = new PatrolState(stateMachine, this);
        goTo = new GoToState(stateMachine, this);
        stateMachine.Initialize(idle);
    }

    void Update()
    {
        stateMachine.currentState.Update();
    }

    public void NoticePlayer()
    {
        canSeePlayer = true;
    }

    public void CantSeePlayer()
    {
        canSeePlayer = false;
    }

    public void GoToTarget(Transform target)
    {
        startTarget = target;
        stateMachine.ChangeState(goTo);
    }
    public void GoToPlayerLatestPosition()
    {
        //NoticePlayer();
        startTarget = playerTransform;
        stateMachine.ChangeState(goTo);
    }
   
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PatrolArea"))
        {
            List<Transform> tempWaypoints = new List<Transform>();
            foreach (Transform children in other.transform)
            {
                tempWaypoints.Add(children);
            }
            waypoints = tempWaypoints.ToArray();

        }


    }

    void OnTriggerExit(Collider other)
    {
        
    }

}

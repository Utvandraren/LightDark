using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIController : MonoBehaviour
{

    public float walkSpeed;
    public float runSpeed;
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
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        walkSpeed = navAgent.speed;

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

    public void GoToTarget(Vector3 target)
    {
        stateMachine.ChangeState(goTo);
    }
}

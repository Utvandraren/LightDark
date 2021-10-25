using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StaringEyes : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("Blink", 15f, 15f);
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        
    }

    void Blink()
    {
        animator.SetTrigger("Blink");
    }
}

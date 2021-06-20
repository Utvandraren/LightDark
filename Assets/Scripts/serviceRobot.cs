using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class serviceRobot : MonoBehaviour
{
    [SerializeField] float alertArea = 10f;
    [SerializeField] float turnedOffTimer = 20f;
    [SerializeField] AudioClip alertSoundClip;
    [SerializeField] AudioClip loseSightPlayerSoundClip;


    NavMeshAgent agent;
    Transform playerTransform;
    bool seesPlayer = false;
    bool isAlerted = false;
    AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        Idle();
    }

    void FixedUpdate()
    {
        if (seesPlayer)
            agent.destination = playerTransform.position;
    }

    public void SeePlayer()
    {      
        agent.destination = playerTransform.position;
        seesPlayer = true;
        agent.isStopped = false;
        StopAllCoroutines();
    }

    public void LoseSightOfPlayer()
    {
        source.PlayOneShot(loseSightPlayerSoundClip);
        seesPlayer = false;
        agent.isStopped = true;
    }

    public void Idle()
    {
        StartCoroutine(ChangeRandomPosition());
        agent.isStopped = false;
        seesPlayer = false;
    }

    IEnumerator ChangeRandomPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(8f);
            Vector3 newPos = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
            agent.destination = transform.position + newPos;
        }
       
    }

    public void AlertNearbyEnemies()
    {
        if (isAlerted)
            return;

        isAlerted = true;
        source.PlayOneShot(alertSoundClip);
        Collider[] collisions = Physics.OverlapSphere(transform.position, alertArea);
        foreach (Collider collider in collisions)
        {
            if (collider.CompareTag("Enemy"))
                collider.GetComponent<AIController>().GoToPlayerLatestPosition();
        }
        TurnOff();
    }

    public void TurnOff()
    {
       
        StopAllCoroutines();
        StartCoroutine(TemporaryTurnOffRobot());
    }

    IEnumerator TemporaryTurnOffRobot()
    {
        seesPlayer = false;
        SensorToolkit.RangeSensor[] sensors = GetComponents<SensorToolkit.RangeSensor>();
        for (int i = 0; i < sensors.Length; i++)
        {
            sensors[i].enabled = false;
        }
        yield return new WaitForSeconds(turnedOffTimer);
        for (int i = 0; i < sensors.Length; i++)
        {
            sensors[i].enabled = true;
        }
        isAlerted = false;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alertArea);
    }
}

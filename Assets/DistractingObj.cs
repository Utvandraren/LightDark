using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(AudioSource))]
public class DistractingObj : MonoBehaviour
{
    [SerializeField] float velocityTreshold = 3f;
    [SerializeField] float alertArea = 12f;

    AudioSource source;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }
    
    void OnCollisionEnter(Collision collision)
    {       
        if (collision.relativeVelocity.magnitude <= velocityTreshold)
            AlertNearbyEnemies();
    }

    void AlertNearbyEnemies()
    {
        Debug.Log("Enemies heard something!");
        source.Play();
        Collider[] collisions = Physics.OverlapSphere(transform.position, alertArea);
        foreach (Collider collider in collisions)
        {
            if (collider.CompareTag("Enemy"))
                collider.GetComponent<AIController>().GoToTarget(transform);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, alertArea);
    }
}

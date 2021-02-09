using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExplosiveProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float explosiveRadius;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private SciptableAttackObj attack;

     Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        rigidBody = GetComponent<Rigidbody>();
    }

    void Explode() //The logic handling what happens if a interactable object or enemy is inside the explosion radius when the projectile explode
    {
        GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);
        effect.GetComponent<AudioSource>().Play();
        Destroy(effect, 10f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosiveRadius);

        foreach (Collider nearbyObj in colliders)
        {
            if (nearbyObj.TryGetComponent<Stats>(out Stats attackObj))
            {              
                attackObj.TakeDamage(attack);
            }
        }
    }

    void OnTriggerEnter(Collider other)  //Damage if possible the obj the projectile collided with and then explode 
    {
        if(other.CompareTag("Player") || other.isTrigger)
        {
            return;
        }       
        
        Explode();
        gameObject.SetActive(false);
        Destroy(gameObject, 3f);
    }

    public void SetDirection(Vector3 direction)
    {
        rigidBody.velocity = direction * speed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosiveRadius);
    }
}

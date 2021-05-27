using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    [Tooltip("How long the enemy stays after dying")]
    [SerializeField] private float deathTimer = .2f;

    [SerializeField] private GameObject deathEffect;

    protected override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
            
        if (health <= 0f)
        {
            Die();
        }

    }

    public override void Die()
    {
        base.Die();
        GameObject effect = GameObject.Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity); 
        Destroy(effect, 3);
        Destroy(gameObject, deathTimer);
    }
}

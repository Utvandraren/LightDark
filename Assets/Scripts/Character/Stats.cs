using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Stats : MonoBehaviour
{
    [HideInInspector] public int health;
    [SerializeField] public int startingHealth;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = startingHealth;
    }

    public virtual void TakeDamage(SciptableAttackObj attack)  //Logic handling taking damage
    {
        health -= attack.damage;
        if (health <= 0)
        {
            Die();
        }
    }



    public virtual void Die()  //virtual function for  whatever happens to the gameobject when it dies
    {
    }
}

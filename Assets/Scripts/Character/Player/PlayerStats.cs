using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    public static Action<int> OnHealthChanged;
    [SerializeField] ItemObj startItem;
   
    protected override void Start()
    {
        base.Start();
        //OnHealthChanged(startingHealth);
        //Managers.Inventory.SetEquippedItem(startItem);
    }

    

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        //OnHealthChanged(health);
    }

    public void RestoreHealth(int amount)
    {
        health += amount;
        health = Mathf.Min(health, startingHealth);
    }
    public override void Die()
    {
        base.Die();
       // GameManager.Instance.GameOver();
    }

    public void ResetStats()
    {
        health = startingHealth;
        GetComponent<WeaponManager>().ResetAllAmmo();
    }
}

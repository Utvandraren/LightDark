using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    public static Action<int> OnHealthChanged;   //Experiment with actions 
    public static Action<int> OnEnergyChanged;

    [SerializeField] ItemObj startItem;
    [SerializeField] int startEnergy;
    int _energy;




    protected override void Start()
    {
        base.Start();
        _energy = startEnergy;
        //OnHealthChanged(startingHealth);
        //OnEnergyChanged(startEnergy);
        Managers.Inventory.SetEquippedItem(startItem);
        Managers.Player.SetPlayerObj(gameObject);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        OnHealthChanged(health);
    }

    public void RestoreHealth(int amount)
    {
        health += amount;
        health = Mathf.Min(health, startingHealth);
    }

    public void ConsumeEnergy(int energyUsed)
    {
        _energy -= energyUsed;
        _energy = Mathf.Max(_energy, 0);
        OnEnergyChanged(_energy);
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

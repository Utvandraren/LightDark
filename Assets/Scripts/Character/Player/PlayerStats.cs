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
    [HideInInspector] public int energy;

    protected override void Start()
    {
        base.Start();
        Invoke("ResetStats", 0.5f);
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
        OnHealthChanged(health);
    }

    public void ConsumeEnergy(int energyUsed)
    {
        energy -= energyUsed;
        energy = Mathf.Max(energy, 0);
        OnEnergyChanged(energy);
    }

    public override void Die()
    {
        base.Die();
       // GameManager.Instance.GameOver();
    }

    public void ResetStats()
    {
        health = startingHealth;
        energy = startEnergy;
        OnHealthChanged(health); //To update the data in UI
        OnEnergyChanged(energy);

    }
}

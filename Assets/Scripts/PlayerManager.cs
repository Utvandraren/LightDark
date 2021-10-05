using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public GameObject playerObj {
        get
        {
            if (_PlayerObj == null)
            {
                _PlayerObj = FindObjectOfType<PlayerStats>().gameObject;
            }
            return _PlayerObj;
        }
        private set
        {
            _PlayerObj = value;
        }
    }
    public int health { get; private set; }
    public int maxHealth { get; private set; }
    public int energy;
    GameObject _PlayerObj;


    public void Startup()
    {
        health = 50;
        maxHealth = 100;
        energy = 0;
        status = ManagerStatus.Started;
    }

    public void ChangeHealth(int value)
    {
        health += value;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        else if(health < 0)
        {
            health = 0;
        }
    }

    public void SetPlayerObj(GameObject newPlayer)
    {
        playerObj = newPlayer;
    }
}

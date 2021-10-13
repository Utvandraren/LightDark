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

    GameObject _PlayerObj;


    public void Startup()
    {
        status = ManagerStatus.Started;
    }


    public void SetPlayerObj(GameObject newPlayer)
    {
        playerObj = newPlayer;
    }
}

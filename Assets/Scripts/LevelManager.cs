using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public void Startup()
    {
        status = ManagerStatus.Started;
    }

    public void TransitionToLevel(string name)
    {

    }

}

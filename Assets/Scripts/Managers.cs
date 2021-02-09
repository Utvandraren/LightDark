using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(EventManager))]
[RequireComponent(typeof(LevelManager))]

public class Managers : MonoBehaviour
{
    public static PlayerManager Player { get; private set; }
    public static InventoryManager Inventory { get; private set; }
    public static EventManager Event { get; private set; }
    public static LevelManager Level { get; private set; }


    private List<IGameManager> _startSequence;

    void Awake()
    {
        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InventoryManager>();
        Event = GetComponent<EventManager>();
        Level = GetComponent<LevelManager>();


        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Inventory);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in _startSequence)
        {
            manager.Startup();
        }
        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;
            foreach (IGameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }
            if (numReady > lastReady)
                Debug.Log("Progress: " + numReady + "/" + numModules);
            yield return null;
        }
    }

}

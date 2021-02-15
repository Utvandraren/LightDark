using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject EnemyToSpawn;
    [SerializeField] Transform spawnPos;
    [SerializeField] bool spawnOnEnable = false;

    void Start()
    {
        if(spawnPos == null)
        {
            spawnPos = gameObject.transform;
        }
    }

    void OnEnable()
    {
        if (spawnOnEnable)
            SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        SpawnEnemyAt(spawnPos);
    }

    public void SpawnEnemyAt(Transform target)
    {
        GameObject Obj = Instantiate(EnemyToSpawn, target.position, Quaternion.identity);
        Obj.GetComponent<AIController>().GoToPlayerLatestPosition();
    }
}

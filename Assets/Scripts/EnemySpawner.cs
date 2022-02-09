using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartSpawnEnemies()
    {

    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        while (true)
        {

        }
        yield return new WaitForSeconds(1f);
    }

    void SpawnEnemy()
    {

    }
}

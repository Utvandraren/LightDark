using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float dayCycleVelocity = 1f;

    [Header("Enemies")]
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private GameObject giantToSpawn;
    [Range(0, 10)]
    [SerializeField] int maxAmountEnemies = 4;
    [SerializeField] float enemiesWaitTimeSpawnWindow = 5f;


    [HideInInspector] public bool isNight = false;

    Transform playerTransform;
    Light sunSource;
    int enemyCount = 0;
    List<GameObject> enemyList;

    // Start is called before the first frame update
    void Start()
    {
        sunSource = GetComponentInChildren<Light>();
        playerTransform = Managers.Player.playerObj.transform;
        enemyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleDayCycle();
    }

    void HandleDayCycle()
    {
        float angleX = sunSource.transform.rotation.eulerAngles.x;
        sunSource.transform.Rotate(Vector3.right, dayCycleVelocity * Time.deltaTime);

        if (angleX >= 200f && angleX <= 340f)
        {
            //Debug.Log("Nighty Nighty");
            if (!isNight)
                StartNightPhase();
        }
        else 
        {
            EndNightPhase();
        }
    }

    void StartNightPhase()
    {
        isNight = true;
        SpawnGiant();
        StartCoroutine("SpawnEnemiesCoroutine");
    }

    void EndNightPhase()
    {
        StopAllCoroutines();
        isNight = false;
        RemoveEnemies();
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        enemyCount = 0;

        while (isNight)
        {
            yield return new WaitForSeconds(enemiesWaitTimeSpawnWindow);
            if (enemyCount < maxAmountEnemies)
                SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 pos = playerTransform.position;
        float x = Random.Range(-10f, 10f);
        float z = Random.Range(-10f, 10f);
        x *= 20f;
        z *= 20f;
        pos.x += x;
        pos.y += 300f;
        pos.z += z;

        //Raycast down to get point where ground is
        RaycastHit hit;
        if (Physics.SphereCast(pos, 10f, Vector3.down, out hit))
        {
            pos = hit.point;
            pos.y += 20f;
            enemyList.Add(Instantiate(enemyToSpawn, pos, Quaternion.identity));
            enemyCount++;
        }
    }

    void SpawnGiant()       
    {
        Vector3 pos = playerTransform.position;
        float x = Random.Range(-10f, 10f);
        float z = Random.Range(-10f, 10f);
        x *= 2000f;
        z *= 2000f;
        pos.x += x;
        pos.y += 300f;
        pos.z += z;

        //Raycast down to get point where ground is
        RaycastHit hit;
        if (Physics.SphereCast(pos, 10f, Vector3.down, out hit))
        {
            pos = hit.point;
            pos.y += 20f;
            enemyList.Add(Instantiate(giantToSpawn, pos, Quaternion.identity));
            enemyCount++;
        }
        else
        {
            SpawnGiant();
        }
    }

    void RemoveEnemies()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            //TODO: add an effect to moster enememies to show them burying into the ground before destroyed 
            Destroy(enemyList[i]);
            enemyList.RemoveAt(i);
        }
    }
}

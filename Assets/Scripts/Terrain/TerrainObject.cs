using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class TerrainObject : MonoBehaviour
{
    [HideInInspector] public Terrain terrainData;
    bool isProcessed = false;
    TerrainManager manager;

    void Start()
    {
        terrainData = GetComponent<Terrain>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            manager.FocusNewObject(this);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }

    public void SetManager(TerrainManager newManager)
    {
        manager = newManager;
    }





}

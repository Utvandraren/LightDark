using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;


[RequireComponent(typeof(BoxCollider), typeof(NavMeshSurface))]
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

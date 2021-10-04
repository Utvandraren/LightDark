using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] GameObject[] terrainObjects;
    GameObject[,] instantiatedObjects;


    TerrainObject terrainInFocus;
    public TerrainObject terrainToDestroy;


    void Start()
    {
        instantiatedObjects = new GameObject[3, 3];
        terrainInFocus = FindObjectOfType<TerrainObject>();

        CreateStartTerrain();
    }

    public void FocusNewObject(TerrainObject obj)
    {
        //Debug.Log("New focus terrain");
        terrainInFocus = obj;
        CreateNewTerrain(terrainInFocus);
    }

    void CreateStartTerrain()
    {
        int k = 2;
        int l = 2;

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0)
                {
                    l--;
                    continue;                
                }
                Vector3 pos = new Vector3(i * 1000f, 0f, j * 1000f);
                GameObject objToInst = terrainObjects[Random.Range(0, terrainObjects.Length)];
                instantiatedObjects[k, l] = Instantiate(objToInst, pos, Quaternion.identity, transform);
                instantiatedObjects[k, l].GetComponent<TerrainObject>().SetManager(this);

                l--;
            }
            l = 2;
            k--;
        }
    }

    public void CreateNewTerrain(TerrainObject startTerrain)
    {

        if (startTerrain.gameObject == instantiatedObjects[0, 1]) //is up
        {
            Vector3 newPosition = startTerrain.transform.position;
            newPosition += new Vector3(1000f, 0f, 0f);
            GameObject newTerrain = CreateTerrain(newPosition);
            GameObject newLeftTerrain = CreateTerrain(newPosition + new Vector3(0, 0f, 1000f));
            GameObject newRightTerrain = CreateTerrain(newPosition + new Vector3(0, 0f, -1000f));

            Destroy(instantiatedObjects[2, 0]);
            Destroy(instantiatedObjects[2, 1]);
            Destroy(instantiatedObjects[2, 2]);

            for (int i = 2; i > 0; i--)
            {
                for (int j = 0; j < 3; j++)
                {
                    instantiatedObjects[i, j] = instantiatedObjects[i - 1, j];
                }
            }

            instantiatedObjects[0, 0] = newLeftTerrain;
            instantiatedObjects[0, 1] = newTerrain;
            instantiatedObjects[0, 2] = newRightTerrain;

        }
        else if (startTerrain.gameObject == instantiatedObjects[2, 1]) //is down
        {
            Vector3 newPosition = startTerrain.transform.position;
            newPosition += new Vector3(-1000f, 0f, 0f);
            GameObject newTerrain = CreateTerrain(newPosition);
            GameObject newLeftTerrain = CreateTerrain(newPosition + new Vector3(0, 0f, 1000f));
            GameObject newRightTerrain = CreateTerrain(newPosition + new Vector3(0, 0f, -1000f));


            Destroy(instantiatedObjects[0, 0]);
            Destroy(instantiatedObjects[0, 1]);
            Destroy(instantiatedObjects[0, 2]);

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    instantiatedObjects[i, j] = instantiatedObjects[i + 1, j];
                }
            }

            instantiatedObjects[0, 0] = newLeftTerrain;
            instantiatedObjects[0, 1] = newTerrain;
            instantiatedObjects[0, 2] = newRightTerrain;
        }
        else if (startTerrain.gameObject == instantiatedObjects[1, 2]) //is right
        {
            Vector3 newPosition = startTerrain.transform.position;
            newPosition += new Vector3(0f, 0f, -1000f);
            GameObject newTerrain = CreateTerrain(newPosition);

            instantiatedObjects[1, 2] = newTerrain;

        }
        else if (startTerrain.gameObject == instantiatedObjects[1, 0]) //is left
        {
            Vector3 newPosition = startTerrain.transform.position;
            newPosition += new Vector3(0f, 0f, 1000f);
            GameObject newTerrain = CreateTerrain(newPosition);

            instantiatedObjects[1, 0] = newTerrain;

        }
    }

    GameObject CreateTerrain(Vector3 pos)
    {
        GameObject objToInst = terrainObjects[Random.Range(0, terrainObjects.Length)];

        GameObject obj = Instantiate(objToInst, pos, Quaternion.identity, transform);
        obj.GetComponent<TerrainObject>().SetManager(this);
        return obj;
    }




    //public void CreateNewTerrain(TerrainObject startTerrain)
    //{
    //    Debug.Log("new terrain being created");

    //    if (startTerrain == terrainInFocus.terrainData.topNeighbor) //is up
    //    {            
    //        TerrainObject newTopTerrain = Instantiate(terrainObjects[0], transform).GetComponent<TerrainObject>();
    //        TerrainObject newTopLeftTerrain = Instantiate(terrainObjects[0], transform).GetComponent<TerrainObject>();
    //        TerrainObject newTopRightTerrain = Instantiate(terrainObjects[0], transform).GetComponent<TerrainObject>();

    //        startTerrain.terrainData.topNeighbor.SetNeighbors(startTerrain.terrainData.leftNeighbor, newTopTerrain.terrainData, startTerrain.terrainData.rightNeighbor, startTerrain.terrainData.bottomNeighbor);

    //        newTopTerrain.terrainData.SetNeighbors(newTopLeftTerrain.terrainData, null , newTopRightTerrain.terrainData, startTerrain.terrainData);
    //    }
    //    else if (startTerrain == terrainInFocus.terrainData.bottomNeighbor) //is down
    //    {
    //        TerrainObject newBottomTerrain = Instantiate(terrainObjects[0], transform).GetComponent<TerrainObject>();
    //        TerrainObject newBottomLeftTerrain = Instantiate(terrainObjects[0], transform).GetComponent<TerrainObject>();
    //        TerrainObject newBottomRightTerrain = Instantiate(terrainObjects[0], transform).GetComponent<TerrainObject>();

    //        startTerrain.terrainData.bottomNeighbor.SetNeighbors(startTerrain.terrainData.leftNeighbor, startTerrain.terrainData.topNeighbor, startTerrain.terrainData.rightNeighbor, newBottomTerrain.terrainData);

    //        newBottomTerrain.terrainData.SetNeighbors(newBottomLeftTerrain.terrainData, startTerrain.terrainData, newBottomRightTerrain.terrainData, null);
    //    }
    //    else if (startTerrain == terrainInFocus.terrainData.rightNeighbor) //is right
    //    {
    //        TerrainObject newRightTerrain = Instantiate(terrainObjects[0], transform).GetComponent<TerrainObject>();
    //        TerrainObject newRightTopTerrain = Instantiate(terrainObjects[0], transform).GetComponent<TerrainObject>();
    //        TerrainObject newRightBottomTerrain = Instantiate(terrainObjects[0], transform).GetComponent<TerrainObject>();

    //        startTerrain.terrainData.rightNeighbor.SetNeighbors(startTerrain.terrainData.leftNeighbor, startTerrain.terrainData.topNeighbor, newRightTerrain.terrainData, startTerrain.terrainData.bottomNeighbor);

    //        newRightTerrain.terrainData.SetNeighbors(startTerrain.terrainData, newRightTopTerrain.terrainData, null, newRightBottomTerrain.terrainData);
    //    }
    //    else if (startTerrain == terrainInFocus.terrainData.leftNeighbor) //is left
    //    {
    //        TerrainObject newLeftTerrain = Instantiate(terrainObjects[0], transform).GetComponent<TerrainObject>();
    //        TerrainObject newLeftTopTerrain = Instantiate(terrainObjects[0], transform).GetComponent<TerrainObject>();
    //        TerrainObject newLeftBottomTerrain = Instantiate(terrainObjects[0], transform).GetComponent<TerrainObject>();

    //        startTerrain.terrainData.rightNeighbor.SetNeighbors(newLeftTerrain.terrainData, startTerrain.terrainData.topNeighbor, startTerrain.terrainData.rightNeighbor, startTerrain.terrainData.bottomNeighbor);

    //        newLeftTerrain.terrainData.SetNeighbors(null, newLeftTerrain.terrainData, startTerrain.terrainData, newLeftBottomTerrain.terrainData);

    //    }
    //}
}

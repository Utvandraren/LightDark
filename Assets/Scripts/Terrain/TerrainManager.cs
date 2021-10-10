using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] GameObject[] terrainObjects;
    [SerializeField] GameObject[] rnadomObjectsToPlace;
    [SerializeField] GameObject[] stationaryObjectsToPlace;

    [Header("Objects")]
    [SerializeField] int objPerTerrain = 5;
    [SerializeField] int stationaryObjPerTerrain = 5;


    [SerializeField] bool DontSpawn = false;
    GameObject[,] instantiatedObjects;
    TerrainObject terrainInFocus;


    void Start()
    {
        instantiatedObjects = new GameObject[3, 3];
        terrainInFocus = FindObjectOfType<TerrainObject>();

        if (!DontSpawn)
            Invoke("CreateStartTerrain", 0.1f);
            //CreateStartTerrain();
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
                    instantiatedObjects[k, l] = terrainInFocus.gameObject;
                    PlaceObjects(instantiatedObjects[k, l].GetComponent<Terrain>());
                    PlaceStationaryObjects(instantiatedObjects[k, l].GetComponent<Terrain>());

                    l--;
                    continue;                
                }
                Vector3 pos = new Vector3(i * 1000f, 0f, j * 1000f);
                GameObject objToInst = terrainObjects[Random.Range(0, terrainObjects.Length)];
                instantiatedObjects[k, l] = Instantiate(objToInst, pos, Quaternion.identity, transform);
                instantiatedObjects[k, l].GetComponent<TerrainObject>().SetManager(this);
                PlaceObjects(instantiatedObjects[k, l].GetComponent<Terrain>());
                PlaceStationaryObjects(instantiatedObjects[k, l].GetComponent<Terrain>());

                l--;
            }
            l = 2;
            k--;
        }
        //instantiatedObjects[1,1].GetComponent<TerrainObject>().surface.BuildNavMesh();

    }

    public void CreateNewTerrain(TerrainObject startTerrain)
    {

        if (startTerrain.gameObject == instantiatedObjects[0, 1] || startTerrain.gameObject == instantiatedObjects[0, 0] || startTerrain.gameObject == instantiatedObjects[0, 2]) //is up
        {
            Vector3 newPosition = instantiatedObjects[0, 1].transform.position;
            newPosition += new Vector3(1000f, 0f, 0f);
            GameObject newTerrain = CreateTerrain(newPosition);
            GameObject newLeftTerrain = CreateTerrain(newPosition + new Vector3(0, 0f, 1000f));
            GameObject newRightTerrain = CreateTerrain(newPosition + new Vector3(0, 0f, -1000f));

            Destroy(instantiatedObjects[2, 0]);
            Destroy(instantiatedObjects[2, 1]);       //Fix these thayt doesnt seems to be working so they keep track of the right terrains
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
        else if (startTerrain.gameObject == instantiatedObjects[2, 1] || startTerrain.gameObject == instantiatedObjects[2, 0] || startTerrain.gameObject == instantiatedObjects[2, 2]) //is down
        {
            Vector3 newPosition = instantiatedObjects[2, 1].transform.position;
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

            instantiatedObjects[2, 0] = newLeftTerrain;
            instantiatedObjects[2, 1] = newTerrain;
            instantiatedObjects[2, 2] = newRightTerrain;
        }
        else if (startTerrain.gameObject == instantiatedObjects[1, 2] || startTerrain.gameObject == instantiatedObjects[0, 2] || startTerrain.gameObject == instantiatedObjects[2, 2]) //is right
        {
            Vector3 newPosition = instantiatedObjects[1, 2].transform.position;
            newPosition += new Vector3(0f, 0f, -1000f);
            GameObject newTerrain = CreateTerrain(newPosition);
            GameObject newLeftTerrain = CreateTerrain(newPosition + new Vector3(1000f, 0f, 0f));
            GameObject newRightTerrain = CreateTerrain(newPosition + new Vector3(-1000f, 0f, -0f));


            Destroy(instantiatedObjects[0, 0]);
            Destroy(instantiatedObjects[1, 0]);
            Destroy(instantiatedObjects[2, 0]);

            //instantiatedObjects[0, 0] = instantiatedObjects[0, 1];
            //instantiatedObjects[1, 0] = instantiatedObjects[1, 1];
            //instantiatedObjects[2, 0] = instantiatedObjects[2, 1];

            //instantiatedObjects[0, 1] = instantiatedObjects[0, 2];
            //instantiatedObjects[1, 1] = instantiatedObjects[1, 2];
            //instantiatedObjects[2, 1] = instantiatedObjects[2, 2];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    instantiatedObjects[i, j] = instantiatedObjects[i, j + 1];
                }
            }

            instantiatedObjects[0, 2] = newLeftTerrain;
            instantiatedObjects[1, 2] = newTerrain;
            instantiatedObjects[2, 2] = newRightTerrain;

            Debug.Log("Right side detected");
        }
        else if (startTerrain.gameObject == instantiatedObjects[1, 0] || startTerrain.gameObject == instantiatedObjects[0, 0] || startTerrain.gameObject == instantiatedObjects[2, 0]) //is left
        {
            Vector3 newPosition = instantiatedObjects[1, 0].transform.position;
            newPosition += new Vector3(0f, 0f, 1000f);
            GameObject newTerrain = CreateTerrain(newPosition);
            GameObject newLeftTerrain = CreateTerrain(newPosition + new Vector3(1000f, 0f, 0f));
            GameObject newRightTerrain = CreateTerrain(newPosition + new Vector3(-1000f, 0f, -0f));


            Destroy(instantiatedObjects[0, 2]);
            Destroy(instantiatedObjects[1, 2]);
            Destroy(instantiatedObjects[2, 2]);

            //instantiatedObjects[0, 2] = instantiatedObjects[0, 1];
            //instantiatedObjects[1, 2] = instantiatedObjects[1, 1]; //not done
            //instantiatedObjects[2, 2] = instantiatedObjects[2, 1];

            //instantiatedObjects[0, 1] = instantiatedObjects[0, 0];
            //instantiatedObjects[1, 1] = instantiatedObjects[1, 0]; //not done
            //instantiatedObjects[2, 1] = instantiatedObjects[2, 0];


            for (int i = 0; i < 3; i++)
            {
                for (int j = 2; j > 0; j--)
                {
                    instantiatedObjects[i, j] = instantiatedObjects[i, j - 1];
                }
            }

            instantiatedObjects[0, 0] = newLeftTerrain;
            instantiatedObjects[1, 0] = newTerrain;
            instantiatedObjects[2, 0] = newRightTerrain;

        }
    }

    GameObject CreateTerrain(Vector3 pos)
    {
        GameObject objToInst = terrainObjects[Random.Range(0, terrainObjects.Length)];
        GameObject obj = Instantiate(objToInst, pos, Quaternion.identity, transform);
        obj.GetComponent<TerrainObject>().SetManager(this);
        PlaceObjects(obj.GetComponent<Terrain>());
        return obj;
    }

    void PlaceObjects(Terrain terrain)
    {
        for (int i = 0; i < objPerTerrain; i++)
        {
            //Randomize pos on current terrain
            float x = Random.Range(0f, 1000f);
            float y = 200f;
            float z = Random.Range(0f, 1000f);
            Vector3 objPos = terrain.GetPosition() + new Vector3(x, y, z);

            //Raycast down to get point where ground is
            RaycastHit hit;
            if (Physics.SphereCast(objPos, 25f, Vector3.down, out hit))
            {
                objPos = hit.point;
                Quaternion rotation = Random.rotation;
                Instantiate(rnadomObjectsToPlace[Random.Range(0, rnadomObjectsToPlace.Length)], objPos, rotation, terrain.transform);
            }

        }

    }

    void PlaceStationaryObjects(Terrain terrain)
    {
        for (int i = 0; i < stationaryObjPerTerrain; i++)
        {
            //Randomize pos on current terrain
            float x = Random.Range(0f, 1000f);
            float y = 200f;
            float z = Random.Range(0f, 1000f);
            Vector3 objPos = terrain.GetPosition() + new Vector3(x, y, z);

            //Get size of object to place and check if current point is eligable
            //do this later

            //Raycast down to get point where ground is
            RaycastHit hit;
            if (Physics.SphereCast(objPos, 25f, Vector3.down, out hit))
            {
                objPos = hit.point;
                Instantiate(rnadomObjectsToPlace[Random.Range(0, rnadomObjectsToPlace.Length)], objPos, Quaternion.identity, terrain.transform);
            }

        }

    }

}

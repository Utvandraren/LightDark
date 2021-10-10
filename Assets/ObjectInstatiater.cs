using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInstatiater : MonoBehaviour
{
    [SerializeField] private GameObject objToInstantiate;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(objToInstantiate, transform.position, Quaternion.identity);
    }

    
}

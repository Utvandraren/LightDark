using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientTowards : MonoBehaviour
{

    Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.LookAt(target);
    }


}

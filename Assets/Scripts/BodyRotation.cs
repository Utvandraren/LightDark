using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    [SerializeField] GameObject lLeg, rLeg;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 fromRotation = rLeg.transform.position - lLeg.transform.position;
        //transform.rotation = Quaternion.FromToRotation(Vector3.left, fromRotation);
        transform.rotation = (Quaternion.FromToRotation(rLeg.transform.position, lLeg.transform.position));

        //RaycastHit hit;
        ////Vector3 rayOrigin = transform.position;
        //if (Physics.Raycast(transform.position, Vector3.down * 3f, out hit))
        //{
        //   Quaternion newRot = Vector3.RotateTowards = hit.normal;
        //        //hit.normal;
        //}
    }
}

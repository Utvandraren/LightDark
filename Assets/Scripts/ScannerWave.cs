using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerWave : MonoBehaviour
{
    [SerializeField] string tagToUse;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToUse))
        {
            other.GetComponent<HiddenObject>().Reveal();
            //Debug.Log("Object found!!!!!!!!");
        }
    }

    
}

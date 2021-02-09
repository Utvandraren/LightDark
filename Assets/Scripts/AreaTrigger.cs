using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent onEnter;
    [SerializeField] UnityEvent onExit;


    void OnTriggerEnter(Collider other)
    {
        onEnter.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
        onExit.Invoke();
    }

}

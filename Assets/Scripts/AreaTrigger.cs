using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent onEnter;
    [SerializeField] UnityEvent onExit;

    GameObject lastObjectHit;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            lastObjectHit = other.gameObject;
            onEnter.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        onExit.Invoke();
    }

    public void KillObject()
    {
        if (lastObjectHit != null)
        {
            if (lastObjectHit.TryGetComponent(out PlayerStats player))
                player.TakeDamage(99999);
        }
    }

}

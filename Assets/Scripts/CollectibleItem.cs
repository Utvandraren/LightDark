using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractOutline))]
public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private string itemName;

    //void OnTriggerEnter(Collider other)
    //{
    //    Managers.Inventory.AddItem(itemName);
    //    Destroy(gameObject);
    //}

    public void PickUp()
    {
        Managers.Inventory.AddItem(itemName);
        Destroy(gameObject);
    }

}

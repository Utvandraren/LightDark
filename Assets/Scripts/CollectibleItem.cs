using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractOutline))]
public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private ItemObj item;

    //void OnTriggerEnter(Collider other)
    //{
    //    Managers.Inventory.AddItem(itemName);
    //    Destroy(gameObject);
    //}

    public void PickUp()
    {
        Managers.Inventory.AddItem(item);
        Destroy(gameObject);
    }

}

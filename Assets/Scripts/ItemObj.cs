using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "SciptableObjects/item")]
public class ItemObj : ScriptableObject 
{
    public Sprite image;
    public string itemName;
    public string itemDescription;
    public GameObject preFab;

    void OnEnable()
    {
        image = Resources.Load<Sprite>("Icons/key");
    }

    public virtual void Use()
    {
    }

    public virtual GameObject Equip() 
    {
        Transform itemRoot = Managers.Inventory.itemHook;
        GameObject obj = Instantiate(preFab, itemRoot);
        obj.transform.position = itemRoot.position;
        return obj;
    }

    

   
}

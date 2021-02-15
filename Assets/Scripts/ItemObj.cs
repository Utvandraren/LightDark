using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "SciptableObjects/item")]

public class ItemObj : ScriptableObject 
{
    public Sprite image;
    public string itemName;
    public string itemDescription;

    void OnEnable()
    {
        image = Resources.Load<Sprite>("Icons/key");
    }

    public virtual void Use()
    {
    }

    public virtual void equip()
    {
    }
}

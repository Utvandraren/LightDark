using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public ItemObj equippedItem { get; private set; }
    public ItemObj stashedEquippedItem { get; private set; }
    private GameObject instatiatedEquipment;
    public Transform itemHook { get; private set; }

    private Dictionary<ItemObj, int> _items;

    [SerializeField] private List<ItemObj> items;

    public void Startup()
    {
        _items = new Dictionary<ItemObj, int>();
        itemHook = FindObjectOfType<PlayerInputManager>().equipPoint;
        
        status = ManagerStatus.Started;
    }

    //private void DisplayItems()
    //{
    //    string itemDisplay = "Items: ";

    //    foreach (KeyValuePair<string, int> item in _items)
    //    {
    //        itemDisplay += item.Key + "(" + item.Value + ") ";
    //    }

    //    Debug.Log(itemDisplay);
    //}

    public void AddItem(ItemObj name)
    {
        if(_items.ContainsKey(name))
        {
            _items[name] += 1;

        }
        else
        {
            _items[name] = 1;
        }

        //DisplayItems();
    }

    public List<ItemObj> GetItemList()
    {
        List<ItemObj> list = new List<ItemObj>(_items.Keys);
        return list;
    }

    public int GetItemCount(ItemObj name)
    {
        if (_items.ContainsKey(name))
        {
            return _items[name];
        }
        return 0;
    }

    public bool EquipItem(ItemObj name)
    {
        if(_items.ContainsKey(name) && equippedItem != name)
        {
            equippedItem = name;
            instatiatedEquipment = name.Equip();
            return true;
        }
        equippedItem = null;
        return false;
    }

    public void SetEquippedItem(ItemObj name)
    {
        equippedItem = name;
        instatiatedEquipment = name.Equip();
    }

    public void UnEquip()
    {
        if (equippedItem != null)
        {
            stashedEquippedItem = equippedItem;
            Destroy(instatiatedEquipment);
            instatiatedEquipment = null;
            equippedItem = null;
                       
        }
    }

    public void ToogleHolster()
    {
        if(stashedEquippedItem == null)
        {
            UnEquip();
        }
        else
        {
            TakeOutStashedItem();
        }
    }

    void TakeOutStashedItem()
    {
        if(stashedEquippedItem != null)
        {
            equippedItem = stashedEquippedItem;
            instatiatedEquipment = stashedEquippedItem.Equip();
            stashedEquippedItem = null;
        }
    }

    public bool ConsumeItem(ItemObj name)
    {
        if (_items.ContainsKey(name))
        {
            name.Use();
            _items[name]--;
            if (_items[name] == 0)
            {
                _items.Remove(name);
            }
            else
            {
                return false;
            }
        }
        //DisplayItems();
        return true;
    }

//public void useQuippedItem()
//    {
//        if (equippedItem == null)
//            return;


//    }

    public bool ContainsItem(ItemObj name)
    {
        if (_items.ContainsKey(name))
        {
            return true;
        }

        return false;
    }
    
}

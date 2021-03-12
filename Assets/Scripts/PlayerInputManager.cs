using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;


public class PlayerInputManager : MonoBehaviour
{
    public static Action OnOpenInventory;
    [SerializeField] GameObject lightObj;
    public Transform equipPoint;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            OnOpenInventory();
        }
        if (Input.GetButtonDown("Light"))
            lightObj.SetActive(!lightObj.activeSelf);

        if (Input.GetButtonDown("Holster"))
            Managers.Inventory.ToogleHolster();
       

    } 

    
}

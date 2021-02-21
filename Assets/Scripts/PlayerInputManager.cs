using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;


public class PlayerInputManager : MonoBehaviour
{
    public static Action OnOpenInventory;
    [SerializeField] GameObject lightObj;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            OnOpenInventory();
            ECM.Controllers.BaseFirstPersonController obj = GetComponent<ECM.Controllers.BaseFirstPersonController>();
            GetComponent<ECM.Components.MouseLook>().SetCursorLock(!obj.enabled);
            Cursor.visible = obj.enabled;
            GetComponent<ECM.Components.MouseLook>().SetCursorLock(!obj.enabled);
            //GetComponent<ECM.Components.MouseLook>().UpdateCursorLock();
            obj.enabled = !obj.enabled;

            
        }
        if (Input.GetButtonDown("Light"))
            lightObj.SetActive(!lightObj.activeSelf);

       

    } 

    
}

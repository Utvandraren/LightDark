using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[RequireComponent(typeof(InteractOutline))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] bool requireKey;
    [SerializeField] bool onlyInteractOnce = false;

    [SerializeField] ItemObj requiredKeyItem;

    UnityEvent onShowContext;
    [SerializeField] UnityEvent onInteract;

    bool isShowingOutline = true;
    bool hasInteracted = false;

    // Start is called before the first frame update
    void Start()
    {
        //onShowContext = new UnityEvent();
        
        //onShowContext.AddListener(GetComponent<InteractOutline>().ToogleOutline);
    }

    public void ShowContext()
    {
        if (hasInteracted)
            return;

    }

    public void Interact()
    {
        if (requireKey && Managers.Inventory.ContainsItem(requiredKeyItem) == false)
            return;

        if (onlyInteractOnce && hasInteracted)
            return;

        onInteract.Invoke();
        hasInteracted = true;
        if (requireKey)
            Managers.Inventory.ConsumeItem(requiredKeyItem);
    }

    void OnDestroy()
    {
        //onShowContext.RemoveAllListeners();
        onInteract.RemoveAllListeners();
    }

    
}

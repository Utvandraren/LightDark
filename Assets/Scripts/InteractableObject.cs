using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InteractOutline))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] bool requireKey;
    [SerializeField] ItemObj requiredKeyItem;

    UnityEvent onShowContext;
    [SerializeField] UnityEvent onInteract;

    InteractOutline outlineScript;
    bool isShowingOutline = true;

    // Start is called before the first frame update
    void Start()
    {
        onShowContext.AddListener(GetComponent<InteractOutline>().ToogleOutline);
    }

    public void ShowContext()
    {

        onShowContext.Invoke();

    }

    public void Interact()
    {
        if (requireKey && Managers.Inventory.ContainsItem(requiredKeyItem) == false)
            return;
        onInteract.Invoke();
    }

    void OnDestroy()
    {
        onShowContext.RemoveAllListeners();
        onInteract.RemoveAllListeners();
    }

    
}

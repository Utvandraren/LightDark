using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    [SerializeField] LayerMask layerToUse;
    [SerializeField] float checkThickness = 0.1f;
    [SerializeField] float maxRange = 10f;
    [SerializeField] float itemInfoRange = 4f;


    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInteractableObjects();
        ShowNearbyitemsInfo();
    }
    
    void CheckForInteractableObjects()
    {
        RaycastHit hit;
        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        if (Physics.SphereCast(rayOrigin, checkThickness, camera.transform.forward, out hit, maxRange, layerToUse))
        {
            //hit.transform.GetComponent<InteractableObject>().ShowContext();

            if (Input.GetButtonDown("Fire1"))
            {
                if (hit.transform.TryGetComponent<InteractableObject>(out InteractableObject obj))
                    obj.Interact();
                if (hit.transform.TryGetComponent<CollectibleItem>(out CollectibleItem item))
                    item.PickUp();

            }
        }
    }

    void ShowNearbyitemsInfo()
    {
       Collider[] colliders = Physics.OverlapSphere(transform.position, itemInfoRange, layerToUse);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.TryGetComponent<InteractableObject>(out InteractableObject obj))
                obj.ShowContext();
            
            //colliders[i].transform.GetComponent<InteractableObject>().ShowContext();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, itemInfoRange);
    }


}

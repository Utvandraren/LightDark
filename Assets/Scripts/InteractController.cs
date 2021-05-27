using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    [SerializeField] LayerMask layerToUse;
    [SerializeField] float checkThickness = 0.1f;
    [SerializeField] float maxRange = 10f;
    [SerializeField] float itemInfoRange = 4f;
    [SerializeField] Transform movePoint;
    [SerializeField] float throwingForce = 60f;
    [SerializeField] AudioClip pickupSoundClip;

    InteractableObject currentobj;
    Camera camera;
    AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        source = GetComponent<AudioSource>();

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

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.SphereCast(rayOrigin, checkThickness, camera.transform.forward, out hit, maxRange, layerToUse))
            {
                if (hit.transform.TryGetComponent<InteractableObject>(out InteractableObject obj))
                    obj.Interact();
                if (hit.transform.TryGetComponent<CollectibleItem>(out CollectibleItem item))
                {
                    item.PickUp();
                    source.PlayOneShot(pickupSoundClip);
                }
                if (hit.transform.TryGetComponent<PuzzleManager>(out PuzzleManager puzzle))
                    puzzle.EnterPuzzle();
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (Managers.Inventory.equippedItem != null)
                return;
            if (Physics.SphereCast(rayOrigin, checkThickness, camera.transform.forward, out hit, maxRange, layerToUse))
            {
                if (hit.transform.TryGetComponent<InteractableObject>(out InteractableObject obj))
                {
                    PickUpObj(obj);
                }
            }
        }
   
        if (Input.GetButtonUp("Fire2"))
        {
            DropObj();
        }

        if (Input.GetButtonDown("Fire1") && currentobj != null)
        {
            ThrowObj();
        }
    }
    
    void PickUpObj(InteractableObject obj)
    {
        obj.transform.position = movePoint.position;
        currentobj = obj;
        obj.transform.SetParent(movePoint);
        obj.GetComponent<Rigidbody>().isKinematic = true;
    }

    void DropObj()
    {
        currentobj.GetComponent<Rigidbody>().isKinematic = false;
        movePoint.DetachChildren();
        currentobj = null;
    }
    
    void ThrowObj()
    {
        currentobj.GetComponent<Rigidbody>().isKinematic = false;
        movePoint.DetachChildren();
        currentobj.GetComponent<Rigidbody>().AddForce(movePoint.forward * throwingForce, ForceMode.Impulse);
        currentobj = null;

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
        //Gizmos.DrawWireSphere(transform.position, itemInfoRange);
        //Gizmos.DrawLine(camera.transform.position, camera.transform.position + Vector3.forward);
    }


}

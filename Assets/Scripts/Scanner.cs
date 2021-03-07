using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] GameObject scannerPrefab;
    [SerializeField] float detectedAreaRange = 20f;

    List<GameObject> hiddenObjsNearby;
    Animator animator;
    Light lightComponent;
    float lightStartIntensity;

    // Start is called before the first frame update
    void Start()
    {
        hiddenObjsNearby = new List<GameObject>();
        animator = GetComponent<Animator>();
        lightComponent = GetComponentInChildren<Light>();
        lightStartIntensity = lightComponent.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject obj = Instantiate(scannerPrefab, transform.position, transform.rotation);
            //obj.GetComponent<Projectile>().SetDirection();
        }
        UpdateAnimationSpeed();
    }

    void UpdateAnimationSpeed()
    {
        if (hiddenObjsNearby.Count == 0)
        {
            animator.SetFloat("PulsePower", 0f);
            lightComponent.intensity = lightStartIntensity;
            return;
        }
        else
        {
            float value = 1f - (GetClosestDistHiddenObj() / detectedAreaRange);
            animator.SetFloat("PulsePower", value);
            lightComponent.intensity = Mathf.Pow(value / 5f, 2f);
        }
    }

    float GetClosestDistHiddenObj()
    {
        float closestDistance = 1000f;
        foreach (GameObject obj in hiddenObjsNearby)
        {
            if(Vector3.Distance(obj.transform.position, transform.position) < closestDistance)
            {
                closestDistance = Vector3.Distance(obj.transform.position, transform.position);
            }
        }
        return closestDistance;
    }

    public void UpdateList()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, detectedAreaRange);
        hiddenObjsNearby.Clear();
        for (int i = 0; i < coll.Length; i++)
        {
            if (coll[i].CompareTag("Hidden"))
                hiddenObjsNearby.Add(coll[i].gameObject);
        }
    }

    
}

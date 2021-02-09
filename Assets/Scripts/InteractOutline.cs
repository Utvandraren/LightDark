using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class InteractOutline : MonoBehaviour
{
     GameObject outlineObj;
    [SerializeField] GameObject textPrompt;
    Material outlineMaterial;
    [SerializeField] float timerOutline = 1f;

    GameObject InstantOutline;

    float coolDown = 0f;

    void Awake()
    {
        outlineObj = Instantiate(new GameObject(), transform);
        outlineObj.transform.localScale *= 1.1f;
        if (textPrompt != null)
            Instantiate(textPrompt, outlineObj.transform);
        outlineObj.AddComponent<MeshFilter>();
        outlineObj.AddComponent<MeshRenderer>();
        outlineObj.GetComponent<MeshFilter>().mesh = GetComponent<MeshFilter>().mesh;
        //outlineObj.GetComponent<MeshRenderer>().material = outlineMaterial;
        outlineObj.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/OutlineMaterial");

        outlineObj.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
       

    }

    void Update()
    {
        coolDown -= Time.deltaTime;
        coolDown = Mathf.Clamp(coolDown, 0f, timerOutline);

        if(coolDown <= 0f)
        {
            outlineObj.SetActive(false);
        }
    }

    public void ToogleOutline()
    {
        if (outlineObj.activeSelf == true)
        {
            coolDown = timerOutline;
            return;
        }

        outlineObj.SetActive(true);
        coolDown = timerOutline;
        //StartCoroutine(TimerContext());
    }

    

    IEnumerator TimerContext()
    {
        yield return new WaitForSeconds(1f);
        outlineObj.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegCreature : MonoBehaviour
{
    AIMovement AiMov;

    void Start()
    {
        AiMov = GetComponent<AIMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightSource"))
            FrightenCreature();

    }

    void FrightenCreature()
    {
        //Debug.Log("Create has seen the light!");
        AiMov.MoveAwayFromTarget();
    }
}

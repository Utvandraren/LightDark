using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PuzzlePiece : MonoBehaviour
{

   
    public int nmbr = 1; //Has to be public

    float deltaForce;
    ParticleSystem particle;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particle = GetComponentInChildren<ParticleSystem>();
        particle.gameObject.SetActive(false);
    }

    



    public void FitInPlace()
    {
        particle.gameObject.SetActive(true);
        rb.isKinematic = true;
    }

}
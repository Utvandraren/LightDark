using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PuzzlePiece : MonoBehaviour
{
    public int nmbr = 1; //Has to be public

    float deltaForce;
    Vector3 mouseDelta;
    Vector3 lastMousePos;
    Rigidbody rb;

    ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particle = GetComponentInChildren<ParticleSystem>();
        particle.gameObject.SetActive(false);
    }

    void Update()
    {
        mouseDelta = Input.mousePosition - lastMousePos;
        lastMousePos = Input.mousePosition;



    }


    void OnMouseDrag() //called continuously while the button is pressed down every frame -----needs to fix the Algo to use all directions
    {
        //Vector3 dir = Camera.main.ScreenToWorldPoint(mouseDelta);

        rb.AddForce(mouseDelta * Time.deltaTime);

    }

    public void FitInPlace()
    {
        particle.gameObject.SetActive(true);
        rb.isKinematic = true;
    }

}
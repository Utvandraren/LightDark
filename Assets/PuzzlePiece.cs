using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzlePiece : MonoBehaviour
{
    public int nmbr = 1;

    float deltaForce;
    Vector3 mouseDelta;
    Vector3 lastMousePos;
    Rigidbody rb;

    [SerializeField] UnityEvent onFitInPlace;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        mouseDelta = Input.mousePosition - lastMousePos;
        deltaForce = Vector3.Distance(lastMousePos, Input.mousePosition);
        lastMousePos = Input.mousePosition;
    }

    void OnMouseDrag() //called continuously while the button is pressed dowwn every frame
    {
        rb.AddForce(mouseDelta);
    }

    public void FitInPlace()
    {
        rb.isKinematic = true;
        onFitInPlace.Invoke();
    }

}

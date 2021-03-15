using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] float moveStrength;
    [SerializeField] Direction moveDirection = Direction.X;

    enum Direction
    {
        X,
        Y
    };
    public int nmbr = 1; //Has to be public

    float deltaForce;
    Vector3 mouseDelta;
    Vector3 lastMousePos;
    Vector3 offset;
    Rigidbody rb;
    Transform parentTransform;
    ParticleSystem particle;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particle = GetComponentInChildren<ParticleSystem>();
        particle.gameObject.SetActive(false);
        moveStrength = 2f;
        parentTransform = GetComponentInParent<PuzzleManager>().transform;
        lastMousePos = Input.mousePosition;
    }

    void Update()
    {
        mouseDelta = Input.mousePosition - lastMousePos;
        lastMousePos = Input.mousePosition;

    }

    void OnMouseDrag() //called continuously while the button is pressed down every frame -----needs to fix the Algo to use all directions
    {
        Vector3 dir = parentTransform.position;
        Vector3 newdir = Vector3.zero;

        switch (moveDirection)
        {
            case Direction.X:
                float changeX = -mouseDelta.x;
                 newdir = new Vector3(dir.x * changeX, 0f, 0f);
                break;

            case Direction.Y:
                float changeY = mouseDelta.y * 10f;
                 newdir = new Vector3(0f, dir.y * changeY, 0f);
                break;
        }

        dir = parentTransform.TransformDirection(newdir);
        rb.AddForce(dir * Time.deltaTime * moveStrength);
    }



    public void FitInPlace()
    {
        particle.gameObject.SetActive(true);
        rb.isKinematic = true;
    }

}
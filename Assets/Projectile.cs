using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;

    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {    
        rigidBody = GetComponent<Rigidbody>();
    }

    public void SetDirection(Vector3 direction)
    {
        rigidBody.velocity = direction * speed;
    }

}

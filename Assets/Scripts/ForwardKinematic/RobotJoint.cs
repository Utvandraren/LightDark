using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotJoint : MonoBehaviour
{
    public Vector3 axis;
    public Vector3 startOffSet;

    public float minAngle;
    public float maxAngle;

    public char axisChar;

    void Awake()
    {
        startOffSet = transform.localPosition;

        if (axis == Vector3.right)   //X
        {
            axisChar = 'x';
        }
        else if (axis == Vector3.up)     //Y
        {
            axisChar = 'y';
        }
        else if (axis == Vector3.forward)   //Z
        {
            axisChar = 'z';
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

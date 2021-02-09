using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    public RobotJoint[] joints;
    public float samplingDistance = 5f;
    public float learningRate = 100f;
    public float distanceTreshold = 0.01f;

    public Transform targetTransform;

    float[] angles;

    // Start is called before the first frame update
    void Start()
    {
        joints = GetComponentsInChildren<RobotJoint>();

        angles = new float[joints.Length];

        for (int i = 0; i < joints.Length; i++)
        {
            if (joints[i].axis == Vector3.right)   //X
            {
                angles[i] = joints[i].transform.localRotation.eulerAngles.x;
            }
            else if (joints[i].axis == Vector3.up)     //Y
            {
                angles[i] = joints[i].transform.localRotation.eulerAngles.y;
            }
            else if (joints[i].axis == Vector3.forward)   //Z
            {
                angles[i] = joints[i].transform.localRotation.eulerAngles.z;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //modelshowing.position = ForwardKinematics(angles);
        InverseKinematics(targetTransform.position, angles);
    }

    public Vector3 ForwardKinematics(float[] angles)
    {
        Vector3 prevPoint = joints[0].transform.position;
        Quaternion rotation = Quaternion.identity;
        for (int i = 1; i < joints.Length; i++)
        {
            // Rotates around a new axis
            rotation *= Quaternion.AngleAxis(angles[i - 1], joints[i - 1].axis);
            Vector3 nextPoint = prevPoint + rotation * joints[i].startOffSet;

            prevPoint = nextPoint;
        }
        return prevPoint;
    }

    public float DistanceFromTarget(Vector3 target, float[] angles)
    {
        Vector3 point = ForwardKinematics(angles);
        return Vector3.Distance(point, target);
    }

    public float PartialGradient(Vector3 target, float[] angles, int i)
    {
        // Saves the angle,
        // it will be restored later
        float angle = angles[i];

        // Gradient : [F(x+SamplingDistance) - F(x)] / h
        float f_x = DistanceFromTarget(target, angles);

        angles[i] += samplingDistance;
        float f_x_plus_d = DistanceFromTarget(target, angles);

        float gradient = (f_x_plus_d - f_x) / samplingDistance;

        // Restores
        angles[i] = angle;

        return gradient;
    }

    //public void InverseKinematics(Vector3 target, float[] angles)
    //{
    //    if (DistanceFromTarget(target, angles) < distanceTreshold)
    //        return;

    //    for (int i = joints.Length - 1; i >= 0; i--)
    //    {
    //        // Gradient descent
    //        // Update : Solution -= LearningRate * Gradient
    //        float gradient = PartialGradient(target, angles, i);
    //        angles[i] -= learningRate * gradient;

    //        // Clamp
    //        angles[i] = Mathf.Clamp(angles[i], joints[i].minAngle, joints[i].maxAngle);

    //        // Early termination
    //        if (DistanceFromTarget(target, angles) < distanceTreshold)
    //            return;
    //    }
    //}

    public void InverseKinematics(Vector3 target, float[] angles)
    {
        if (DistanceFromTarget(target, angles) < distanceTreshold)
            return;

        for (int i = joints.Length - 1; i >= 0; i--)
        {
            // Gradient descent
            // Update : Solution -= LearningRate * Gradient
            float gradient = PartialGradient(target, angles, i);
            angles[i] -= learningRate * gradient;

            // Clamp
            angles[i] = Mathf.Clamp(angles[i], joints[i].minAngle, joints[i].maxAngle);

            // Early termination
            if (DistanceFromTarget(target, angles) < distanceTreshold)
                return;

            switch (joints[i].axisChar)
            {
                case 'x':
                    joints[i].transform.localEulerAngles = new Vector3(angles[i], 0, 0);
                    break;
                case 'y':
                    joints[i].transform.localEulerAngles = new Vector3(0, angles[i], 0);
                    break;
                case 'z':
                    joints[i].transform.localEulerAngles = new Vector3(0, 0, angles[i]);
                    break;
            }
        }


       

           
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraEffects : MonoBehaviour
{
    private CinemachineImpulseSource source;

    void Start()
    {
        source = GetComponent<CinemachineImpulseSource>();
    }
    public void ShakeCamera()
    {
        source.GenerateImpulse();
        Debug.Log("Pulse generated");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float dayCycleVelocity = 1f;
    Light sunSource;

    public bool isNight = false;

    // Start is called before the first frame update
    void Start()
    {
        sunSource = GetComponentInChildren<Light>();

    }

    // Update is called once per frame
    void Update()
    {
        HandleDayCycle();

    }

    void HandleDayCycle()
    {
        float angleX = sunSource.transform.rotation.eulerAngles.x;
        sunSource.transform.Rotate(Vector3.right, dayCycleVelocity * Time.deltaTime);

        if (angleX >= 200f)
        {
            Debug.Log("Nighty Nighty");
        }

    }
}

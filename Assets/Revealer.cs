using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revealer : MonoBehaviour
{

    [SerializeField] protected float timeBetweenAttacks = 2f;

    Animator animator;
    AudioSource source;
    protected float currentTime;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentTime <= 0f)
            {
                source.Play();
                animator.SetBool("isScanning", true);
                currentTime = timeBetweenAttacks;
            }
        }
        if(Input.GetButtonUp("Fire1"))
        {
            source.Stop();
            animator.SetBool("isScanning", false);
        }
        currentTime -= Time.deltaTime;
        Mathf.Clamp(currentTime, 0f, timeBetweenAttacks);

    }
}

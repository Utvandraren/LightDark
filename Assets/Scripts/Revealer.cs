using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Revealer : MonoBehaviour
{
    [SerializeField] private float timeBetweenAttacks = 2f;
    [SerializeField] private AudioClip alarmSoundClip;
    [SerializeField] private TextMeshPro textIndicator;

    AudioSource source;
    Animator animator;
    protected float currentTime;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
        //textIndicator = GetComponentInChildren<TextMeshPro>();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hidden"))
        {
            //source.PlayOneShot(alarmSoundClip);
            HiddenObject objFound = other.GetComponent<HiddenObject>();
            UpdateText(objFound.scannerInfo);
        }

    }

    void UpdateText(string newText)
    {
        textIndicator.SetText(newText);
    }

}

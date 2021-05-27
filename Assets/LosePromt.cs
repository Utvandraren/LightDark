using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePromt : MonoBehaviour
{
    AudioClip loseDeathEffect;

    void OnEnable()
    {
        AudioSource.PlayClipAtPoint(loseDeathEffect, transform.position);
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
            Managers.Level.RestartLevel();
            //GetComponent<UIController>().CloseLoseUI();

    }
}

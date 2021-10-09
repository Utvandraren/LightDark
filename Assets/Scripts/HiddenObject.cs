using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HiddenObject : MonoBehaviour
{
    [SerializeField] bool isRevealedPermanet = false;
    [SerializeField] float timeRevealed = 0.04f;
    [SerializeField] UnityEvent onReveal;
    [SerializeField] UnityEvent onHide;
    [SerializeField] UnityEvent onFirstTimeRevealed;


    bool firstTimeRevealed = true;

    public void Reveal()
    {
        onReveal.Invoke();
        if(firstTimeRevealed)
        {
            firstTimeRevealed = false;
            onFirstTimeRevealed.Invoke();
        }
        if (!isRevealedPermanet)
            StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeRevealed);
        onHide.Invoke();
    }
}

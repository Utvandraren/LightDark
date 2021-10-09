using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HiddenObject : MonoBehaviour
{
    [SerializeField] bool isRevealedPermanet = false;
    [SerializeField] float timeRevealed = 0.04f;
    public string scannerInfo = "Broken thread found";

    [SerializeField] UnityEvent onReveal;
    [SerializeField] UnityEvent onHide;
    [SerializeField] UnityEvent onFirstTimeRevealed;

    bool firstTimeRevealed = true;
    bool currentlyRevealed = false;

    public void Reveal()
    {
        if (currentlyRevealed)
            return;
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
        currentlyRevealed = true;
        yield return new WaitForSeconds(timeRevealed);
        onHide.Invoke();
        currentlyRevealed = false;
    }
}

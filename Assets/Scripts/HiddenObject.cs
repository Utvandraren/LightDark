using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HiddenObject : MonoBehaviour
{
    [SerializeField] bool isRevealedPermanet = false;
    [SerializeField] float timeRevealed = 15f;
    [SerializeField] UnityEvent onReveal;
    [SerializeField] UnityEvent onHide;


    public void Reveal()
    {
        onReveal.Invoke();
        if (isRevealedPermanet)
            StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeRevealed);
        onHide.Invoke();
    }
}

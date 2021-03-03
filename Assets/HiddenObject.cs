using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HiddenObject : MonoBehaviour
{
    [SerializeField] UnityEvent onReveal;

    public void Reveal()
    {
        onReveal.Invoke();
    }
}

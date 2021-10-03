using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundPlayer : MonoBehaviour
{
    AudioSource source;

    void Start()
    {
        TryGetComponent(out source);
    }

    public void PlaySoundClip(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}

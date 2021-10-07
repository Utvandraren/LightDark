using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private AudioClip collectSoundClip;
    [SerializeField] private int amountToIncrease = 10;

    private AudioSource source;
    private ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    CapsuleCollider playerCollider;


    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        source = GetComponent<AudioSource>();
        playerCollider = Managers.Player.playerObj.GetComponentInParent<CapsuleCollider>();
        ps.trigger.SetCollider(0, playerCollider);
    }


    void OnParticleTrigger()
    {
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        for (int i = 0; i < numEnter; i++)
        {
            resourceCollected();
        }
    }

    public void resourceCollected()
    {
        source.pitch = Random.Range(0.99f, 1.01f);
        source.PlayOneShot(collectSoundClip, Random.Range(0.99f, 1.01f));
        Managers.Player.energy += amountToIncrease;
    }
}

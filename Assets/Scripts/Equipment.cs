using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [Header("GuardianLight")]
    [SerializeField] GameObject guardianLight;
    [SerializeField] int energyUsePerSecond;
    [SerializeField] AudioClip energyFailClip;

    AudioSource source;


    PlayerStats stats;
    bool LightOn = false;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        source = GetComponent<AudioSource>();
    }

    public void ToogleLight()
    {
        if (stats.energy < energyUsePerSecond)
        {
            source.PlayOneShot(energyFailClip);
            return;
        }

        guardianLight.SetActive(!guardianLight.activeSelf);
        if (guardianLight.activeSelf)
            StartCoroutine("UseEnergy");
        else
            LightOn = false;
        
    }

    IEnumerator UseEnergy()
    {
        LightOn = true;
        while (LightOn)
        {
            yield return new WaitForSeconds(1f);
            stats.ConsumeEnergy(energyUsePerSecond);
            if (stats.energy < energyUsePerSecond)
            {
                source.PlayOneShot(energyFailClip);
                guardianLight.SetActive(false);
                LightOn = false;
            }
        }       
    }
}

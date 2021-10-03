using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class DraggingCannon : MonoBehaviour
{
    LineRenderer line;
    [SerializeField] AudioSource source;
    [SerializeField] VisualEffect explosionFX;
    [SerializeField] AudioClip laserShootSound;
    [SerializeField] AudioClip LaserStartClip;
    [SerializeField] AudioClip LaserEndClip;
    [SerializeField] Light lightSource;
    [SerializeField] GameObject postProcessingVolume;



    // Start is called before the first frame update
    void Start()
    {
        //TryGetComponent(out source);
        line = FindObjectOfType<LineRenderer>();
    }


    public void StartLaser()
    {
        StartCoroutine("ShootLaser");
    }

    IEnumerator ShootLaser()
    {
        line.SetPosition(1, new Vector3(line.transform.position.x * 100f, line.transform.position.y, line.transform.position.z));
        source.PlayOneShot(LaserStartClip);
        source.PlayOneShot(laserShootSound);
        float oldIntensity = lightSource.intensity;
        lightSource.intensity *= 100f;
        explosionFX.Play();
        postProcessingVolume.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        source.Stop();
        postProcessingVolume.SetActive(false);
        lightSource.intensity = oldIntensity;
        source.PlayOneShot(LaserEndClip);
        line.SetPosition(1, Vector3.zero);

    }
}

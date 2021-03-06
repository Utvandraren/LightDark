using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] GameObject scannerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject obj = Instantiate(scannerPrefab, transform.position, transform.rotation);
            //obj.GetComponent<Projectile>().SetDirection();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    Vector3 aterissage;
    public float vitesse = 10;
    public float timeurExplose;

    bool once;

    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        once = false;
        timeurExplose = 0f;
        aterissage = transform.position;
        aterissage.z += 27;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z< aterissage.z)
        {
            transform.position += Vector3.forward * Time.deltaTime * vitesse;
            Debug.Log(Vector3.forward * Time.deltaTime * vitesse);
        }
        else
        {
            timeurExplose += Time.deltaTime;

        }

        if (timeurExplose > 3f)
        {
            if (!once)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                once = true;

            }
            

        }
    }
}

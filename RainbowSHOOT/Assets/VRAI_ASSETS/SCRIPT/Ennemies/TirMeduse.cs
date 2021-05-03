using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirMeduse : MonoBehaviour
{
    Vector3 dest;
    Vector3 pos;
    float mid;
    public float vitesse;
    // Start is called before the first frame update
    void Start()
    {
        dest = GameObject.FindObjectOfType<SpaceShip>().transform.position;
        mid = (transform.position.z + dest.z) / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        if (pos.z > mid)
        {
            pos.y += Time.deltaTime * vitesse;
            
        }else
        {
            pos.y -= Time.deltaTime * vitesse;
        }
        pos.z -= Time.deltaTime * vitesse;
        transform.position = pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Vector3 scale;
    public Vector3 vitesse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Alien")
        {
            collision.gameObject.GetComponent<Alien>().Explosion();
        }
        if (collision.gameObject.tag == "Grenade")
        {
            Destroy(collision.gameObject);
        }
    }
    public void Destruct()
    {
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS : MonoBehaviour
{
    public float vie;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TirVaisseau")
        {
            vie -= 12;
            if (vie < 0)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                FindObjectOfType<WaveManager>().ennemies -= 1;
            }
        }
    }


    // Start is called before the first frame update

}

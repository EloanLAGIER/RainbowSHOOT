using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS : MonoBehaviour
{
    public GameObject hit;
    public float vie;
    public AudioSource tirB;
    public AudioSource touche;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TirVaisseau")
        {
            if (vie > 0)
            {
                touche.Play();
                Instantiate(hit, collision.transform.position, Quaternion.identity);
                vie -= 12;
                if (vie < 0)
                {
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    FindObjectOfType<WaveManager>().ennemies -= 1;
                }
            }
        }
    }


    // Start is called before the first frame update

}

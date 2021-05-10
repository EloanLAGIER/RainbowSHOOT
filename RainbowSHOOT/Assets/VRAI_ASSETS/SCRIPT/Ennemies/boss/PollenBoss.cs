using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenBoss : MonoBehaviour
{
    Vector3 position;
    public int etage;
    public int sens;
    public float vitesse;
    
    public int vie;

    public GameObject tir;
    public float timeur;

    public bool divise;

    public GameObject hit;
    public AudioSource tirB;
    public AudioSource touche;
    // Start is called before the first frame update
    void Start()
    {
        timeur = Random.Range(0f, 4f);

    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        position.x += Time.deltaTime * sens * vitesse;
        if (position.x > 8f){
            sens = -1;
        }
        if (position.x < -8f)
        {
            sens = 1;
        }
        transform.position = position;

        timeur += Time.deltaTime;
        if (timeur > 5f)
        {

            Instantiate(tir, transform.position, Quaternion.identity);
            timeur = 0f;
        }

        if (transform.position.y < -50f)
        {
            Destroy(this.gameObject);
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TirVaisseau")
        {
            Destroy(collision.gameObject);
            vie -= 12;
            if (vie < 0)
            {
                Instantiate(hit, collision.transform.position, Quaternion.identity);
                touche.Play();
                if ((etage > 0) && (!divise))
                {
                    divise = true;
                    vie = 100;
                    GameObject b = Instantiate(this.gameObject, transform.position, Quaternion.identity);
                    b.GetComponent<PollenBoss>().divise = false;
                    b.transform.localScale = transform.localScale * 0.75f;
                    b.GetComponent<PollenBoss>().etage -= 1;
                    
                    b.GetComponent<PollenBoss>().vitesse += 2f;
                    b = Instantiate(b, transform.position, Quaternion.identity);
                    b.GetComponent<PollenBoss>().sens = -b.GetComponent<PollenBoss>().sens;
                    vie = -1;
                }
                else {
                    FindObjectOfType<WaveManager>().ennemies -= 1;
                }
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                FindObjectOfType<WaveManager>().ennemies -= 1;
            }
        }
    }
}

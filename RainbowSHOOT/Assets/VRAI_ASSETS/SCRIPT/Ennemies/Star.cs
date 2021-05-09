using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Alien
{
    public AudioSource hits;
    public AudioSource shoot;
    public float timeur; 
    // Start is called before the first frame update
    void Start()
    {
        timeur = -1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (life > 0)
        {
            
            position = transform.position;

            if (transform.position.y != 0f)
            {
                position.y -= Time.deltaTime* vitesse;

                if (transform.position.y < 0f)
                {
                    position.y = 0f;
                }
                transform.position = position;
                return;
            }
            timeur += Time.deltaTime;
            if (timeur > .7f)
            {
                TirBoule();
                timeur = 0f;
            }
            distanceTravelled += vitesse * Time.deltaTime;

            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        }
        else
        {
            if (transform.position.y <= -50)
            {
                Destroy(this.gameObject);
            }

        }


    }


    public void TirBoule()
    {
        shoot.Play();
        Instantiate(tir, transform.position, Quaternion.identity);
    }


    void OnCollisionEnter(Collision c)
    {
        if (life > 0)
        {
            if (c.gameObject.tag == "TirVaisseau")
            {
                hits.Play();
                GameObject g = Instantiate(hit, transform.position, Quaternion.identity);
                StartCoroutine(DestroyHit(g));
                life -= c.gameObject.GetComponent<TirVaisseau>().valeur;
                Scorehit();
                Destroy(c.gameObject);
                if (life <= 0)
                {
                    int rando = Random.Range(0, 10);
                    if (rando == 5)
                    {
                        Instantiate(dropGrenade, transform.position, Quaternion.identity);
                    }

                    GameObject.Find("WaveManager").GetComponent<WaveManager>().ennemies -= 1;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    GetComponentInChildren<Animator>().enabled = false;
                }
            }
        }
    }
}

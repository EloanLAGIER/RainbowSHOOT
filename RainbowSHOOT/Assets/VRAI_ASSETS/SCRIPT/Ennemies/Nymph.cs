using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nymph : Alien
{
    public Animator anim;

    public AudioSource hitSound;
    public AudioSource shootSound;
    public float lasthit;
    // Start is called before the first frame update
    void Start()
    {
        lasthit = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life > 0)
        {
            position = transform.position;


            if (transform.position.y != -3f)
            {
                position.y -= Time.deltaTime;

                if (transform.position.y < -3f)
                {
                    position.y = -3f;
                }
                transform.position = position;
                return;
            }



            distanceTravelled += vitesse * Time.deltaTime;
            position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            position.y = -3;
            transform.position =position;
        }
        else {
            if (transform.position.y <= -50)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if (life > 0)
        {
            if (c.gameObject.tag == "TirVaisseau")
            {
                hitSound.Play();

                if (Time.time > lasthit + 5f)
                {

                    
                    GameObject g = Instantiate(hit, transform.position, Quaternion.identity);
                    StartCoroutine(DestroyHit(g));
                    lasthit = Time.time;
                    life -= c.gameObject.GetComponent<TirVaisseau>().valeur;
                    Scorehit();
                    Destroy(c.gameObject);

                    anim.SetTrigger("hit");
                    int rand = Random.Range(3, 7);
                    shootSound.Play();
                    for (int i = 0; i <= rand; i++)
                    {
                        Instantiate(tir, transform.position, Quaternion.identity);
                    }
                }
                if (life <= 0)
                {
                    int rando = Random.Range(0, 10);
                    if (rando == 5)
                    {
                       
                        Instantiate(dropGrenade, transform.position, Quaternion.identity);
                    }

                    GameObject.Find("WaveManager").GetComponent<WaveManager>().ennemies -= 1;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
            }
        }
    }

}

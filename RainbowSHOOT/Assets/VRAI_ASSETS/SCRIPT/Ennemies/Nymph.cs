using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nymph : Alien
{
    public Animator anim;

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


            if (transform.position.y != 0f)
            {
                position.y -= Time.deltaTime;

                if (transform.position.y < 0f)
                {
                    position.y = 0f;
                }
                transform.position = position;
                return;
            }



            distanceTravelled += vitesse * Time.deltaTime;

            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
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
                if (Time.time > lasthit + 2)
                {
                    lasthit = Time.time;
                    life -= c.gameObject.GetComponent<TirVaisseau>().valeur;
                    Scorehit();
                    Destroy(c.gameObject);

                    anim.SetTrigger("hit");
                    int rand = Random.Range(5, 15);
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

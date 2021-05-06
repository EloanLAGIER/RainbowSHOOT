using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : Alien
{


    public float tempsperso;



    public float tempspersoUp;


    // Start is called before the first frame update
    void Start()
    {
        tempsperso = Random.Range(4, 6f);
        tempspersoUp = 0f;
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

            tempspersoUp += Time.deltaTime;
            if (tempspersoUp >= tempsperso)
            {

                tempspersoUp = 0f;

                Instantiate(tir, transform.position, Quaternion.identity);

            }

        }
        else
        {
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
                }
            }
        }
    }

}

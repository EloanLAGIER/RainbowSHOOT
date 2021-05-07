using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Alien
{

    public bool protect;
    // Start is called before the first frame update
    void Start()
    {
        protect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (life < 0)
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

    public void ChangeProtect()
    {
        protect = !protect;
        if (!protect)
        {
            Instantiate(tir, transform.position, Quaternion.identity);
        }
    }


    void OnCollisionEnter(Collision c)
    {
        if (life > 0)
        {
            if (c.gameObject.tag == "TirVaisseau")
            {
                if (!protect)
                {
                    GameObject g = Instantiate(hit, transform.position, Quaternion.identity);
                    Invoke("DestroyHit(g)", 5f);
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

}

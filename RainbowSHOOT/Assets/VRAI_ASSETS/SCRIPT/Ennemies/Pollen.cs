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

        if (life <= 0)
        {
            //GameObject.Find("UIManager").GetComponent<UIManager>().score += Random.Range(100, 200);

            //GameObject.Find("AUDIOMANAGER").GetComponent<AudioSource>().Play();
            GameObject.Find("WaveManager").GetComponent<WaveManager>().ennemies -= 1;
            Destroy(this.gameObject);
        }


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



    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "TirVaisseau")
        {

            life -= 12;
            Scorehit();
            Destroy(c.gameObject);
        }
    }

}

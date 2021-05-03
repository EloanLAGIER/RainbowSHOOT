using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;


public class Meduse : Alien
{





    public Animator anim;


    public float tempsperso;



    public float tempspersoUp;
    // Start is called before the first frame update
    void Start()
    {
        life = 12;
        vitesse = 1f;
        tempsperso = Random.Range(4, 6f);
        tempspersoUp = 0f;
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
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


        tempspersoUp += Time.deltaTime;
        if (tempspersoUp >= tempsperso)
        {
            anim.SetTrigger("shoot");
            tempspersoUp = 0f;
        }
        

        distanceTravelled += vitesse * Time.deltaTime;

        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);


        if (life <= 0)
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().score += Random.Range(100, 200);

            GameObject.Find("AUDIOMANAGER").GetComponent<AudioSource>().Play();
            GameObject.Find("WaveManager").GetComponent<WaveManager>().ennemies -= 1;
            Destroy(this.gameObject);
        }

    }

    public void TirBoule()
    {
        Instantiate(tir, transform.position, Quaternion.identity);
    }


}

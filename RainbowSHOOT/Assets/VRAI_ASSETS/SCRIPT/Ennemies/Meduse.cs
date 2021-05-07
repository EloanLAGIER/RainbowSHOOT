using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;


public class Meduse : Alien
{
    public Quaternion rotation;
    private Vector3 from;
    private Vector3 to;
    private float timeCount;

    public float dureeRotate;
    public float currentRotate;
    public bool rotate;

    public Animator anim;


    public float tempsperso;



    public float tempspersoUp;
    // Start is called before the first frame update
    void Start()
    {
        from = new Vector3(90f, 0, 0);
        from = new Vector3(180f, 0, 0);
        timeCount = 0f;
        rotate = false;
        life = 12;
        vitesse = 1f;
        tempsperso = Random.Range(4, 6f);
        tempspersoUp = 0f;
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


            tempspersoUp += Time.deltaTime;
            if (tempspersoUp >= tempsperso)
            {
                anim.SetTrigger("shoot");
                tempspersoUp = 0f;
                rotate = true;
            }


            distanceTravelled += vitesse * Time.deltaTime;

            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);

            if (rotate)
            {
                currentRotate += Time.deltaTime;
                if (currentRotate < dureeRotate / 2)
                {
                    transform.Rotate(200f * Time.deltaTime, 0f, 0f, Space.Self);



                }
                else
                {
                    transform.Rotate(-200f * Time.deltaTime, 0f, 0f, Space.Self);


                }

            }
            if (currentRotate > dureeRotate)
            {
                rotate = false;

                currentRotate = 0f;
            }
        }
        else {
            if (transform.position.y <= -50)
            {
                Destroy(this.gameObject);
            }
            position = transform.position;
        }
       

    }

    public void TirBoule()
    {
        Instantiate(tir, transform.position, Quaternion.identity);
    }

    void OnCollisionEnter(Collision c)
    {
        if (life > 0)
        {
            if (c.gameObject.tag == "TirVaisseau")
            {
                Instantiate(hit, transform.position, Quaternion.identity);
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
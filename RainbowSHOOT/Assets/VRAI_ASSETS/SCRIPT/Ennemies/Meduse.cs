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

    public AudioSource hitSound;
    public AudioSource tirB;

    public float tempspersoUp;
    // Start is called before the first frame update
    void Start()
    {
        from = new Vector3(90f, 0, 0);
        from = new Vector3(180f, 0, 0);
        timeCount = 0f;
        rotate = false;

        tempsperso = Random.Range(1.5f, 3f);
        tempspersoUp = 0f;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life > 0)
        {
            position = transform.position;


            if (transform.position.y != 0)
            {
                position.y -= Time.deltaTime * vitesse;

                if (transform.position.y < 0)
                {
                    position.y = 0;
                }
                transform.position = position;
                return;
            }


            tempspersoUp += Time.deltaTime;
            if (tempspersoUp >= tempsperso)
            {

                anim.SetTrigger("shoot");
                StartCoroutine(TirBoule());
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


    public IEnumerator TirBoule()
    {
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < 8; i++)
        {
            tirB.Play();
            Instantiate(tir, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }

        
    }

    void OnCollisionEnter(Collision c)
    {
        if (life > 0)
        {
            if (c.gameObject.tag == "TirVaisseau")
            {
                hitSound.Play();
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
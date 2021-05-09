using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocon : Alien
{
    public float timerShoot = 0f;
    public float ecartShoot = 1f;
    public AudioSource shootSound;
    public AudioSource hitSound;
    public AudioSource deathSound;
    public Animator anim;

    public int r;
    public List<Material> materials;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        timerShoot = Random.Range(0f, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        if (life > 0) { 
            position = transform.position;


            if (transform.position.y != 0f)
            {
                position.y -= Time.deltaTime * vitesse;

                if (transform.position.y < 0f)
                {
                    position.y = 0f;
                }
                transform.position = position;
                return;
            }




            timerShoot += Time.deltaTime;
            distanceTravelled += vitesse * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);


            if (timerShoot >= ecartShoot)
            {
                anim.SetTrigger("shoot");
                timerShoot = 0f;
                shootSound.Play();
                Instantiate(tir, transform.position, Quaternion.identity);


            }

            position = transform.position;
        }
            else{
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

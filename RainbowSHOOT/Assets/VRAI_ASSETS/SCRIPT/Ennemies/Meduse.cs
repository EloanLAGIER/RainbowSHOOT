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
                transform.Rotate(200f* Time.deltaTime, 0f, 0f, Space.Self);
  
                

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

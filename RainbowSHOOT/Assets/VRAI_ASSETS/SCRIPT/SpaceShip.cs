using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpaceShip : MonoBehaviour
{
    Vector3 position;
    Quaternion rotation;
    public float life = 100;
    public float vitesse = 1f;
    public float timerShoot = 1f;
    public float lastShoot = 0f;
    public GameObject Tir;
    public GameObject grenade;
    public int nbrGrenade;
    public int GrenadeMax=0;
    public Text vie;

    public AudioSource tirBruit;
    public AudioSource hitBruit;
    public AudioSource GameOver;


    public AudioClip diament;
    public AudioClip brut;
    public Animator anim;

    public float f;
    public LaserScript laser;
    public int incLaser;
    public Transform laserPointeur;
    public Transform GrenadePointeur;


    private bool rotate; // pour savoir si il �tait inclin�



    [Header("module")]
    public GameObject lanceGrenade1;
    public GameObject lanceGrenade2;
    public GameObject laser1;
    public GameObject laser2;
    public GameObject bouclier1;
    public GameObject bouclier2;



    // Start is called before the first frame update
    void Start()
    {
        
        incLaser = 0;
    }

    // Update is called once per frame
    void Update()
    {

        lastShoot += Time.deltaTime;

        position = transform.position;
        position.z += Time.deltaTime * Input.GetAxis("Vertical") * vitesse;
        position.x += Time.deltaTime * Input.GetAxis("Horizontal") * vitesse;
        position = new Vector3(Mathf.Clamp(position.x, -8f, 9f), position.y, Mathf.Clamp(position.z, -1.5f, 6f));
        transform.position = position;


        rotation = transform.rotation;
        rotation.x += Time.deltaTime * 2f * (Input.GetAxis("Horizontal"));

        rotation.x = Mathf.Clamp(rotation.x, -0.5f, 0.5f);

        if (Input.GetKeyDown(KeyCode.E))
        {
            
            anim.SetTrigger("armON");

        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(grenade, GrenadePointeur.position, Quaternion.identity);
        }
        if ((Input.GetAxis("Horizontal") == 1f) || (Input.GetAxis("Horizontal") == -1f))
        {
            rotate = true;
        }
        if ((Input.GetAxis("Horizontal") == 0f))
        {
            rotate = false;
        }
        if ((Input.GetAxis("Horizontal") < 1f) && (rotation.x != 0f) && (Input.GetAxis("Horizontal") > -1f) && (rotate == true))
        {

            if (rotation.x < 0.01f)
            {
                rotation.x += Time.deltaTime * 3f;
            }
            if (rotation.x > 0.01f)
            {
                rotation.x -= Time.deltaTime * 3f;
            }
        }

        
        transform.rotation = rotation;


        if (lastShoot >= timerShoot)
        {
            
            incLaser = ((incLaser + 1) % laser.TailleList);

            if (Input.GetKey(KeyCode.Space))
            {
                tirBruit.mute = false;

                GameObject g = Instantiate(Tir, laserPointeur.position, Quaternion.identity);

                g.GetComponent<TirVaisseau>().ChangeMaterial(laser.couleurs[incLaser]);
                
            }
            else
            {
                tirBruit.mute = true;
                
            }
            lastShoot = 0f;
        }
        

    }


    public void brutToDiament()
    {
        float f = tirBruit.time;
        if (tirBruit.clip == diament)
        {
            tirBruit.clip = brut;
        }
        else
        {
            Debug.Log("je passe par ici");
            tirBruit.clip = diament;
        }
        tirBruit.time = f;
        tirBruit.Play();
    }

    void SOUND()
    {
        tirBruit.time = f;
        tirBruit.Play();
    }
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "TirAlien")
        {
            hitBruit.Play();
            life -= 12;
            vie.text = "VIE :" + life.ToString();
            Destroy(c.gameObject);
            if (life <= 0)
            {
                GameOver.Play();
                GameObject.FindObjectOfType<UIManager>().GameOver();
            }
        }

    }

    public void activateLanceGrenade1()
    {
        GrenadeMax = 3;
        lanceGrenade1.SetActive(true);
        lanceGrenade2.SetActive(false);
    }

    public void activateLanceGrenade2()
    {
        GrenadeMax = 9;
        lanceGrenade2.SetActive(true);
        lanceGrenade1.SetActive(false);
    }
}

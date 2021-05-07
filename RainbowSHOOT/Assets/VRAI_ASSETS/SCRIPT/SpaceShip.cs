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
    public float timeurhealo;
    public float lastShoot = 0f;
    public GameObject Tir;
    public GameObject grenade;
    public int nbrGrenade;
    public int GrenadeMax;
    public Text vie;
    public Text nbrGrenad;
    public Text grenaMax;

    public AudioSource tirBruit;
    public AudioSource hitBruit;
    public AudioSource GameOver;



    public AudioClip diament;
    public AudioClip brut;
    public Animator anim;

    public float f;
    public LaserScript laserPepouz;
    public LaserScript LaserRando;
    public LaserScript laser;
    public int incLaser;

    [Header("transform")]
    public Transform laserPointeur;
    public Transform GrenadePointeur;
    public Transform diamentPlace;
    private bool diamRainbo;
    private bool arm;
    private bool rotate; // pour savoir si il �tait inclin�
    public bool healoBool;
    private bool resistoBool;
    

    [Header("Module")]
    public GameObject TirGrenade1;
    public GameObject TirGrenade2;
    public GameObject RandoLaser;
    public GameObject PepouzLaser;
    public GameObject diamentDivise;
    public GameObject diamentRainbow;
    public GameObject megaDiam;
    public GameObject speedo;
    public GameObject resisto;
    public GameObject healo;
    public GameObject bouc1;
    public GameObject bouc2;
    public GameObject bouc1Shad;
    public GameObject bouc2Shad;


    [Header("audio")]
    public AudioClip laserrando;
    public AudioClip laserpepous;
    public AudioSource gronade;

    // Start is called before the first frame update
    void Start()
    {
        healoBool = false;
        timeurhealo = 0f;
        diamRainbo = false;
        arm = false;
        GrenadeMax = 0;
        incLaser = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (healoBool)
        {
            timeurhealo += Time.deltaTime;
            if (timeurhealo > 3)
            {
                if (life < 100)
                {
                    life += 1;
                    vie.text = "VIE : " + life;
                }
                timeurhealo = 0f;
            }
            
        }
        lastShoot += Time.deltaTime;

        position = transform.position;
        position.z += Time.deltaTime * Input.GetAxis("Vertical") * vitesse;
        position.x += Time.deltaTime * Input.GetAxis("Horizontal") * vitesse;
        position = new Vector3(Mathf.Clamp(position.x, -4f, 4f), position.y, Mathf.Clamp(position.z, -1.5f, 6f));
        transform.position = position;


        rotation = transform.rotation;
        rotation.x += Time.deltaTime * 2f * (Input.GetAxis("Horizontal"));

        rotation.x = Mathf.Clamp(rotation.x, -0.5f, 0.5f);

        if (Input.GetKeyDown(KeyCode.E))
        {
            
            anim.SetTrigger("armON");
            arm = !arm;
            if (arm)
            {
                if (diamRainbo)
                {
                    diamentRainbow.GetComponent<DiamentRainbow>().changetir();
                }
            }

        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            if (nbrGrenade > 0)
            {
                
                Instantiate(grenade, GrenadePointeur.position, Quaternion.Euler(0,180,0));
                gronade.Play();
                nbrGrenade -= 1;
                nbrGrenad.text = "grenade : " + nbrGrenade.ToString();
            }
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
        //tirBruit.time = f;
        //tirBruit.Play();
    }
    void OnCollisionEnter(Collision c)
    {

        if (c.gameObject.tag == "TirAlien")
        {
            hitBruit.Play();
            if (resistoBool) { life -= 6; }
            else
            {
                life -= 12;
            }
            vie.text = "VIE :" + life.ToString();
            Destroy(c.gameObject);
            if (life <= 0)
            {
                GameOver.Play();
                GameObject.FindObjectOfType<UIManager>().GameOver();
            }
        }

        if (c.gameObject.tag == "Grenade")
        {

            if (nbrGrenade + 1 <= GrenadeMax)
            {
                nbrGrenade += 1;
                nbrGrenad.text = "grenade : " + nbrGrenade.ToString();
                Destroy(c.gameObject);
            }
        }

    }


    public void LanceGrenade1()
    {
        TirGrenade1.SetActive(true);
        TirGrenade2.SetActive(false);
        GrenadeMax = 3;
        grenaMax.text = "/3";
    }

    public void LanceGrenade2()
    {
        TirGrenade1.SetActive(false);
        TirGrenade2.SetActive(true);
        GrenadeMax = 9;
        grenaMax.text = "/9";
    }


    public void Laserrando()
    {
        laser = LaserRando;
        RandoLaser.SetActive(true);
        PepouzLaser.SetActive(false);
        float t = tirBruit.time;
        tirBruit.clip = laserrando;
        tirBruit.time = t;
        tirBruit.Play();
    }


    public void LaserPepouz()
    {

        laser = laserPepouz;
        RandoLaser.SetActive(false);

        PepouzLaser.SetActive(true);

        float t = tirBruit.time;
        tirBruit.clip = laserpepous;
        tirBruit.time = t;
        tirBruit.Play();
    }

    public void DiamenDivise()
    {
        diamentDivise.SetActive(true);
        diamentRainbow.SetActive(false);
        megaDiam.SetActive(false);
    }

    public void DiamentRainbow()
    {
        diamRainbo = true;
        diamentDivise.SetActive(false);
        diamentRainbow.SetActive(true);
        megaDiam.SetActive(false);
    }

    public void DiamMega()
    {
        megaDiam.SetActive(true);
        diamentDivise.SetActive(false);
        diamentRainbow.SetActive(false);
    }

    public void Speedo()
    {
        speedo.SetActive(true);
        vitesse = 10f;
    }

    public void Healo()
    {
        healo.SetActive(true);
        healoBool = true;

    }

    public void Resisto()
    {
        resisto.SetActive(true);
        resistoBool = true;
    }

    public void Bouc1()
    {
        bouc1.SetActive(true);
        bouc2.SetActive(false);
        if (FindObjectOfType<Bouclier>())
        {
            Destroy(FindObjectOfType<Bouclier>());
        }
        Instantiate(bouc1Shad, transform.position, Quaternion.identity);
    }

    public void Bouc2()
    {
        bouc2.SetActive(true);
        bouc1.SetActive(false);
        if (FindObjectOfType<Bouclier>())
        {
            Destroy(FindObjectOfType<Bouclier>().gameObject);
        }
        Instantiate(bouc2Shad, transform.position, Quaternion.identity);
    }
}

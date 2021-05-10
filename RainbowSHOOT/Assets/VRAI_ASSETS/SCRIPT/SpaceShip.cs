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

    public int lasernum = 0;

    public AudioClip diament;
    public AudioClip brut;
    public AudioClip diament2;
    public AudioClip brut2;
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
    public bool arm;
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

    public GameObject impact;

    [Header("audio")]
    public AudioClip laserrando;
    public AudioClip laserpepous;
    public AudioSource gronade;
    public AudioClip diamRando;
    public AudioClip diamPepous;
    public AudioSource RamasseGrenade;

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
                    vie.text = life.ToString();
                }
                timeurhealo = 0f;
            }
            
        }
        lastShoot += Time.deltaTime;

        position = transform.position;
        position.z += Time.deltaTime * Input.GetAxis("Vertical") * vitesse;
        position.x += Time.deltaTime * Input.GetAxis("Horizontal") * vitesse;
        position.z = Mathf.Clamp(position.z, -1.5f, 6f);
        position = new Vector3(Mathf.Clamp(position.x, -4f*((position.z/7.5f)+1.2f), 4f * ((position.z / 7.5f) + 1.2f)), position.y, position.z);
        transform.position = position;


        rotation = transform.rotation;
        rotation.x += Time.deltaTime * 2f * (Input.GetAxis("Horizontal"));

        rotation.x = Mathf.Clamp(rotation.x, -0.5f, 0.5f);

        if (Input.GetKeyDown(KeyCode.E))
        {
            
            anim.SetTrigger("armON");

            arm = !arm;
            if (!arm)
            {
                if (lasernum == 0)
                {
                    brutToDiament1();
                }
                else { brutToDiament2(); }
                if (diamRainbo)
                {
                    diamentRainbow.GetComponent<DiamentRainbow>().changetir();
                }
            }
            else
            {
                if (lasernum == 0)
                {
                    Invoke("brutToDiament1",3f);
                }
                else { Invoke("brutToDiament2", 3f); }
            }

        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            if (nbrGrenade > 0)
            {
                
                Instantiate(grenade, GrenadePointeur.position, Quaternion.Euler(0,180,0));
                gronade.Play();
                nbrGrenade -= 1;
                nbrGrenad.text = nbrGrenade.ToString();
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

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }


    public void brutToDiament1()
    {
        float f = tirBruit.time;
        if (tirBruit.clip == diament)
        {
            tirBruit.clip = laserrando;
        }
        else
        {
            
            tirBruit.clip = diament;
        }
        tirBruit.time = f;
        tirBruit.Play();
    }
    public void brutToDiament2()
    {
        float f = tirBruit.time;
        if (tirBruit.clip == diament2)
        {
            tirBruit.clip = laserpepous;
        }
        else
        {

            tirBruit.clip = diament2;
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
            GameObject g =Instantiate(impact, c.transform.position, Quaternion.identity);
            StartCoroutine(DestroyImpact(g));
            hitBruit.Play();
            if (resistoBool) { life -= 6; }
            else
            {
                life -= 12;
            }
            vie.text = life.ToString();
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
                RamasseGrenade.Play();
                nbrGrenade += 1;
                nbrGrenad.text = nbrGrenade.ToString();
                Destroy(c.gameObject);
            }
        }

    }

    public IEnumerator DestroyImpact(GameObject g)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(g.gameObject);

    }
        public void LanceGrenade1()
    {
        TirGrenade1.SetActive(true);
        TirGrenade2.SetActive(false);
        GrenadeMax = 3;
        grenaMax.text = "3";
    }

    public void LanceGrenade2()
    {
        TirGrenade1.SetActive(false);
        TirGrenade2.SetActive(true);
        GrenadeMax = 9;
        grenaMax.text = "9";
    }


    public void Laserrando()
    {
        lasernum = 0;
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
        lasernum = 1;

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

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


    public int r;
    public List<Material> materials;

    // Start is called before the first frame update
    void Start()
    {
        timerShoot = Random.Range(0f, 1f);

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




        timerShoot += Time.deltaTime;
        distanceTravelled += vitesse * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);


        if (timerShoot >= ecartShoot)
        {
            timerShoot = 0f;
            shootSound.Play();
            Instantiate(tir, transform.position, Quaternion.identity);


        }


        if (life <= 0)
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().score += Random.Range(100, 200);
            
            GameObject.Find("AUDIOMANAGER").GetComponent<AudioSource>().Play();
            GameObject.Find("WaveManager").GetComponent<WaveManager>().ennemies -= 1;
            Destroy(this.gameObject);
        }
    }
}

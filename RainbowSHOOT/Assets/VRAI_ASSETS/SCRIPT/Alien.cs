using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathCreation;

public class Alien : MonoBehaviour
{
    public int life = 100;
    public float vitesse = 1f;

    public float timerShoot = 0f;
    public float ecartShoot = 1f;

    public GameObject tir;
    Vector3 position;

    
    public List<Material> materials;


    public AudioSource shootSound;
    public AudioSource hitSound;
    public AudioSource deathSound;


    public PathCreator pathCreator;
    public float distanceTravelled;

    public int r;


    // Start is called before the first frame update
    void Start()
    {
        r = Random.Range(0, 6);
        timerShoot = Random.Range(0f, 1f);
        GetComponentInChildren<Renderer>().material = materials[r];
    }

    // Update is called once per frame
    void Update() {
        

        


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
            GameObject t = Instantiate(tir, transform.position, Quaternion.identity);
            t.GetComponent<TirAlien>().ChangeMaterial(materials[r]);
            
        }


        if (life <=0){
            GameObject.Find("UIManager").GetComponent<UIManager>().score += Random.Range(100, 200);
            GameObject.Find("Score").GetComponent<Text>().text = GameObject.Find("UIManager").GetComponent<UIManager>().score.ToString();
            GameObject.Find("AUDIOMANAGER").GetComponent<AudioSource>().Play();
            GameObject.Find("WaveManager").GetComponent<WaveManager>().ennemies -= 1;
            Destroy(this.gameObject);
        }
        
    }
}

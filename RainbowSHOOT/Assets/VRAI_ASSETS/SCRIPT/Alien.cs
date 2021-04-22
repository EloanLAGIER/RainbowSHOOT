using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alien : MonoBehaviour
{
    public int life = 100;
    public int sens = -1;
    float timerSens = 0f;
    public float vitesse = 1f;

    public float timerShoot = 0f;
    public float ecartShoot = 1f;

    public GameObject tir;
    Vector3 position;

    
    public List<Material> materials;


    public AudioSource shootSound;
    public AudioSource hitSound;
    public AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
        timerShoot = Random.Range(0f, 1f);
        GetComponentInChildren<Renderer>().material = materials[Random.Range(0,6)];
    }

    // Update is called once per frame
    void Update() {
        
        timerSens+= Time.deltaTime;
        


        position = transform.position;
        position.x += sens*vitesse*Time.deltaTime;

        if (((position.x <= -9f) || (position.x >= 9f)) && (timerSens >= 1f))
        {
            sens *= -1;
            timerSens = 0f;
        }


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


        transform.position = position;


        if (timerShoot >= ecartShoot)
        {
            shootSound.Play();
            Instantiate(tir, transform.position, Quaternion.identity);
            timerShoot = 0f;
        }


        if (life <=0){
            GameObject.Find("UIManager").GetComponent<UIManager>().score += Random.Range(100, 200);
            GameObject.Find("Score").GetComponent<Text>().text = GameObject.Find("UIManager").GetComponent<UIManager>().score.ToString();
            GameObject.Find("AUDIOMANAGER").GetComponent<AudioSource>().Play();
            GameObject.Find("WaveManager").GetComponent<WaveManager>().ennemies -=1;
            Destroy(this.gameObject);
        }
        
    }
}

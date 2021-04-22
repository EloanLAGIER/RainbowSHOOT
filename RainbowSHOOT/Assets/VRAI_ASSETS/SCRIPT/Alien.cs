using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        timerSens+= Time.deltaTime;
        timerShoot += Time.deltaTime;
        position = transform.position;
        position.x += sens*vitesse*Time.deltaTime;

        if (timerShoot>= ecartShoot)
        {
            Instantiate(tir, transform.position, Quaternion.identity);
            timerShoot = 0f;
        }
      
        if (((position.x <=-9f) || (position.x>=9f)) && (timerSens>=1f)) {
	        sens*=-1;
	        timerSens=0f;
	    }

        transform.position = position;
        if (life <=0){
            Destroy(this.gameObject);
        }
        
    }
}

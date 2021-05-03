using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirVaisseau : Tir
{
    Vector3 position;
    public float vitesse;
    public bool diamentcreated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

	    transform.position += transform.forward * vitesse * Time.deltaTime;
	    if(transform.position.z >=80f){
       
        	Destroy(this.gameObject);	
	    } 
    }

    void OnCollisionEnter(Collision c){
    if (c.gameObject.tag == "Alien" ){

	    c.gameObject.GetComponent<Alien>().life -=12;
        c.gameObject.GetComponent<Alien>().Scorehit();
        Destroy(this.gameObject);
	}
}
    
}

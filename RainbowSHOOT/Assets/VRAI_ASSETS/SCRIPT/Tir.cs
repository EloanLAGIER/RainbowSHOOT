using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{
    Vector3 position;
    public float vitesse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
	position.z += vitesse * Time.deltaTime;
	transform.position = position;
	if(position.z >=40f){
        	Destroy(this.gameObject);	
	} 
    }

    void OnCollisionEnter(Collision c){
    if (c.gameObject.tag == "Alien" ){

	    c.gameObject.GetComponent<Alien>().life -=12;
        c.gameObject.GetComponent<Alien>().hitSound.Play();
        Destroy(this.gameObject);
	}
}
    
}

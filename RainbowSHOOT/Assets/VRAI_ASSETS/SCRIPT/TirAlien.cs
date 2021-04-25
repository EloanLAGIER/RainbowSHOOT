using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirAlien : Tir
{
    Vector3 position;
    public float vitesse;

    // Start is called before the first frame update
    void Start()
    {
        mat = "jaune";
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
	    position.z -= vitesse * Time.deltaTime;
	    transform.position = position;
	    if(position.z <=-10f){
        	Destroy(this.gameObject);	
	    } 
    }


}

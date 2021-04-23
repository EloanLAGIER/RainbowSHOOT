using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpaceShip : MonoBehaviour
{
    Vector3 position;
    public float life = 100;
    public float vitesse = 1f;
    public float timerShoot = 1f;
    public float lastShoot = 0f;
    public GameObject Tir;
    public Text vie;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
	lastShoot += Time.deltaTime;
        position = transform.position;
        position.z += Time.deltaTime * Input.GetAxis("Vertical") * vitesse;
        position.x += Time.deltaTime * Input.GetAxis("Horizontal") * vitesse;
        position = new Vector3(Mathf.Clamp(position.x,-8f,9f),position.y,Mathf.Clamp(position.z,-1.5f,6f));
        transform.position = position;
	if (Input.GetKey(KeyCode.Space)){
		if(lastShoot>=timerShoot){
		Instantiate(Tir,transform.position,Quaternion.identity);
		lastShoot = 0f;
		}
	}
		
    }


    void OnCollisionEnter(Collision c)
    {
        Debug.Log("hit");
        if(c.gameObject.tag == "TirAlien")
        {
            life -= 12;
            vie.text = "VIE :" + life.ToString();
        }
    }
}

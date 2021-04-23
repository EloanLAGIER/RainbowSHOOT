﻿using System.Collections;
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
    public float lastShoot = 0f;
    public GameObject Tir;
    public Text vie;

    public AudioSource tirBruit;
    public AudioSource hitBruit;
    public AudioSource GameOver;

    private bool rotate; // pour savoir si il �tait inclin�
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
        position = new Vector3(Mathf.Clamp(position.x, -8f, 9f), position.y, Mathf.Clamp(position.z, -1.5f, 6f));
        transform.position = position;


        rotation = transform.rotation;
        rotation.z += Time.deltaTime * -2f * (Input.GetAxis("Horizontal"));

        rotation.z = Mathf.Clamp(rotation.z, -0.5f, 0.5f);


        if ((Input.GetAxis("Horizontal") == 1f) || (Input.GetAxis("Horizontal") == -1f))
        {
            rotate = true;
        }
        if ((Input.GetAxis("Horizontal") == 0f))
        {
            rotate = false;
        }
        if ((Input.GetAxis("Horizontal") < 1f) && (rotation.z != 0f) && (Input.GetAxis("Horizontal") > -1f) && (rotate == true))
        {

            if (rotation.z < 0.01f)
            {
                rotation.z += Time.deltaTime * 3f;
            }
            if (rotation.z > 0.01f)
            {
                rotation.z -= Time.deltaTime * 3f;
            }
        }

        transform.rotation = rotation;

        if (Input.GetKey(KeyCode.Space))
        {
            if (lastShoot >= timerShoot)
            {
                tirBruit.Play();

                Instantiate(Tir, transform.position, Quaternion.identity);
                lastShoot = 0f;
            }
        }

    }


    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "TirAlien")
        {
            hitBruit.Play();
            life -= 12;
            vie.text = "VIE :" + life.ToString();
            Destroy(c.gameObject);
            if (life <= 0)
            {
                GameOver.Play();
                GameObject.FindObjectOfType<UIManager>().GameOver();
            }
        }

    }
}

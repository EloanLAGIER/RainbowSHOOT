using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouclier : MonoBehaviour
{
    public Transform Vaisseau;
    public Shield info;
    private int inc;
    private float timerShield;
    public string currentMat;
    public bool matEmpty = false;

    // Start is called before the first frame update
    void Start()
    {
        Vaisseau = GameObject.Find("SpaceShip").transform;
        inc = 0;
        timerShield = 0;
        currentMat = info.nomMat[inc];
        gameObject.GetComponent<ParticleSystem>().startColor = info.couleurs[inc];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vaisseau.position;

        timerShield += Time.deltaTime;
        if (timerShield >= info.time[inc])
        {
            inc = (inc + 1) % info.tailleList;
            timerShield = 0;
            gameObject.GetComponent<ParticleSystem>().startColor = info.couleurs[inc];
            currentMat = info.nomMat[inc];


        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!matEmpty)
        {
            if (collision.gameObject.GetComponent<Tir>())
            {
                if (collision.gameObject.GetComponent<Tir>().mat == currentMat)
                {
                    Debug.Log(collision.gameObject.GetComponent<Tir>().mat + " == " + currentMat);
                    Destroy(collision.gameObject);
                    Debug.Break();
                }
                else
                {
                    Debug.Log(collision.gameObject.GetComponent<Tir>().mat + " != " + currentMat);
                }
            }
        }
    }
}

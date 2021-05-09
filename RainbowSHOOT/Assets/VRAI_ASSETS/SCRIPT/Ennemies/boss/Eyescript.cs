using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyescript : BOSS
{
    Vector3 position;
    int sens = -1;
    public int vitesse;
    public float timeur;
    public GameObject tir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        position.x += Time.deltaTime * sens * vitesse;
        if (position.x > 4f)
        {
            sens = -1;
        }
        if (position.x < -4f)
        {
            sens = 1;
        }
        transform.position = position;

        timeur += Time.deltaTime;
        if (timeur > 5f)
        {
            Instantiate(tir, transform.position, Quaternion.identity);
            timeur = 0f;
        }

        if (transform.position.y < -50f)
        {
            Destroy(this.gameObject);
        }

    }
}

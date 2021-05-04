using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Alien
{

    public bool protect;
    // Start is called before the first frame update
    void Start()
    {
        protect = false;
    }

    // Update is called once per frame
    void Update()
    {
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





        distanceTravelled += vitesse * Time.deltaTime;

        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
    }

    public void ChangeProtect()
    {
        protect = !protect;
        if (!protect)
        {
            Instantiate(tir, transform.position, Quaternion.identity);
        }
    }


    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "TirVaisseau")
        {
            if (!protect)
            {
                life -= 12;
                Scorehit();
                Destroy(c.gameObject);
            }
            
        }
    }

}

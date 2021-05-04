using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nymph : Alien
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
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



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MeduseBoss : BOSS

{
    public Meduse meduse;
    public PathCreator path;
    public float timeur;
    public Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        timeur = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        timeur += Time.deltaTime;
        if (timeur > 10f)
        {
            Meduse g =Instantiate(meduse, position, Quaternion.identity);
            g.pathCreator = path;

        }
    }
}

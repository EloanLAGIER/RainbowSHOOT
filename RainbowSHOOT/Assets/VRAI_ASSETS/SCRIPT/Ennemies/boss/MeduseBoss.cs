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
    public int nbmeduse;
    // Start is called before the first frame update
    void Start()
    {
        timeur = 0;

        
    }

    // Update is called once per frame
    void Update()
    {
        timeur += Time.deltaTime;
        if (timeur > 4f)
        {
            if (FindObjectsOfType<Meduse>().Length < 10)
            {
                FindObjectOfType<WaveManager>().ennemies += 1;
                Meduse g = Instantiate(meduse, path.path.GetPointAtDistance(path.path.length / 2), Quaternion.Euler(90, 0, 0));
                g.pathCreator = path;
                g.distanceTravelled = path.path.length / 2;
            }
            timeur = 0f;



        }
    }
}

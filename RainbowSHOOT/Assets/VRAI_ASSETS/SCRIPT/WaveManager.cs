using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathCreation;


public class WaveManager : MonoBehaviour
{

    public int waveCount = 0;
    public bool game;
    public int ennemies;
    public GameObject alien;
    public Text waves;

    public PathCreator pathCreator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (game && (ennemies == 0) )
        {
            waveCount += 1;
            GenerateNewWave();
            waves.text = " vagues : " + waveCount;
        }
    }

    void GenerateNewWave()
    {
        Debug.Log("new wave");
        Debug.Log("wave="+waveCount);

        int r = Random.Range(waveCount, waveCount + 2);
        Debug.Log("nbr alien="+r);
        for (int i = 0; i < r; i++)
        {
            Vector3 p = pathCreator.path.GetPointAtDistance(pathCreator.path.length / (r + 1) * (i + 1));
            p.y += 10;
            Debug.Log("transform alien"+i+" : "+p);
            GameObject a = Instantiate(alien, p, Quaternion.identity);
            a.GetComponent<Alien>().pathCreator = pathCreator;
            a.GetComponent<Alien>().distanceTravelled = pathCreator.path.length / (r + 1) * (i + 1);
            ennemies += 1;
        }
    }
}

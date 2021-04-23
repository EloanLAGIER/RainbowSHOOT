using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public int waveCount = 0;
    public bool game;
    public int ennemies;
    public GameObject alien;
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
            Instantiate(alien, new Vector3(-9f+(18f/(r+1)*(i+1f)), 10f, 22f), Quaternion.identity);
            ennemies += 1;
        }
    }
}

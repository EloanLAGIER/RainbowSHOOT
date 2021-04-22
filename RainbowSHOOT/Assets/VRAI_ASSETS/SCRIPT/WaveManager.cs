using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{

    public int waveCount = 0;
    public bool game;
    public int ennemies;
    public GameObject alien;
    public Text wave;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            foreach (Alien g in GameObject.FindObjectsOfType<Alien>())
            {
                Destroy(g.gameObject);
            }
            ennemies = 0;
            waveCount = 0;
            GameObject.Find("vie").GetComponent<Text>().text = "vie : 100";
            GameObject.Find("Score").GetComponent<Text>().text = "0";
            GameObject.Find("GAMEOVER").GetComponent<Text>().text = "";
            GameObject.FindObjectOfType<UIManager>().score = 0;
            GameObject.FindObjectOfType<SpaceShip>().life = 100;
        }
        if (game && (ennemies == 0) )
        {
            waveCount += 1;
            wave.text = "Wave : " + waveCount;
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

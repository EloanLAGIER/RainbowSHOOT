using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathCreation;


public class WaveManager : MonoBehaviour
{

    public int niveauCount = 1;

    public int waveCount = 0;
    public bool game;
    public int ennemies;
    
    public Text waves;
    public Text niveau;
    public int randomShop;
    public PathCreator pathCreator;


    public List<PathCreator> paths;

    public GameObject Mag;
    public List<Flocon> flocons;
    public List<Star> stars;
    public List<Pollen> pollens;
    public List<Nymph> nymphs;
    public List<Rock> rocks;
    public List<Meduse> meduses;


    public AudioSource level;
    public AudioClip l1;
    public AudioClip l2;
    public AudioClip l3;
    // Start is called before the first frame update
    void Start()
    {
        randomShop = Random.Range(3, 7);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (waveCount == randomShop)
        {
            Mag.gameObject.SetActive(true);
            Mag.GetComponent<MagasinManager>().reroll();
            game = false;
            randomShop = 10 * niveauCount + Random.Range(3, 7);
        }

        if (game && (ennemies == 0) )
        {
            waveCount += 1;
            if (waveCount == 10)
            {
                niveauCount += 1;
                niveau.text = "niveau : " + niveauCount;
                waveCount = 0;
                float c = level.time;
                if (niveauCount == 2)
                {
                    level.clip = l2;
                }
                if (niveauCount == 3)
                {
                    level.clip = l3;
                }
                level.time = c;
                level.Play();

            }
            GenerateNewWave();
           
        }
        
    }

    void GenerateNewWave()
    {
        waves.text = "vague : " + waveCount;
        if (niveauCount == 1)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    ennemies += 1;
                    Vector3 p = pathCreator.path.GetPointAtDistance(pathCreator.path.length / (5 + 1) * (i + 1));
                    p.y += 10;
                   
                    Alien a = Instantiate(flocons[Random.Range(0, flocons.Count)], p, Quaternion.identity);
                    a.GetComponent<Alien>().pathCreator = pathCreator;
                    a.GetComponent<Alien>().distanceTravelled = pathCreator.path.length / (5 + 1) * (i + 1);
                    
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    ennemies += 1;
                    Vector3 p = pathCreator.path.GetPointAtDistance(pathCreator.path.length / (5 + 1) * (i + 1));
                    p.y += 10;
                   
                    Star a = Instantiate(stars[Random.Range(0, stars.Count)], p, Quaternion.Euler(0, 180, 0));
                    a.pathCreator = pathCreator;
                    a.distanceTravelled = pathCreator.path.length / (5 + 1) * (i + 1);

                }
            }
        }

        if (niveauCount == 2)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Vector3 p = pathCreator.path.GetPointAtDistance(pathCreator.path.length / (5 + 1) * (i + 1));
                    p.y += 10;

                    Nymph a = Instantiate(nymphs[Random.Range(0, nymphs.Count)], p, Quaternion.Euler(00, 180, 0));
                    a.pathCreator = pathCreator;
                    a.distanceTravelled = pathCreator.path.length / (5 + 1) * (i + 1);
                    ennemies += 1;
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    Vector3 p = pathCreator.path.GetPointAtDistance(pathCreator.path.length / (5 + 1) * (i + 1));
                    p.y += 10;

                    Pollen a = Instantiate(pollens[Random.Range(0, pollens.Count)], p, Quaternion.Euler(90, 0, 0));
                    a.pathCreator = pathCreator;
                    a.distanceTravelled = pathCreator.path.length / (5 + 1) * (i + 1);
                    ennemies += 1;
                }
            }
        }

        if (niveauCount == 3)
        {
            int rand = Random.Range(0, 2);
            rand = 1;
            if (rand == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Vector3 p = pathCreator.path.GetPointAtDistance(pathCreator.path.length / (5 + 1) * (i + 1));
                    p.y += 10;

                    Rock a = Instantiate(rocks[Random.Range(0, rocks.Count)], p, Quaternion.Euler(0, 180, 0));
                    a.pathCreator = pathCreator;
                    a.distanceTravelled = pathCreator.path.length / (5 + 1) * (i + 1);
                    ennemies += 1;
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    Vector3 p = pathCreator.path.GetPointAtDistance(pathCreator.path.length / (5 + 1) * (i + 1));
                    p.y += 10;

                    Meduse a = Instantiate(meduses[Random.Range(0, meduses.Count)], p, Quaternion.Euler(90, 0, 0));
                    a.pathCreator = pathCreator;
                    a.distanceTravelled = pathCreator.path.length / (5 + 1) * (i + 1);
                    ennemies += 1;
                }
            }
        }




    }
}

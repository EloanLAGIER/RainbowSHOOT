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
    public Text go;
    public Text vie;
    public int randomShop;
    public PathCreator pathCreator;


    public List<PathCreator> paths;

    [Header("ennemies")]
    public GameObject Mag;
    public List<Flocon> flocons;
    public List<Star> stars;
    public List<Pollen> pollens;
    public List<Nymph> nymphs;
    public List<Rock> rocks;
    public List<Meduse> meduses;

    [Header("audio")]
    public AudioSource level;
    public AudioClip l1;
    public AudioClip l2;
    public AudioClip l3;

    [Header("Boss")]
    public BOSS flocon;
    public BOSS star;
    public BOSS nymph;
    public GameObject pollen;
    public BOSS meduse;
    public GameObject rock;

    public Vector3 Bosspos;
    public Vector3 BossPos2;
    public Vector3 BossPos3;
    // Start is called before the first frame update
    void Start()
    {
        randomShop = Random.Range(3, 7);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ennemies = 0;
            FindObjectOfType<SpaceShip>().life = 100;
            vie.text = "100";
            niveauCount = 1;

            waveCount = 0;

            go.text = "";
            game = true;
        }
        

        
        if (game && (ennemies == 0) )
        {
            waveCount += 1;
            if (waveCount == randomShop)
            {
                foreach (TirAlien g in GameObject.FindObjectsOfType<TirAlien>())
                {
                    Destroy(g.gameObject);
                }
                Mag.gameObject.SetActive(true);
                Mag.GetComponent<MagasinManager>().reroll();
                game = false;
                randomShop = waveCount + 1;
                randomShop = 10 * niveauCount + Random.Range(3, 7);
            }
            else if (waveCount == 9)
            {
                BossWave();
            }
            else if (waveCount == 10)
             {

                foreach (TirAlien g in GameObject.FindObjectsOfType<TirAlien>())
                {
                    Destroy(g.gameObject);
                }
                Mag.gameObject.SetActive(true);
                Mag.GetComponent<MagasinManager>().reroll();
                game = false;

                niveauCount += 1;

                
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
            else 
            { 
                
                
                GenerateNewWave(); 
            }
           
        }
        
    }

    void BossWave()
    {
        ennemies += 1;
        int rand = Random.Range(0, 2);
        rand = 0;
        if (niveauCount == 1)
        {
            if (rand == 0)
            {
                Instantiate(flocon, Bosspos, Quaternion.identity);
            }
            else
            {
                Instantiate(star, Bosspos, Quaternion.identity);
            }
        }
        if (niveauCount == 2)
        {
            if (rand == 0)
            {
                Instantiate(nymph, Bosspos, Quaternion.identity);
                Invoke("BossNymph2", .66f);
                Invoke("BossNymph3", 1.33f);
            }
            else
            {
                Instantiate(pollen, Bosspos, Quaternion.identity);
            }
        }
        if (niveauCount == 3)
        {
            if (rand == 0)
            {
                Instantiate(meduse, Bosspos, Quaternion.identity);
                
            }
            else
            {
                Instantiate(rock, Bosspos, Quaternion.identity);

            }

        }
    }
    public void BossNymph2()
    {
        Instantiate(nymph, BossPos2, Quaternion.identity);
    }

    public void BossNymph3()
    {
        Instantiate(nymph, BossPos3, Quaternion.identity);
    }
    void GenerateNewWave()
    {

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
                   
                    Flocon a = Instantiate(flocons[Random.Range(0, flocons.Count)], p, Quaternion.identity);
                    a.pathCreator = pathCreator;
                    a.distanceTravelled = pathCreator.path.length / (5 + 1) * (i + 1);
                    
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
            rand = 0;
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

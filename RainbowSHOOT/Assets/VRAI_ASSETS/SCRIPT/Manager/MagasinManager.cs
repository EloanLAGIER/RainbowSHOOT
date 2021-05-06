using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagasinManager : MonoBehaviour
{
    public int current;

    public List<Image> fonds;
    public Image img1;
    public Image img2;

    public List<Sprite> miniatures;

    public SpaceShip spaceship;

    int r;
    int r2;

    void Start()
    {
        spaceship = FindObjectOfType<SpaceShip>();
        current = 0;
        r = Random.Range(0, miniatures.Count);
        img1.sprite = miniatures[r];
        r2 = Random.Range(0, miniatures.Count);
        while (r2 == r)
        {
            r2 = Random.Range(0, miniatures.Count);
        }
        img2.sprite = miniatures[r2];

    }

    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) == true)
        {
            fonds[current].color = Color.gray;
            current = ((current+2)% 3);

        }
        if (Input.GetKeyDown(KeyCode.RightArrow) == true)
        {
            fonds[current].color = Color.gray;
            current = ((current +1) % 3);

        }

        fonds[current].color = Color.blue;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            int r3 = 0;
            
            if (current == 0) {
                r3 = r;
               
            }

            if (current == 1)
            {
                r3 = r2;

            }

            if (r3 == 0)
            {
                spaceship.Laserrando();
            }
            if (r3 == 1)
            {
                spaceship.LaserPepouz();
            }
            if (r3 == 2)
            {
                spaceship.LanceGrenade1();
            }
            if (r3 == 3)
            {
                spaceship.LanceGrenade2();
            }

            if (r3== 4)
            {
                spaceship.DiamenDivise();
            }

            if (r3 == 5)
            {
                spaceship.DiamentRainbow();
            }

            if (r3 == 6)
            {
                spaceship.DiamMega();
            }

            if (r3 == 7)
            {
                spaceship.Healo();
            }
            if (r3 == 8)
            {
                spaceship.Resisto();
            }
            if (r3 == 9)
            {
                spaceship.Speedo();
            }
            if (r3 == 10)
            {
                spaceship.Bouc1();
            }
            if (r3 == 11)
            {
                spaceship.Bouc2();
            }
            FindObjectOfType<WaveManager>().game = true;
            this.gameObject.SetActive(false);

        }


    }

    public void reroll()
    {
        spaceship = FindObjectOfType<SpaceShip>();
        current = 0;
        r = Random.Range(0, miniatures.Count);
        img1.sprite = miniatures[r];
        r2 = Random.Range(0, miniatures.Count);

        
        while (r2 == r)
        {
            r2 = Random.Range(0, miniatures.Count);
        }
        img2.sprite = miniatures[r2];
    }
}

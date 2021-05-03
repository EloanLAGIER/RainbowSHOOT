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

    public List<GameObject> objects;
    public List<Sprite> miniatures;


    int r;
    int r2;

    void Start()
    {
        current = 0;
        r = Random.Range(0, objects.Count);

        img1.sprite = miniatures[r];
        r2 = Random.Range(0, objects.Count);
        while (r2 == r)
        {
            r2 = Random.Range(0, objects.Count);
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
            if (current == 0) {
                Instantiate(objects[r], transform.position, Quaternion.identity);
            }

            if (current == 1)
            {
                Instantiate(objects[r2], transform.position, Quaternion.identity);
            }
            FindObjectOfType<WaveManager>().game = true;
            this.gameObject.SetActive(false);

        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSNymph : BOSS
{
    public GameObject tir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (transform.position.y < -50)
        {
            Destroy(this.gameObject);
        }
    }
    public void TIRS()
    {
        int rand = Random.Range(5, 15);
        for (int i = 0; i <= rand; i++)
        {
            tirB.Play();
            Instantiate(tir, transform.position, Quaternion.identity);
        }
    }
}

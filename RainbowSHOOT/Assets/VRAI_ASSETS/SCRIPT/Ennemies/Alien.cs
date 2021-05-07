using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathCreation;

public class Alien : MonoBehaviour
{
    public int life = 100;
    public float vitesse = 1f;

    public GameObject hit;

    public GameObject tir;
    public Vector3 position;
    public PathCreator pathCreator;
    public float distanceTravelled;

    public GameObject dropGrenade;

    // Start is called before the first frame update
    void Start()
    {
        

    }



    public void Explosion()
    {
        life -= 50;
        if (life <= 0)
        {
            int rando = Random.Range(0, 10);
            if (rando == 5)
            {
                Instantiate(dropGrenade, transform.position, Quaternion.identity);
            }

            GameObject.Find("WaveManager").GetComponent<WaveManager>().ennemies -= 1;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
    public void Scorehit()
    {
        GameObject.Find("Score").GetComponent<Text>().text = GameObject.Find("UIManager").GetComponent<UIManager>().score.ToString();
    }


    public IEnumerator DestroyHit(GameObject g)
    {
        yield return new WaitForSeconds(2f);
        Destroy(g);
        Debug.Log("destroyed");
    }
}

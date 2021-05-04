using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathCreation;

public class Alien : MonoBehaviour
{
    public int life = 100;
    public float vitesse = 1f;



    public GameObject tir;
    public Vector3 position;

    






    public PathCreator pathCreator;
    public float distanceTravelled;



    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update() {





       

    }

    public void Explosion()
    {
        life -= 50;
    }
    public void Scorehit()
    {
        GameObject.Find("Score").GetComponent<Text>().text = GameObject.Find("UIManager").GetComponent<UIManager>().score.ToString();
    }
}

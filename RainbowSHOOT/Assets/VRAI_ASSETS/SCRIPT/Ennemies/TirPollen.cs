using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirPollen : MonoBehaviour
{
    Vector3 position;
    public float vitesse;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(GameObject.Find("SpaceShip").transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * vitesse * Time.deltaTime;

        if (position.z <= -10f)
        {
            Destroy(this.gameObject);
        }
    }
}

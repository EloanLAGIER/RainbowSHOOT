using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloconBoss : BOSS
{
    Vector3 rotate = new Vector3(45, 45, 45);
    float z = 0f;

    public GameObject tir;
    public List<Transform> transforms;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        vie = 200;
        time = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vie > 0)
        {
            z = ((z + Time.deltaTime) % 30);
            rotate = new Vector3(15 * Time.deltaTime, 15 * Time.deltaTime, (z - 15) * Time.deltaTime);
            transform.Rotate(rotate);

            time += Time.deltaTime;

            if (time > 0.5f)
            {
                if (vie > 0)
                {
                    tirB.Play();
                    Instantiate(tir, transforms[Random.Range(0, transforms.Count)].position, Quaternion.identity);
                    time = 0f;
                }
            }
        }

        if (transform.position.y < -50)
        {
            Destroy(this.gameObject);
        }
        
    }


}

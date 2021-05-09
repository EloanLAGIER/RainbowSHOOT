using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STARBOSS : BOSS
{
    public Vector3 position;
    public int sens;
    public float vitesse;

    public float timeurTir;
    public Animator anim;
    public GameObject tir;
    
    // Start is called before the first frame update
    void Start()
    {
        vie = 100;
        sens = 1;
        timeurTir = Random.Range(10f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        if (vie > 0)
        {
            timeurTir -= Time.deltaTime;
            position = transform.position;
            position.x += Time.deltaTime * sens * vitesse;
            if ((position.x > 8f) || (position.x < -8f))
            {
                sens *= -1;
            }
            transform.position = position;
            if ((timeurTir < 0f) && (vie > 0))
            {
                anim.SetTrigger("rotation");
                StartCoroutine("Tirage");
                timeurTir = Random.Range(10f, 15f);



            }
        }

        if (transform.position.y < -50)
        {
            Destroy(this.gameObject);
        }
    }


    public IEnumerator Tirage()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 30; i++)
        {
            tirB.Play();
            Instantiate(tir, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }

        anim.SetTrigger("rotation");
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Diament : MonoBehaviour
{



    public void OnCollisionEnter(Collision c)
    {


        if (c.gameObject.tag == "TirVaisseau")
        {

            if (!c.gameObject.GetComponent<TirVaisseau>().diamentcreated)
            {
                TirVaisseau t = c.gameObject.GetComponent<TirVaisseau>();
                TirVaisseau t2 = Instantiate(t, t.transform.position, Quaternion.identity);
                t2.transform.forward = Quaternion.Euler(0f, 15f, 0) * t.transform.forward;
                t2.diamentcreated = true;
                t.transform.forward = Quaternion.Euler(0f, -15f, 0) * t.transform.forward;

            }
        }
    }
}

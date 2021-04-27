using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Diament : MonoBehaviour
{
    public int numero;

    
    public void Diament1(TirVaisseau t)
    {
        
        TirVaisseau t2 = Instantiate(t, t.transform.position, Quaternion.identity);
        t2.transform.forward = Quaternion.Euler(0f, 15f, 0) * t.transform.forward;
        t2.diamentcreated = true;
        t.transform.forward = Quaternion.Euler(0f, -15f, 0) * t.transform.forward;
    }



    public void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.GetComponent<TirVaisseau>())
        {   if (!c.gameObject.GetComponent<TirVaisseau>().diamentcreated)
            {
                if (numero == 1)
                {
                    Diament1(c.gameObject.GetComponent<TirVaisseau>());
                }

            }
        }
    }
}

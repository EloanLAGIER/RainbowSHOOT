using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIamPower : MonoBehaviour
{
    public int nbrTir=0;
    public TirVaisseau megaTir;
    public void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "TirVaisseau")
        {
            if (!c.gameObject.GetComponent<TirVaisseau>().diamentcreated)
            {
                nbrTir += 1;
                Destroy(c.gameObject);
            }
        }

        if (nbrTir == 10)
        {
            TirVaisseau t = Instantiate(megaTir, transform.position, Quaternion.identity);
            t.diamentcreated = true;
            nbrTir = 0;
        }
    }

}

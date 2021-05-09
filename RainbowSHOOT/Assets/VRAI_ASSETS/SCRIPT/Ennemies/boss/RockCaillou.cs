using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCaillou : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TirVaisseau")
        {
            Destroy(collision.gameObject);
        }
    }
}

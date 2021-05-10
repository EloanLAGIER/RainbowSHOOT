using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCaillou : MonoBehaviour
{
    public GameObject hit;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TirVaisseau")
        {
            Destroy(collision.gameObject);
            Instantiate(hit, collision.transform.position, Quaternion.identity);
        }
    }
}

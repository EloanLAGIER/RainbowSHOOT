using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamentRainbow : MonoBehaviour
{
    public List<Material> tirs;
    public Material currentTir;
    public int cur;
    // Start is called before the first frame update
    void Start()
    {
        cur = Random.Range(0, tirs.Count);
        currentTir = tirs[cur];

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TirVaisseau")
        {
            collision.gameObject.GetComponentInChildren<Renderer>().material = currentTir;
        }
    }

    public void changetir()
    {
        cur = (cur + 1) % tirs.Count;
        currentTir = tirs[cur];
    }
}

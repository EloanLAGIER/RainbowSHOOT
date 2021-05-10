using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;



[ExecuteInEditMode]
public class LigthParameter : MonoBehaviour
{

    GameObject nocturne;
    GameObject nocturneLigth;
    GameObject crepuscule;
    GameObject crepusculeLigth;


    float nocturneW;

    float crepusculeW;

    float blend = 0;

    public IEnumerator Changement()
    {
        // yield return new WaitForSeconds(3f);


        float A = 1;
        float B = 0;

        for(int i =0; i<100; i++)
        {
            yield return new WaitForSeconds(0.2f);
            
            A =- i; //de A à B
            B =+ i;



        }

     
    }

    


    // Start is called before the first frame update
    void Start()
    {
        nocturneW = GameObject.Find("NocturneVolume").GetComponent<Volume>().weight;
        nocturneLigth = GameObject.Find("NocturneLigth");

        crepusculeW = GameObject.Find("CrepusculeVolume").GetComponent<Volume>().weight;
        crepusculeLigth = GameObject.Find("CrepusculeLigth");

        StartCoroutine("Changement");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

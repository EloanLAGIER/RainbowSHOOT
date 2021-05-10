using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


//[ExecuteInEditMode]
public class LigthParameter : MonoBehaviour
{

    private IEnumerator Coroutine;


    public GameObject[] nocturne;
    //GameObject nocturneLigth;
    public GameObject[] crepuscule;
    //GameObject crepusculeLigth;

    int nbParam = 2;
    float[] ligthParm;


    //float crepusculeW;

    //float blend = 0;

    float A, B, n;
    GameObject[] a, b;

    void Changement(GameObject[] atrans, GameObject[] btrans)  //de A to B
    {
         a = atrans;
         b = btrans;


            ligthParm[0] = a[1].GetComponent<Light>().intensity;
            ligthParm[1] = b[1].GetComponent<Light>().intensity;

        // yield return new WaitForSeconds(3f);
         A = 1;
         B = 0;
         n = 0;

        a[1].GetComponent<Light>().shadows = 0;

        b[0].GetComponent<Volume>().weight = 0;
        b[1].GetComponent<Light>().intensity = 0;

      
        b[0].SetActive(true);
        b[1].SetActive(true);


        //
        /*
            Debug.Log("baba");
            //yield return new WaitForSeconds(0.04f);
            Debug.Log("baba");

            A =- n; //de A à B
            B =+ n;

            a[0].GetComponent<Volume>().weight = A;
            a[1].GetComponent<Light>().intensity = A* ligthParm[0];

            b[0].GetComponent<Volume>().weight = B;
            b[1].GetComponent<Light>().intensity= B * ligthParm[1];

            n =+ 0.01f;
        */

        //
        //StartCoroutine("looper");
        InvokeRepeating("looper",0f, 0.1f);

        
        if (n == 1)
        {
            a[0].SetActive(false);
            a[1].SetActive(false);
            a[1].GetComponent<Light>().intensity = ligthParm[0];
            Debug.Log("marche");
        }


        //Debug.Log("marche");
    }

    

    void looper()
    {
        //yield return new WaitForSeconds(0.04f);
        //Debug.Log("baba" + n);

        A = 1- n; //de A à B
        B = n;

        a[0].GetComponent<Volume>().weight = A;
        a[1].GetComponent<Light>().intensity = A * ligthParm[0];

        b[0].GetComponent<Volume>().weight = B;
        b[1].GetComponent<Light>().intensity = B * ligthParm[1];

        n = n + 0.01f;
        if (n > 1) { //Debug.Log("end");
            CancelInvoke();
            a[0].SetActive(false);
            a[1].SetActive(false);
            a[1].GetComponent<Light>().intensity = ligthParm[0];

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        ligthParm = new float[nbParam];
    }

        /*
        nocturne[0] = GameObject.Find("NocturneVolume");//.GetComponent<Volume>().weight;
        nocturne[1] = GameObject.Find("NocturneLigth");

        crepuscule[0] = GameObject.Find("CrepusculeVolume");//.GetComponent<Volume>().weight;
        crepuscule[1]= GameObject.Find("CrepusculeLigth");
        */
//de la nuit a la journée
    public void ToDay()
    {
        if (nocturne[0].activeSelf == true)
        {
            Changement(nocturne, crepuscule);
        }
        else { Debug.Log("deja le jour"); }
    }
    //de la journee a la nuit

    public void ToNigth()
    {
        if (crepuscule[0].activeSelf == true)
        {
            Changement(crepuscule, nocturne);
        }
        else { Debug.Log("deja la nuit"); }
    }



    // Update is called once per frame

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundShip : MonoBehaviour
{

    public AudioSource tirBruit;
    public AudioClip brut;
    public AudioClip diament;


    public void SOUND()
    {
        float f = tirBruit.time;
        if (tirBruit.clip == diament)
        {
            tirBruit.clip = brut;
        }
        else
        {
            Debug.Log("je passe par ici");
            tirBruit.clip = diament;
        }
        tirBruit.time =f;
        tirBruit.Play();
    }


}

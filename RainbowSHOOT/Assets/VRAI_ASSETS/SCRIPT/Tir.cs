using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{
    public string mat;

    public void ChangeMaterial(Material m)
    {
        GetComponentInChildren<Renderer>().material = m;
        mat = m.name;
    }
}

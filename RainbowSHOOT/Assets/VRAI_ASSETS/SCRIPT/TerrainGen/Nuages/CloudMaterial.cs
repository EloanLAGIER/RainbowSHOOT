using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CloudMaterial : MonoBehaviour
{
    public int cloudResolution = 20;
    public float cloudHeigth;
    public Mesh cloudMesh;
    public Material cloudMaterial;
    private float _offset;
    private Camera _sceneCamera;
    private Matrix4x4 _cloudPosMatrix;
    public bool shadowCasting, ShadowReceive, useLigthProbe;

    private void OnEnable() //lorsque l'object est activé/cocher
    {
        _sceneCamera = Camera.main;
    }


    void Update()
    {
        //Debug.Log("Working");
        var CurrentTransform = transform;
        //parameter pour le material:
        cloudMaterial.SetFloat("WorldPosY", CurrentTransform.position.y);
        cloudMaterial.SetFloat("CloudHeigth", cloudHeigth);
        _offset = cloudHeigth / cloudResolution / 2f;
        var initPos = transform.position + (Vector3.up * (_offset * cloudResolution / 2f));
        
        //Une facon de dessiner le mesh a l'ecran sans game object:
        for (var i =0; i<cloudResolution; i++)
        {
            //Debug.Log("baba");
            _cloudPosMatrix = Matrix4x4.TRS(initPos - (Vector3.up * _offset * i),
                CurrentTransform.rotation, CurrentTransform.localScale);
            //push mesh data to render:
            Debug.Log(_cloudPosMatrix);
            Graphics.DrawMesh(cloudMesh, _cloudPosMatrix, cloudMaterial, 0, _sceneCamera, 0
                , null, shadowCasting, ShadowReceive, useLigthProbe);
        }
        Graphics.DrawMesh(cloudMesh, _cloudPosMatrix, cloudMaterial, 0);
    }
}

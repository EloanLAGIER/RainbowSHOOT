using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MGen_HugeWorld : MonoBehaviour
{
    public MGen_TerrainCreator TerrainCrea;
    GameObject[] Terrain;
    Vector2[] TilteNum = new Vector2[9];
    


    [Header("BasicMap Generation")]
    public int Resolution = 250;
    public float Size = 2500;

    public Texture2D baseHeigth;
    public Material mat;

    public Vector3 Pos = new Vector3(-2900, 1, 0);

    [Header("MediumMap")]
    public float AmpliMed = 10f;

    [Header("LargeMap")]
    public float Ampli = 50f;
    public float Offset = -50;
    public float Expo = 1.5f;


    int[] adaptRes = new int[] { 125, 250, 125, 50, 125, 50, 40, 40,40 };

    private void Start()
    {
        Terrain = new GameObject[9];
        float speed;
        int i = 0;


        for (int z = 0; z < 3; z++)
        {
            for (int x = 0; x < 3; x++)
            {
                Terrain[i] = new GameObject("terrain_"+(i+1));
                Terrain[i].AddComponent<MeshRenderer>();
                Terrain[i].AddComponent<MGen_TerrainCreator>();
                Terrain[i].GetComponent<Renderer>().material = mat;

                //take le script en cours:
                TerrainCrea = Terrain[i].GetComponent<MGen_TerrainCreator>();

                //Debug.Log(i);
                
                TerrainCrea.CreateShape(150, Size,
                    new Vector3(Size*x, 0, Size*z)+ Pos, baseHeigth);
                TerrainCrea.HeigthDeform(AmpliMed, Ampli,
                    Offset, Expo);

                speed = (0.02f * adaptRes[i]) / 250;

                TerrainCrea.MeshLaunch(speed);
                i++;
            }
        }
        //TerrainCrea.Test();

        //TerrainCrea.UpdateMesh();



    }



    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class MGen_HugeWorld : MonoBehaviour
{
    MGen_TerrainCreator TerrainCrea;
    
    GameObject[] Terrain;
    GameObject TerrainTest;

    Vector2[] TilteNum = new Vector2[9];
    


    [Header("BasicMap Generation")]
    public int Resolution = 250;
    public float Size = 2500;

    public Texture2D baseHeigth;
    public Material mat;
    public Vector3 Pos; 


    [Header("MediumMap")]
    public float AmpliMed = 10f;

    [Header("LargeMap")]
    public float Ampli = 50f;
    public float Offset = -50;
    public float Expo = 1.5f;


    int[] adaptRes = new int[] { 125, 250, 125, 50, 125, 50, 40, 40,40 };

    private void Start()
    {

        Tilting();
        //Solo();

    }


 void Tilting()
        {
        Terrain = new GameObject[9];
        //TerrainCrea=new MGen_TerrainCreator[9];

        float speed;
        int i = 0;

        for (int z = 0; z < 3; z++)
            {
                for (int x = 0; x < 3; x++)
                {
                    TilteNum[i] = new Vector2(x, z);

                    Terrain[i] = new GameObject("terrain_" + (i + 1));
                    Terrain[i].AddComponent<MeshRenderer>();
                    Terrain[i].AddComponent<MGen_TerrainCreator>();
                    Terrain[i].GetComponent<Renderer>().material = mat;

                    //take le script en cours:
                    TerrainCrea = Terrain[i].GetComponent<MGen_TerrainCreator>();

                    //Debug.Log(i);

                    TerrainCrea.CreateShape(125, Size,
                        new Vector3(Size * x, 0, Size * z) + Pos,
                        baseHeigth, TilteNum[i]);


                    TerrainCrea.HeigthDeform(AmpliMed, Ampli,
                        Offset, Expo, TilteNum[i]);

                    speed = (0.02f * adaptRes[x]) / 250;
                    TerrainCrea.MeshLaunch(speed);


                    i++;
                }
            }
        }
        
    void Solo()
    {
        Terrain = new GameObject[2];
        float speed;

        for (int i=0; i < 2; i++)
        {
            
            TilteNum[i] = new Vector2(0, i);

            Terrain[i] = new GameObject("terrain_0");
            Terrain[i].AddComponent<MeshRenderer>();
            Terrain[i].AddComponent<MGen_TerrainCreator>();
            Terrain[i].GetComponent<Renderer>().material = mat;

            TerrainCrea = Terrain[i].GetComponent<MGen_TerrainCreator>();

            TerrainCrea.CreateShape(170, Size,
                            new Vector3(Size, 0, Size*i) + Pos,
                            baseHeigth, TilteNum[i]);

         //   TerrainCrea.UpdateMesh();

            
            TerrainCrea.HeigthDeform(AmpliMed, Ampli,
                Offset, Expo, TilteNum[i]);
            
            speed = 0.02f;
            TerrainCrea.MeshLaunch(speed);
        }
    }

/*
        //Deform et MoveMesh apres la creation des objets/mesh
        
   for(int x =0; x< TerrainCrea.Length; x++)
        {
           //if (i + 3 < 9) { afterTilt = Terrain[i + 3].GetComponent<MGen_TerrainCreator>(); }
            else { afterTilt = null; }

            TerrainCrea[x].HeigthDeform(AmpliMed, Ampli,
                    Offset, Expo, TilteNum[x]);
        }
        for (int x = 0; x < TerrainCrea.Length; x++)
        {
            speed = (0.02f * adaptRes[x]) / 250;
            TerrainCrea[x].MeshLaunch(speed);
        }
      */


    

}

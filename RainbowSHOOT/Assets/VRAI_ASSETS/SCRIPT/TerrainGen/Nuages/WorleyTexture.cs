using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]



//Mix from keishiro: https://github.com/keijiro/CloudSkybox/blob/master/Assets/CloudSkybox/NoiseVolume.cs
//et la doc sur les texture 3d d'unity https://docs.unity3d.com/Manual/class-Texture3D.html
//bonus video generation map 3d similaire a la doc d'unity:https://www.youtube.com/watch?v=xImlGKpjU7o


[ExecuteInEditMode]
public class WorleyTexture : MonoBehaviour
{
    int Taille = 32;

    enum NoiseType { Perlin, Worley } // a comprendre a l'occasion le enum/ serializedfield...

    [SerializeField]
    NoiseType _noiseType = NoiseType.Perlin;

    [SerializeField]
    int _frequency = 1;

    [SerializeField]
    int _fractalLevel = 0;

    [SerializeField]
    int _seed;




    //MeshRenderer meshRenderer;
    //MeshFilter meshFilter;
    //Material material;
    Texture3D texture3d;

    // Start is called before the first frame update
    void Start()
    {

        texture3d = new Texture3D(Taille, Taille, Taille, TextureFormat.Alpha8, false);
        texture3d.wrapMode = TextureWrapMode.Clamp;
        GenerateWorley();

    }

    void GenerateWorley()
    {

        var scale = 1.0f / Taille;

        NoiseTools.NoiseGeneratorBase noise;
        if (_noiseType == NoiseType.Perlin)
            noise = new NoiseTools.PerlinNoise(_frequency, 1, _seed);
        else
            noise = new NoiseTools.WorleyNoise(_frequency, 1, _seed); //j'imagine que d'indiquer le type de noise reoriente les chemin du noise base a la suite...
        //mais ca me reste assez mysterieux comment tout s'echaine et ce correspond. a etudier



        Color[] buffer= new Color[Taille * Taille * Taille]; //le buffer est compté comme le tableau des pixel/color
        int index = 0;

        for (int ix = 0; ix < texture3d.depth; ix++)
        {
            var x = scale * ix;
            for (int iy = 0; iy < texture3d.height; iy++)
            {
                var y = scale * iy;
                for (int iz = 0; iz < texture3d.width; iz++)
                {
                    var z = scale * iz;

                    var c = _fractalLevel > 1 ?
                        noise.GetFractal(x, y, z, _fractalLevel) :
                        noise.GetAt(x, y, z);

                    buffer[index++] = new Color(c, c, c, c);

                }
            }
        }
        texture3d.SetPixels(buffer);

        texture3d.Apply();//ca applique les reglages

        AssetDatabase.CreateAsset(texture3d, "Assets/VRAI_ASSETS/SCRIPT/TerrainGen/Nuages/3DTexture.asset");//pour sauvegarder la texture en tant que asset.
    }


    
}

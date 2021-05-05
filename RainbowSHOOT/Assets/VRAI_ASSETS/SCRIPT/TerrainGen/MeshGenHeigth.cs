using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//[CustomEditor(typeof(MeshGene))]
//[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class MeshGenHeigth : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    Vector2[] uv;

    public int xSize = 20;
    public int zSize = 20;

    public Texture2D BaseHeigth;  // la doc sur les texture: https://docs.unity3d.com/ScriptReference/Texture.html

    //private Vector2 fract(Vector2 x) { return x - Mathf.Floor(x); }

    private float fract(float x) { return x - Mathf.Floor(x); }


    private Vector2 random2 (Vector2 st)
    {
        st = new Vector2 (Vector2.Dot(st, new Vector2(117.1f, 341.7f)),
                  Vector2.Dot(st, new Vector2(29.5f, 13.3f)));
        Vector2 x = new Vector2(Mathf.Sin(st.x), Mathf.Sin(st.y)) * 5.5453123f;
        return new Vector2(-1.0f, -1.0f) + 2.0f * (x - new Vector2(Mathf.Floor(x.y), Mathf.Floor(x.y)));
    }

    float noise(Vector2 st)
    {
        Vector2 i = new Vector2(Mathf.Floor(st.x), Mathf.Floor(st.y));
        Vector2 f = st - new Vector2(Mathf.Floor(st.x), Mathf.Floor(st.y));

        Vector2 u = f * f * (new Vector2(3f,3f) - 2f * f);

        return Mathf.Lerp(Mathf.Lerp(Vector2.Dot(random2(i + new Vector2(0.0f,0.0f)), f - new Vector2(0.0f,0.0f)),
                            Vector2.Dot(random2(i + new Vector2(1f, 0f)), f - new Vector2(1f, 0f)), u.x),
            Mathf.Lerp(Vector2.Dot(random2(i + new Vector2(1f, 0f)), f - new Vector2(1f, 0f)),
                            Vector2.Dot(random2(i + new Vector2(1f, 1f)), f - new Vector2(1f, 1f)), u.x),u.y);
    }




    void CreateShape ()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        uv = new Vector2[vertices.Length]; // Uv on va a la suite prendre la position du points divisé par la taille du plane pour le garder entre 0 et  comme un uv :D
        int i = 0;
        for (int z=0; z<= zSize; z++)
        {
            for (int x= 0; x<= xSize; x++)
            {
                uv[i] = new Vector2((float)x / xSize, (float)z / zSize);  //la precision du float est utile dans les cas ou deux integer sont divisé car il ne donnerai pas la bonn valeur (sans les .000014 ect...
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }

        }

        triangles = new int[xSize * zSize * 6];
        
        
        int vert = 0; //defini les numero des point pour le triangle
        uint tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {

                triangles[tris + 0] = vert;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        HeigthDeform();
    }

    void HeigthDeform() 
    {
        for (var i = 0; i < vertices.Length; i++)
        {
            //uv[i].x = fract(uv[i].x + Time.deltaTime/15.0f);
            
            Vector2 st = uv[i] * new Vector2(BaseHeigth.width, BaseHeigth.height);
            Vector2 l = st /3.0f;
            float y = BaseHeigth.GetPixel((int)l.x, (int)l.y).grayscale * 20.0f;
            y += Mathf.Clamp(BaseHeigth.GetPixel((int)st.x, (int)st.y).grayscale,0.7f,1.0f) * 40.0f;
            st *= 8.0f;
            y -= BaseHeigth.GetPixel((int)st.x, (int)st.y).grayscale * 3f;
            //y -= 100;

            vertices[i].y = y;
        }
    }



    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        
    }


    int Count =0;
    int tris = 0;

    void MoveMesh()
    {
        for (int i = 0; i < vertices.Length; i++) { vertices[i].z -= 1;}  //deplacement de 1 pour tous

        int LineMove =( xSize +1) * Count ;
        
        for (int x = 0; x < xSize+1; x++) //saut de la ligne en arriere
        {
           vertices[LineMove + x].z += zSize+1;
        }
        
        int vert = 0;
        //Debug.Log(LineMove + "line");
        int vert2 = ((Count+zSize) - (zSize+1) * ((Count+zSize) / (zSize+1)))   *(xSize+1); //modulo valeur ligne avant celle deplace

        
        int[] trianglesdel = triangles;
        
      
        
                for (int x = 0; x < xSize; x++)
                {
                     trianglesdel[tris + 0] = vert + vert2;
                     trianglesdel[tris + 1] = vert + LineMove;
                     trianglesdel[tris + 2] = vert + vert2 + 1;
                     trianglesdel[tris + 3] = vert + vert2 + 1;
                     trianglesdel[tris + 4] = vert + LineMove;
                     trianglesdel[tris + 5] = vert + LineMove +1 ;

                vert++;
                tris += 6;

                }
        if(tris == triangles.Length) { tris = 0; }
        
        Count += 1;
        if (Count == zSize+1) { Count = 0; }


        //mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = trianglesdel;
        mesh.RecalculateNormals();
        
    }
        

    public void Start()
    {
       
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();

        InvokeRepeating("MoveMesh", 0.0f, 0.02f);
    }

    /*  void Update()
      {

          for(int i=0; i < vertices.Length; i++)
          {
              /*uv[i].x = fract(uv[i].x + Time.deltaTime/15.0f);
              Vector2 s = uv[i] * new Vector2(BaseHeigth.width, BaseHeigth.height);
              float y = BaseHeigth.GetPixel( (int)s.x, (int)s.y).grayscale*20.0f;
              vertices[i].y = y;
              vertices[i].z -= 1;
              //MoveMesh();
              //float y = Mathf.Sin(vertices[i].z+Time.time);//Mathf.PerlinNoise(vertices[i].x , vertices[i].z)*5f;
              //y = noise(new Vector2(vertices[i].x/10f, vertices[i].z/10f + Time.time)) * 0.2f;
              //y= Mathf.PerlinNoise((vertices[i].x +Time.time ), vertices[i].z) * 4f; ;



          }

          //UpdateMesh();
          //MoveMesh();

          //mesh.Clear();
          mesh.vertices = vertices;
          mesh.triangles = triangles;
          //mesh.RecalculateBounds();

      } */

    /*
    void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i],0.01f);
            //Gizmos.DrawCube(vertices[i], new Vector3(0.1f, 0.1f, 0.1f));
        }
    }
    */
    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//[CustomEditor(typeof(MeshGene))]
//[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class MGen_TerrainCreator : MonoBehaviour
{
    Mesh mesh;

    public Vector3[] vertices;
    int[] triangles;
    Vector2[] uv;



    [Header("BasicMap Generation")]
    public float xSize;
    public float zSize;

    public Texture2D BaseHeigth;  // la doc sur les texture: https://docs.unity3d.com/ScriptReference/Texture.html
    GameObject AfterMesh;
    Mesh AfterTilt;

    private MGen_TerrainCreator AftTilt;

    /*private Vector2 fract(Vector2 x) { return x - Mathf.Floor(x); }
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
    */


    int resolution; //rester dans le multiple de 125, 62.5,125,250.  50 marche aussi... a eviter les chiffre a virgule... probleme de memoire..
    float xEcart =0;
    float zEcart =0;



    //---------------------------------MeshGeneration-Base-------------------------------------------

    public void CreateShape (int Resolution, float Size,Vector3 Coord, Texture2D baseHeigth )
    {
        //initialisation
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;


        resolution = Resolution;
        xSize = Size;
        zSize = Size;
        BaseHeigth =  baseHeigth;


        vertices = new Vector3[(resolution +1) * (resolution +1)];
        uv = new Vector2[vertices.Length]; // Uv on va a la suite prendre la position du points divisé par la taille du plane pour le garder entre 0 et  comme un uv :D
        xEcart = xSize / resolution ;
        zEcart =  zSize / resolution;
        

        //Vertex:
        int i = 0;
        for (int z=0; z<= resolution; z++)
        {
            for (int x= 0; x<= resolution; x++)
            {
                uv[i] = new Vector2((float)x / (float)resolution, (float)z / (float)resolution)*1;  //la precision du float est utile dans les cas ou deux integer sont divisé car il ne donnerai pas la bonn valeur (sans les .000014 ect...
                vertices[i] =Coord + new Vector3((float)xEcart * x, 0, zEcart * z);
                //Debug.Log(vertices[i]);
                i++;
            }

        }


        // Triangles:
        triangles = new int[resolution * resolution * 6];
        
        int vert = 0; //defini les numero des point pour le triangle
        uint tris = 0;

        for (int z = 0; z < resolution; z++)
        {
            for (int x = 0; x < resolution; x++)
            {

                triangles[tris + 0] = vert;
                triangles[tris + 1] = vert + resolution + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + resolution + 1;
                triangles[tris + 5] = vert + resolution + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        
        
        
       
        //end mesh Creation
    }




    //----------------------------------------Height Deform---------------------------------
    Vector2 l;
    Vector2 st;
    
    [Header("MediumMap")]
    float AmpliMed;

    [Header("LargeMap")]
    float Ampli;
    float Offset;
    float Expo;
    public Vector2 TilteNum;
    Vector2 lPass;


    public void HeigthDeform(float ampliMed, //medium
        float ampli, //large
        float offset, float expo, Vector2 tilteNum) //general
    {
        Ampli = ampli;
        AmpliMed = ampliMed;
        Offset = offset;
        Expo = expo;
        TilteNum = tilteNum;
       // AftTilt = afterTilt;//.GetComponent<MGen_TerrainCreator>() ;  //assignation du script/componement du tilte suivant.

        //Debug.Log(AftTilt.vertices[54]);



        for (var i = 0; i < vertices.Length; i++)
        {
            //uv[i].x = fract(uv[i].x + Time.deltaTime/15.0f);

            //Debug.Log((float)uv[i].x);

            Vector2 st = uv[i] * new Vector2(BaseHeigth.width, BaseHeigth.height);
            Vector2 l = (uv[i]+TilteNum)  * (new Vector2(BaseHeigth.width, BaseHeigth.height)) ;
            l /= 3f;
            //float y = BaseHeigth.GetPixel((int)l.x, (int)l.y).grayscale * 20.0f;
            //float y = Mathf.Clamp(BaseHeigth.GetPixel((int)st.x, (int)st.y).grayscale,0.7f,1.0f) *2.0f;

            //Vector2 s = st * 8.0f;
            //y -= BaseHeigth.GetPixel((int)s.x, (int)s.y).grayscale * 3f;
            //y -= 100;*/
            float y = BaseHeigth.GetPixel((int)l.x,(int)l.y).grayscale* Ampli;

            y += BaseHeigth.GetPixel((int)st.x, (int)st.y).grayscale * AmpliMed;  //version repeatable medium

            y = Mathf.Pow(y, Expo);
            y -= Offset;



            //m_Material.SetVector("UvNormal", s);

            vertices[i].y = y;
        }


        //Some info pour la preparation du movemesh (en deformation y)
       /* int n = (int)(TilteNum.x + TilteNum.y + 3);
        if (n <= 9) { AfterTilt = GameObject.Find("terrain_" + n); }
            else { AfterTilt = null; }
       */
        lPass = TilteNum + new Vector2(0,1);
        if (lPass.y == 3) { lPass = new Vector2(TilteNum.x, 0); }

        
        //AfterTilt = GameObject.Find("terrain_" + 1);
        //Vector3 baba = AfterTilt.GetComponent<MeshFilter>().mesh.vertices[50];
        //AfterMesh = GameObject.Find("terrain_" + 8);
        //AfterTilt = AfterMesh.GetComponent<MeshFilter>().mesh;
        
        UpdateMesh();
    }


    //------------------------------MoveMesh-------------------------------------

    int Count =0;
    int tris = 0;

    decimal lx;
    decimal ly;


    void MoveMesh()
    {
        
        
        //Debug.Log("!!!!!!!!!!!" +lPass);

        for (int i = 0; i < vertices.Length; i++) { vertices[i].z -= zEcart;}  //deplacement de 1 pour tous

        int LineMove =( resolution +1) * Count ;

        float y;
        int n;
        //Debug.Log(xSize);
        //Debug.Log(Count);
        /*
        if (TilteNum.y < 2)
        {
            for (int x = 0; x < resolution + 1; x++) //saut de la ligne en arriere
            {
                vertices[LineMove + x].z += zSize + zEcart;

                n = (resolution + 1) + LineMove + x;
                if (n > AfterTilt.vertices.Length) { n = 0+x; }
         
                
                y = AfterTilt.vertices[n].y;

                //float y = baba.y;// AftTilt.vertices[n].y;
                //Debug.Log(y);

                //vertices[LineMove + x].y = y;
            }
        }
        */

        //   if (TilteNum.y == 2)

        for (int x = 0; x < resolution + 1; x++) //saut de la ligne en arriere
        {
            vertices[LineMove + x].z += zSize + zEcart;

            //lPass.y = 0;
            /*n = (resolution + 1) + LineMove + x;
            if (n >= uv.Length) { n = 0 + x; }*/

            st = uv[LineMove + x] * new Vector2(BaseHeigth.width, BaseHeigth.height);
            l = (lPass + uv[LineMove + x]) * (new Vector2(BaseHeigth.width, BaseHeigth.height))/3;//ici pour la taille d'uv /3);
            /*l = lPass + uv[LineMove+x];
            lx = (decimal)(l.x * BaseHeigth.width )/ 3;
            ly = (decimal)(l.y * BaseHeigth.height )/ 3;*/
            y = BaseHeigth.GetPixel((int)l.x, (int)l.y).grayscale * Ampli;
            y += BaseHeigth.GetPixel((int)st.x, (int)st.y).grayscale * AmpliMed;

            y = Mathf.Pow(y, Expo);
            vertices[LineMove + x].y = y - Offset;


            //Debug.Log(lPass + uv[LineMove + x]);
        }
        
        
        int vert = 0;
        //Debug.Log(LineMove + "line");
        int vert2 = ((Count+resolution) - (resolution+1) * ((Count+resolution) / (resolution+1)))   *(resolution+1); //modulo valeur ligne avant celle deplace

        
        int[] trianglesdel = triangles;
        
      
        
                for (int x = 0; x < resolution; x++)
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
        if (Count == resolution+1) { Count = 0; lPass += new Vector2(0f, 1f); }
        if (lPass.y == 3f) { lPass.y = 0; }

        //Debug.Log(lPass.y);

        
        
        //mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = trianglesdel;
        mesh.RecalculateNormals();
        //mesh.RecalculateTangents();

        
    }






    public void UpdateMesh()
    {

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

    }


    public void MeshLaunch(float speed)
    {       /*
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        */
        InvokeRepeating("MoveMesh", 0.0f, 0.06f);// +speed);
    }





    //-----------------Gizmos/Debug-----------------------------

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
    /*
    void OnDrawGizmos()
    {
        if (vertices == null)
            return;
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position+ new Vector3(xSize/2, 0, zSize/2), new Vector3(xSize, 1, zSize));

    }
    */
}


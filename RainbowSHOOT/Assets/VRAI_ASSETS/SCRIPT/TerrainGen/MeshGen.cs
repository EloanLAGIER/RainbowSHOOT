using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//[CustomEditor(typeof(MeshGene))]
//[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class MeshGen : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    
    //private Vector2 fract(Vector2 x) { return x - Mathf.Floor(x); }

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



    // Start is called before the first frame update
    public void Start()
    {
        //Debug.Log("Baba is here");
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }




    void CreateShape ()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        int i = 0;
        for (int z=0; z<= zSize; z++)
        {
            for (int x= 0; x<= xSize; x++)
            {
                
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }

        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0; //defini les numero des point pour le triangle
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {

                triangles[tris + 0] = vert + 0;
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

    }

    
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        
    }

   
    void Update()
    {

        for(var i=0; i < vertices.Length; i++)
        {
            
            float y = Mathf.Sin(vertices[i].z+Time.time);//Mathf.PerlinNoise(vertices[i].x , vertices[i].z)*5f;
            y= noise(new Vector2(vertices[i].x/10f, vertices[i].z/10f + Time.time)) * 0.2f;
            //y= Mathf.PerlinNoise((vertices[i].x +Time.time ), vertices[i].z) * 4f; ;
            
            vertices[i].y = y;
        }

        mesh.vertices = vertices;

        mesh.RecalculateBounds();

    } 
    

    /*
    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for (int i =0; i< vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }*/
}


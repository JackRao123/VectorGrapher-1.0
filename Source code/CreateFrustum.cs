using System.Diagnostics;
using UnityEngine;

public class CreateFrustum : MonoBehaviour
{
    public Material material;



    void Start()
    {
        //Stopwatch sw = Stopwatch.StartNew();

       // makeFrustum(900, 400, 100, 50, 300, 300, 300, 0, 0, 90, 5, 5, 10);

     //   sw.Stop();
       // print("TIME TAKEN TO MAKE FRUSTUM =  " + sw.ElapsedMilliseconds);

    }

    public void makeFrustum(float height, float bottomRadius, float topRadius, int nbSides, float posX, float posY, float posZ, float rotationX, float rotationY, float rotationZ, float scaleX, float scaleY, float scaleZ)
    {
        transform.position = new Vector3(posX, posY, posZ);
        transform.eulerAngles = new Vector3(rotationX, rotationY, rotationZ);
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);




        Mesh mesh = new Mesh();
        float twoPi = 6.2831852f;




        int nbVerticesCap = nbSides + 1;

        // bottom + top + sides
        Vector3[] vertices = new Vector3[nbVerticesCap + nbVerticesCap + nbSides * 2 + 2];


        vertices[0] = new Vector3(0f, 0f, 0f);
        for (var idx = 1; idx <= nbSides; idx++)
        {
            float rad = (float)(idx) / nbSides * twoPi;
            vertices[idx] = new Vector3(Mathf.Cos(rad) * bottomRadius, 0f, Mathf.Sin(rad) * bottomRadius);
        }

        // Top cap
        vertices[nbSides + 1] = new Vector3(0f, height, 0f);
        for (var idx = nbSides + 2; idx <= nbSides * 2 + 1; idx++)
        {
            float rad = (float)(idx - nbSides - 1) / nbSides * twoPi;
            vertices[idx] = new Vector3(Mathf.Cos(rad) * topRadius, height, Mathf.Sin(rad) * topRadius);
        }

        // Sides
        int v = 0;
        for (var idx = nbSides * 2 + 2; idx <= vertices.Length - 4; idx += 2)
        {
            float rad = (float)v / nbSides * twoPi;
            vertices[idx] = new Vector3(Mathf.Cos(rad) * topRadius, height, Mathf.Sin(rad) * topRadius);
            vertices[idx + 1] = new Vector3(Mathf.Cos(rad) * bottomRadius, 0, Mathf.Sin(rad) * bottomRadius);
            v++;
        }
        vertices[vertices.Length - 2] = vertices[nbSides * 2 + 2];
        vertices[vertices.Length - 1] = vertices[nbSides * 2 + 3];


        int nbTriangles = nbSides + nbSides + nbSides * 2;
        int[] triangles = new int[nbTriangles * 3 + 3];

        // Bottom cap
        int tri = 0;
        int i = 0;

        while (tri < nbSides - 1)
        {
            triangles[i] = 0;
            triangles[i + 1] = tri + 1;
            triangles[i + 2] = tri + 2;
            tri++;
            i += 3;
        }
        triangles[i] = 0;
        triangles[i + 1] = tri + 1;
        triangles[i + 2] = 1;
        tri++;
        i += 3;

        // Top cap
        //tri++;
        while (tri < nbSides * 2)
        {
            triangles[i] = tri + 2;
            triangles[i + 1] = tri + 1;
            triangles[i + 2] = nbVerticesCap;
            tri++;
            i += 3;
        }

        triangles[i] = nbVerticesCap + 1;
        triangles[i + 1] = tri + 1;
        triangles[i + 2] = nbVerticesCap;
        tri++;
        i += 3;
        tri++;

        // Sides
        while (tri <= nbTriangles)
        {
            triangles[i] = tri + 2;
            triangles[i + 1] = tri + 1;
            triangles[i + 2] = tri + 0;
            tri++;
            i += 3;

            triangles[i] = tri + 1;
            triangles[i + 1] = tri + 2;
            triangles[i + 2] = tri + 0;
            tri++;
            i += 3;
        }


        mesh.vertices = vertices;
        mesh.triangles = triangles;


        GetComponent<MeshRenderer>().material = material;

        GetComponent<MeshFilter>().mesh = mesh;
    }



}

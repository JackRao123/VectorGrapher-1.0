using System.Collections.Generic;
using UnityEngine;

public class t3em : MonoBehaviour
{
    public GameObject gridPrefab;
    public GameObject parentGameObject;
    public List<GameObject> children = new List<GameObject>();
    public List<GameObject> gridScales = new List<GameObject>();






    float k = 1000;
    int n = 1;


    void spawnGrid(Vector3 position, Vector3 angle, string objectName, int scale, bool collider, GameObject parentObject)
    {
        //"bool collider", as in "True or false, does it have a collider"
        //scale - for the scale, this is how big the grid is. scale of 0 is default. the larger the scale, the larger the grid. grid of scale n+1 has a side length 10x bigger than a grid of scale n.
        //all grids of a particular scale are the same size.
        //'scale' starts at 0. the original grid has scale 0. can go up or down.

        GameObject gridObject = Instantiate(gridPrefab, position, Quaternion.Euler(angle.x, angle.y, angle.z));

        gridObject.transform.localScale = new Vector3(100 * Mathf.Pow(10, scale), 100 * Mathf.Pow(10, scale), 100 * Mathf.Pow(10, scale));


        //int afs = gridObject.transform.GetInstanceID();
        //print(afs);




        gridObject.name = objectName;






        //xz planes must have 'xz' in their name, for CameraControl.cs script to allow for zooming on it.

        if (collider == true)
        {
            gridObject.AddComponent<BoxCollider>();
        }
        gridObject.transform.parent = parentObject.transform;



        //make it a parent of the original xz grid so that making thenm dissapear does it to all of them.

        //the format for naming them is "xzPlane scale n (x,y)", where x,y is the coordinate of the tile in the world, n is scale n.


    }

    void populateTile(Vector3 gridPos, int tileScale, GameObject parentObject)
    {
        //tileScale is the scale of the grid that has the tile that we are populating
        //tilePos is the coordinates of the centre of the grid that has the tile we want to populate
        n = 1;

        for (int row = 0; row <= 9; row++)
        {
            for (int column = 0; column <= 9; column++)
            {
                //we will start at the top left, we go right, then down
                float width = Mathf.Pow(10, tileScale) * 1000f;
                Vector3 spawnPos = gridPos + new Vector3(0.45f * width, 0, 0.45f * width) + new Vector3(-1000 * row * Mathf.Pow(10, tileScale - 1), 0, -1000 * column * Mathf.Pow(10, tileScale - 1));
                spawnGrid(spawnPos, new Vector3(90, 0, 0), "xzMiniPlane " + n, tileScale - 1, true, parentObject);
                n = n + 1;
            }
        }


    }
    void populateArea(Vector3 topLeft, Vector3 bottomRight, GameObject parentObject)
    {

    }
    void dissapearScale(int scale)
    {
        string parentObjectName = "xzScale" + scale;
        GameObject parentGameObject = GameObject.Find(parentObjectName);



    }





    public void Start()
    {

        parentGameObject = GameObject.Find("xzPlane");

        GameObject scaleNegative2 = new GameObject("scale-2");
        scaleNegative2.transform.parent = parentGameObject.transform;

        GameObject scaleNegative1 = new GameObject("scale-1");
        scaleNegative1.transform.parent = parentGameObject.transform;

        GameObject scale0 = new GameObject("scale0");
        scale0.transform.parent = parentGameObject.transform;

        GameObject scale1 = new GameObject("scale1");
        scale1.transform.parent = parentGameObject.transform;

        GameObject scale2 = new GameObject("scale2");
        scale2.transform.parent = parentGameObject.transform;

        GameObject scale3 = new GameObject("scale3");
        scale3.transform.parent = parentGameObject.transform;




        //smallest scale has each tile = 0.01units in unity editor
        //largest scale has each tile 10000units, and is 1m units wide. entire grid is 1m x 1m
        gridScales.Add(scaleNegative2);
        gridScales.Add(scaleNegative1);
        gridScales.Add(scale0);
        gridScales.Add(scale1);
        gridScales.Add(scale2);
        gridScales.Add(scale3);



        populateTile(new Vector3(0, 0, 0), 1, scale1);
        populateTile(new Vector3(0, 0, 0), 2, scale2);

        for (int i = 1; i <= 6; i++)
        {

        }


        //creating scale, scale1....scale3 parents.

    }


    void Update()
    {   /*
        //adjustGridToCamera();
        if (Input.GetKeyDown(KeyCode.P))
        {
            populateTile(new Vector3(0, 0, 0), 0, scale0);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            populateTile(new Vector3(0, 0, 0), 1);
        }
        */


        /*
        spawnGrid(new Vector3(0,0,k),new Vector3 (90,0,0), ("xzPlane" + n), 0 ,true);
        k = k + 2000;
        n = n + 1;
        */


        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 bigGridPos = new Vector3(0, 0, k);
            foreach (GameObject item in children)
            {
                print(item.name);
            }

        }
    }
}

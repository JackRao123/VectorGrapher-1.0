using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class extendXZPlane : MonoBehaviour
{
    public GameObject gridPrefab;
    public GameObject parentGameObject;
    public List<GameObject> children = new List<GameObject>();
    public List<GameObject> grids = new List<GameObject>();



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


    void populateArea(Vector3 topLeft, Vector3 bottomRight, float prefabSideLength, GameObject parentObject)//parentObject is the object that all of these spawned grids will have as a parent.
    {

        int objectNum = 1;

        for (int row = 0; row < Mathf.Abs(bottomRight.x - topLeft.x) / prefabSideLength; row++)
        {
            for (int column = 0; column < Mathf.Abs(bottomRight.x - topLeft.x) / prefabSideLength; column++)
            {





                float horizontalShift = prefabSideLength * row;
                float verticalShift = prefabSideLength * column * -1f;

                Vector3 spawnPos = topLeft + new Vector3(horizontalShift, 0, verticalShift) + new Vector3(0.5f * prefabSideLength, 0, -0.5f * prefabSideLength);



                spawnGrid(spawnPos, new Vector3(90, 0, 0), "xzPlane " + objectNum, (int)(prefabSideLength / 10000), true, parentObject);





                objectNum++;


            }



        }
    }



    public void Start()
    {
        parentGameObject = GameObject.Find("xzPlane");

        GameObject scale10000 = new GameObject("scale10000");
        scale10000.transform.parent = parentGameObject.transform;

        GameObject scale1000 = new GameObject("scale1000");
        scale1000.transform.parent = parentGameObject.transform;


        grids.Add(scale1000);
        grids.Add(scale10000);


        //want 8 octants, each octant 100 x 100 x 100. "1" = 1000 editor units.
        // one gridPrefab = 10x10 = 10000x10000 editor units 



        populateArea(new Vector3(-100000, 0, 100000), new Vector3(100000, 0, -100000), 10000, scale10000);


        Stopwatch myStopWatch = new Stopwatch();
        myStopWatch.Start();

        //this one spawns the small grids (below)
        ////populateArea(new Vector3(-100000, 0, 100000), new Vector3(100000, 0, -100000), 1000, scale1000);

        myStopWatch.Stop();





    }

    private void Update()
    {
        float distanceFromXYPlane = Camera.main.transform.position.y;

        if (distanceFromXYPlane < 3000f)
        {
            grids[0].active = true;
            grids[1].active = false;
        }
        else
        {
            grids[0].active = false;
            grids[1].active = true;
        }
    }












}

using System.Collections.Generic;
using UnityEngine;

public class duplicateXZPlane : MonoBehaviour
{
    public GameObject gridPrefab;
    public GameObject parentGameObject;
    public List<GameObject> children = new List<GameObject>();
    public List<GameObject> gridScales = new List<GameObject>();



    float k = 1000;
    int n = 1;
    void buildIfNotBlocked(Vector3 position, Vector3 angle, string objectName, int scale, bool collider)
    {
        Ray rayToPlane = new Ray(Camera.main.transform.position, (position - Camera.main.transform.position).normalized);
        RaycastHit objectHit;
        Physics.Raycast(rayToPlane, out objectHit);
        if (objectHit.collider.name.Contains("xz") == false)
        {
            spawnGrid(position, angle, objectName, scale, collider);

        }
    }

    void spawnGrid(Vector3 position, Vector3 angle, string objectName, int scale, bool collider)
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

        gridObject.transform.parent = parentGameObject.transform;

        children.Add(gridObject);
        //make it a parent of the original xz grid so that making thenm dissapear does it to all of them.

        //the format for naming them is "xzPlane scale n (x,y)", where x,y is the coordinate of the tile in the world, n is scale n.


    }

    void populateTile(Vector3 gridPos, int tileScale)
    {
        //tileScale is the scale of the grid that has the tile that we are populating
        //tilePos is the coordinates of the centre of the grid that has the tile we want to populate

        for (int row = 0; row <= 9; row++)
        {
            for (int column = 0; column <= 9; column++)
            {
                //we will start at the top left, we go right, then down
                float width = Mathf.Pow(10, tileScale) * 1000f;

                spawnGrid(gridPos + new Vector3(0.45f * width, 0, 0.45f * width) + new Vector3(-1000 * row * Mathf.Pow(10, tileScale - 1), 0, -1000 * column * Mathf.Pow(10, tileScale - 1)), new Vector3(90, 0, 0), "xzMiniPlane " + n, tileScale - 1, true);
                n = n + 1;
            }

        }


    }

    void dissapearScale(int scale)
    {
        string parentObjectName = "xzScale" + scale;
        GameObject parentGameObject = GameObject.Find(parentObjectName);



    }

    bool hasChanged = false;
    float previousScale = 0;
    Vector3 originalCameraPos = Camera.main.transform.position;

    //top = +z, right = +x
    float boundaryTop = 5000;
    float boundaryRight = 5000;
    float boundaryBottom = 5000;
    float boundaryLeft = 5000;
    //assuming we have a 10000xby 10000x (unity cordinate) grid at the start, these are the boundaries. absolute value  distance from the origin.

    void adjustGridToCamera()
    {
        float camHeight = Camera.main.transform.position.y;



        //this is the perpendicular distance of the camera from the ground.

        //tiles should dissapear when their sidelenghts are less than 1/32x the width of the screen.

        // which is at 300x 10^scale   

        float scaleIndicator = Mathf.Floor(Mathf.Log10(camHeight / 300));

        // we only want to display tiles from grids that are of this scale or greater. 


        //0.01 units will be the smallest measurement for our vectors. so in the unity editor, this means coordinate of 1 will be smalest tile width, which is smallest scale = -2
        for (int scale = (int)scaleIndicator; scale >= -2f; scale--)
        {
            dissapearScale(scale);

        }
        Vector3 currentCameraPos = Camera.main.transform.position;
        Vector3 cameraMovement = currentCameraPos - originalCameraPos;

        if (scaleIndicator != previousScale)
        {
            bool scaleExists = false;
            foreach (GameObject scaleCategory in gridScales)
            {
                if (scaleCategory.name == "scale" + scaleIndicator)
                {
                    scaleExists = true;
                }


            }
            if (scaleExists == false)
            {
                GameObject scaleTemp = new GameObject("scale" + scaleIndicator);
                scaleTemp.transform.parent = parentGameObject.transform;
                gridScales.Add(scaleTemp);

                //add a bunch of grids to this parent, but only in the area that the camera is looking at  + a bit around it.

            }
            else
            {
                //if the 
            }



            print("@@@");
        }




        else if (cameraMovement.magnitude != 0)
        {
            if (cameraMovement.x > 0)
            {
                if (cameraMovement.y > 0)
                {
                    float distToAddRight = cameraMovement.x;
                    float distToAddTop = cameraMovement.y;

                    float numToBuildRight = Mathf.Ceil(distToAddRight / 1000);
                    float numToBuildTop = Mathf.Ceil(distToAddRight / 1000);
                    float width = 1000f;
                    for (int i = 0; i < numToBuildRight; i++)
                    {
                        Vector3 position = new Vector3(boundaryRight, 0, boundaryTop) + new Vector3(500, 0, 0) * i;
                        spawnGrid(position, new Vector3(90, 0, 0), ("xzPlane scale 0 (" + position.x + ", " + position.z + ")"), 0, true);




                    }







                    //buildIfNotBlocked();



                    //buildIfNotBlocked(Vector3 position, Vector3 angle, string objectName, int scale, bool collider)

                }
                else if (cameraMovement.y < 0)
                {

                }
            }
            else if (cameraMovement.x < 0)
            {
                if (cameraMovement.y > 0)
                {

                }
                else if (cameraMovement.y < 0)
                {

                }
            }



            float renderRange = camHeight * 5;


            //create some more grids in the area of the camera.
        }



        previousScale = scaleIndicator;


    }




    void Start()
    {

        parentGameObject = GameObject.Find("xzPlane");

        GameObject scale0 = new GameObject("scale0");
        scale0.transform.parent = parentGameObject.transform;

        GameObject scale1 = new GameObject("scale1");
        scale1.transform.parent = parentGameObject.transform;

        gridScales.Add(scale0);
        gridScales.Add(scale1);


        //creating scale0 and scale1 parents.

    }


    void Update()
    {
        adjustGridToCamera();
        if (Input.GetKeyDown(KeyCode.P))
        {
            populateTile(new Vector3(0, 0, 0), 0);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            populateTile(new Vector3(0, 0, 0), 1);
        }



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

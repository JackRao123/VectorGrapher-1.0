using UnityEngine;
using System;
using System.Diagnostics;

public class CameraControl : MonoBehaviour
{


    public GameObject vectorThicknessSlider;
    public GameObject scrollView;



    XAxisNumbers xAxisNumbersScript;
    YAxisNumbers yAxisNumbersScript;
    ZAxisNumbers zAxisNumbersScript;
    void zoomToCursor()

    {

        Ray rayToPlane = Camera.main.ScreenPointToRay(Input.mousePosition);
        // rayToPlane is the name of the ray, the above line shoots a ray towards where the cursor is pointing



        RaycastHit objectHit;
        //objectHit is a boolean (true or false), denotes if the ray rayToPlane hit something or not

        if (Physics.Raycast(rayToPlane, out objectHit)) //this shoots the ray and checks if it hit somehting.
        {
            if (objectHit.collider.name.Contains("xz") == true)
            {

                Vector3 hitPoint = objectHit.point;
                //hitPoint is the coordsinates of where the ray hits the object.

                float distance = Vector3.Distance(transform.position, hitPoint);

                float zoomSpeed = 0.1f * distance;

                Vector3 directionVector = (hitPoint - transform.position);
                directionVector = directionVector / Mathf.Sqrt(directionVector.x * directionVector.x + directionVector.y * directionVector.y + directionVector.z * directionVector.z);
                //this converts into unit vector. that means length of directionVector = 1

                directionVector = directionVector * zoomSpeed;
                //this makes the directionVector a bit longer, it is now x 0.1x distance, so it will zoom slower the more zoomed in you are

                Vector3 scrollDirection = rayToPlane.GetPoint(10);
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {


                    transform.position = new Vector3(transform.position.x + directionVector.x, transform.position.y + directionVector.y, transform.position.z + directionVector.z);


                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    directionVector = directionVector * -1f;

                    transform.position = new Vector3(transform.position.x + directionVector.x, transform.position.y + directionVector.y, transform.position.z + directionVector.z);


                    //old code   transform.position = Vector3.MoveTowards(transform.position, scrollDirection, Input.GetAxis("Mouse ScrollWheel")*zoomSpeed);
                }
            }
            // de bugging  print(objectHit.collider.gameObject.name);
            //make sure the only object with a collider is the xz plane!!! otherwise the zoom function will not work, it will just zoom onto random objects!!
        }

        else
        {

        }


    }


    //public bool mouseMoved = false;
    //public bool mouseStart = true;
    //those two variables are for rotatePerspective() [the old one, not the new one]
    /*this is old code, this rotated the camera around the origin. 
    void rotatePerspective()
         
    {

        Vector2 centreScreen = new Vector2(Screen.width / 2, Screen.height / 2);

        Ray rayToPivot = Camera.main.ScreenPointToRay(centreScreen);
        // rayToPivot is the name of the ray, the above line of code shoots a ray to the 'pivot point', which is where the middle of the screen is pointing towards.



        RaycastHit objectHit;
        //objectHit is a boolean (true or false), denotes if the ray rayToPlane hit something or not

        if (Physics.Raycast(rayToPivot, out objectHit)) //this shoots the ray and checks if it hit somehting.
        {
            if (objectHit.collider.name == "xzPlane")
            { 
                Vector3 hitPoint = objectHit.point;
                //this is the point on the xyplane that it hits (in unity its 'xz', but its supposed to be xy cuz unity flips it)
                Vector3 pivotCentre = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
                //Vector2 startPos = new Vector2 (0,0);
               
                if (mouseMoved == false)
                {
                    print(mouseMoved);
                    mouseMoved = true;
                    Vector3 startPos = Input.mousePosition;
                    
                }
                else
                {
                    Vector2 endPos = Input.mousePosition;
                    Vector2 mouseDelta = endPos - startPos;
                    print(mouseDelta);
                    //run code here 

                    float rotationRadius = Mathf.Sqrt((pivotCentre - transform.position).x*(pivotCentre - transform.position).x + (pivotCentre - transform.position).z*(pivotCentre-transform.position).z);
                    float rotationCircumference = rotationRadius * 2 * 3.14159f;
                    //dragging mouse the entire width of the screen should be 2 rotations
                    float rotationAngleX = ((mouseDelta.x) / (Screen.width)) * 360 * 2;
                    float rotationAngleY = ((mouseDelta.y) / (Screen.height)) * 360 * 2;
                    Vector3 radiusVector1 = new Vector3((transform.position - pivotCentre).x, 0f, (transform.position - pivotCentre).z);
                    //float rotationAngle = 
                    Vector3 radiusVector2 = Quaternion.Euler(0,rotationAngleX,0)*radiusVector1;

                    transform.position = (pivotCentre + radiusVector2);

                    Vector3 directionToTarget = (pivotCentre-transform.position).normalized;
                    //have to flip (x -1) so that it faces towards the point and not away.
                    Quaternion rotation = Quaternion.LookRotation(directionToTarget);
                   
                    

                    transform.rotation = rotation;
                    //print("Hitpoint = " + new Vector3(hitPoint.x, 0, hitPoint.z) + "pivotCentre = " + pivotCentre + "Radiusvector2 = " + radiusVector2);




                    //Transform target;



                    startPos = Input.mousePosition;
                    
                }
                  debugging 
                print(pivotCentre);
                GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                GameObject tempCube = Instantiate(cube1, pivotCentre, Quaternion.identity);
                tempCube.transform.localScale = new Vector3(50,50,50); 
                 
            }
        }
    }
    */



    void movePerspective()
    {
        float speed = 0.07f;
        float smallestDistance = (Camera.main.transform.position.y);
        if (smallestDistance < 100)
        {
            smallestDistance = 100f;

        }
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        transform.position += transform.right * speed * mouseDelta.x * smallestDistance * -1f;
        transform.position += transform.up * speed * mouseDelta.y * smallestDistance * -1f;
    }



    void rotatePerspective()
    {
        float speed = 2f;
        transform.Rotate(-1 * Input.GetAxis("Mouse Y") * speed, 0, 0);
        transform.Rotate(0, Input.GetAxis("Mouse X") * speed, (-1f * transform.rotation.eulerAngles.z));
    }

    public bool onScrollView = false;



    float oldTime;
    void Start()
    {
        Screen.fullScreen = false;



        xAxisNumbersScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<XAxisNumbers>();
        yAxisNumbersScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<YAxisNumbers>();
        zAxisNumbersScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ZAxisNumbers>();

        oldTime = Time.time;
    }

    // Update is called once per frame










    private Transform myCameraTransform;




    void Update()
    {






        if (Input.mousePosition.x > scrollView.transform.GetComponent<RectTransform>().rect.width * Screen.width / 1920f + 20)
        {
            float newTime = Time.time;



            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                zoomToCursor();
                if (newTime - oldTime > 1.0f)//this makes it so spawnAxisNumbersInRange only runs at most every 0.1s.
                {

                
                    
                    xAxisNumbersScript.spawnAxisNumbersAndDotsInRange();
                    yAxisNumbersScript.spawnAxisNumbersAndDotsInRange();
                    zAxisNumbersScript.spawnAxisNumbersAndDotsInRange();

                     xAxisNumbersScript.adjustAxisNumbersToFaceCamera();
                     yAxisNumbersScript.adjustAxisNumbersToFaceCamera();
                     zAxisNumbersScript.adjustAxisNumbersToFaceCamera();
                  

                }
             


            }

            if (Input.GetMouseButton(0))
            {
                movePerspective();
                if (newTime - oldTime > 0.5f)
                {
                    xAxisNumbersScript.spawnAxisNumbersAndDotsInRange();
                    yAxisNumbersScript.spawnAxisNumbersAndDotsInRange();
                    zAxisNumbersScript.spawnAxisNumbersAndDotsInRange();
                    xAxisNumbersScript.adjustAxisNumbersToFaceCamera();
                    yAxisNumbersScript.adjustAxisNumbersToFaceCamera();
                    zAxisNumbersScript.adjustAxisNumbersToFaceCamera();
                    oldTime = newTime;
                }
                

            }

               



            if (Input.GetMouseButton(1))
            {
                rotatePerspective();
              
            }

        }















    }






}


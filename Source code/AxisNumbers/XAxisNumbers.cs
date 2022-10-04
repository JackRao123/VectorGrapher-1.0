using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
public class XAxisNumbers : MonoBehaviour
{

    //to optimise this module, it might be better to just add/remove elements in the list rather than to completely get rid of them and make them again

    static List<GameObject> axisNumbers = new List<GameObject>();
    static List<GameObject> axisDots = new List<GameObject>(); // these are the dots that go right next to the axis numbers, that are ON the axis, that are a dot.
    bool axisNumbersAndDotsCreated = false;//whether or not the axis numbers and dots have been created or not.


    public Material axisDotMaterial;

    /*
    //static List<GameObject> axisNumbersIntervals = new List<GameObject>();//this list is reuqired to make some numbers dissapear when the camera zooms out.
    //e.g. when you are close up, 0,1,2,3... will be visible, when you zoom out, 0,5,10... will be visible, then 0,10,20... etc.


    NOTE - THIS COMMENTED BIT IS NOT NEEDED.


    //static List<GameObject> axisDotsIntervals = new List<GameObject>();//this list is reuqired to make some dots dissapear when the camera zooms out (same as above).


    */





    private void Start()
    {

        spawnAxisNumbersAndDotsInRange();
        adjustAxisNumbersToFaceCamera();


    }

    //this function can probably be adapted to the grid, to make the grid do the same thing.
    public void spawnAxisNumbersAndDotsInRange()   //experimental function, spawns only the numbers and dots that are in a certain range. note - it works
    {
        //this module has lots and lots of debugging output statements

        Stopwatch sw = Stopwatch.StartNew();
        sw.Start();

        float increment = 1;         //the increment between axis dots/axis numbers (changes based on how far the camera is.
        float lowerRange;
        float upperRange;        //lower and upper range are the coordinates up and down to which axis numbers shoul dbe rendered. (unity coordinates, not the vectorgrapher coordinates).
        float lowerRangeNumber;
        float upperRangeNumber;  //these are the upper and lower numbers to be rendered. the actual numbers.



        //float renderDistance = 2500f;//this is the distance from the camera that we should render. before this, we render. after this, we don't.
        //currently set to 2500. can change later.
        //actaully we can't use render distance, because as the user zoom's out this will stay the same and this is not good. instead we use an angle.

        float renderAngle = 0.1974f; //this is arctan(1/5) in radians.
        //this is the maximum angle that the line from number to line to camera, and line from number to line to the point that is closest on x-axis to camera, can make.

        float renderDistance = Mathf.Sqrt(Camera.main.transform.position.y * Camera.main.transform.position.y + Camera.main.transform.position.z * Camera.main.transform.position.z) / Mathf.Tan(renderAngle);
        lowerRange = Camera.main.transform.position.x - renderDistance;
        upperRange = Camera.main.transform.position.x + renderDistance;



        float cameraDistFromXAxis = Mathf.Sqrt(Camera.main.transform.position.y * Camera.main.transform.position.y + Camera.main.transform.position.z * Camera.main.transform.position.z);

        int decimalPlaces = 0;

        switch (cameraDistFromXAxis)
        {
            //smallest increment = 0.0001, largest increment = 100;

            case < 0.15f:
                increment = 0.0001f;
                decimalPlaces = 4;
                break;

            case < 1.5f:
                increment = 0.001f;
                decimalPlaces = 3;
                break;

            case < 15f:
                increment = 0.01f;
                decimalPlaces = 2;
                break;

            case < 150f:
                increment = 0.1f;
                decimalPlaces = 1;
                break;


            case < 1500:
                increment = 1;
                decimalPlaces = 0;
                break;

            case < 3000:
                increment = 2;
                decimalPlaces = 0;
                break;


            case < 7500:
                increment = 5;
                decimalPlaces = 0;
                break;
            case < 15000:
                increment = 10;
                decimalPlaces = 0;
                break;
            case < 30000:
                increment = 20;
                decimalPlaces = 0;
                break;

            case < 75000:
                increment = 50;
                decimalPlaces = 0;
                break;

            case >= 75000:
                increment = 100;
                decimalPlaces = 0;
                break;

        }

        if (lowerRange < 0)
        {
            lowerRangeNumber = -Mathf.Ceil(-lowerRange / 100 / increment) * increment;
        }
        else
        {
            lowerRangeNumber = Mathf.Ceil(lowerRange / 100 / increment) * increment;
        }

        if (upperRange < 0)
        {
            upperRangeNumber = -Mathf.Ceil(-upperRange / 100 / increment) * increment;
        }
        else
        {
            upperRangeNumber = Mathf.Ceil(upperRange / 100 / increment) * increment;
        }

        List<GameObject> newAxisNumbers = new List<GameObject>();
        List<GameObject> newAxisDots = new List<GameObject>();




        if (lowerRangeNumber < -1000)
        {
            lowerRangeNumber = -1000;
        }
        if (upperRangeNumber > 1000)
        {
            upperRangeNumber = 1000;
        }
      //  print($"Increment = {increment }\n, LowerRange = {lowerRange}\n, UpperRange = {upperRange}\n, LowerRangeNumber = {lowerRangeNumber}\n, UpperRangeNumber = {upperRangeNumber}");
        for (float i = lowerRangeNumber; i <= upperRangeNumber; i = i + increment)
        {


            float displayNumber = (float)Math.Round(i, decimalPlaces);

          //  print($"displaynumber =   {displayNumber}\n   I = {i}");



            newAxisNumbers.Add(createAxisNumber((displayNumber).ToString(), new Vector3(i * 100, 0, -50 * increment)));
            newAxisDots.Add(createAxisDot(new Vector3(i * 100, 0, 0)));


        }
        int x = 0;
        Stopwatch stopwatch = Stopwatch.StartNew();
        stopwatch.Start();
        foreach (GameObject item in axisNumbers)
        {

            DestroyImmediate(item, true);
            x++;
        }
        stopwatch.Stop();

     //   print("NUmber deleted =  " + x + "  time elapsed =   " + stopwatch.ElapsedMilliseconds);




        foreach (GameObject item in axisDots)
        {

            DestroyImmediate(item, true);

        }


        axisNumbers = newAxisNumbers;
        axisDots = newAxisDots;


     //   sw.Stop();
      //  print("total time taken for everything =   " + sw.ElapsedMilliseconds);


    }




    public void adjustAxisNumbersToFaceCamera() //adjusts the axis numbers to face the camera
    {
        if (axisNumbersAndDotsCreated == false)
        {
            spawnAxisNumbersAndDotsInRange();
            axisNumbersAndDotsCreated = true;
        }
        else
        {

            float newScale = 0.001f * Mathf.Sqrt(Camera.main.transform.position.y * Camera.main.transform.position.y + Camera.main.transform.position.z * Camera.main.transform.position.z); //localscale factora lready applied in calculation
            //print($"Newscale = {newScale}");





            float cameraX = Camera.main.transform.position.x;
            // float renderDistance = //constant;


            foreach (GameObject item in axisNumbers)
            {
                item.GetComponent<TextMesh>().transform.LookAt(Camera.main.transform.position);
                item.transform.localScale = new Vector3(-newScale, newScale, newScale);

            }

        }
    }

    public void adjustAxisDotSize()
    {
        if (axisNumbersAndDotsCreated == false)
        {
            spawnAxisNumbersAndDotsInRange();
            axisNumbersAndDotsCreated = true;
        }
        else
        {

            float newScale = 0.01010152f * Mathf.Sqrt(Camera.main.transform.position.y * Camera.main.transform.position.y + Camera.main.transform.position.z * Camera.main.transform.position.z); //localscale factora lready applied in calculation



            foreach (GameObject item in axisDots)
            {

                item.transform.localScale = new Vector3(-newScale, newScale * 0.1f, newScale);

            }

        }
    }


    GameObject createAxisNumber(string axisNumberText, Vector3 position)
    {
        /*
        This is how to create 3D text.  
        GameObject myObject = new GameObject(); //creating empty gameobject
        myObject.AddComponent<TextMesh>(); //giving it text properties
        myObject.GetComponent<TextMesh>().text = "TEST TEST TEST"; //assigning text
        */


        GameObject axisNumber = new GameObject();

        axisNumber.AddComponent<TextMesh>();

        var textMesh = axisNumber.GetComponent<TextMesh>();

        textMesh.text = axisNumberText;
        textMesh.color = Color.black;
        textMesh.fontSize = 300;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;

        axisNumber.transform.position = position;
        float scale = 0.3f;
        //when scale = 2.5, radius of each character = 50.
        axisNumber.transform.localScale = new Vector3(-scale, scale, scale); // the x Scale must be negative. this is so that it flips it so that it faces the camera, rather than away, because LookAt() makes it face away.

        axisNumber.transform.LookAt(new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z));


        return axisNumber;



    }



    List<GameObject> createAxisNumberList() // this module is not used anywhere.
    {

      //  var timer = new Stopwatch();
    //    timer.Start();

     
        for (int index = -1000; index <= 1000; index++)
        {
            axisNumbers.Add(createAxisNumber(index.ToString(), new Vector3(index * 100, 0, 50)));


        }
      //  timer.Stop();
    //    print("time taken = " + timer.Elapsed.ToString());


        return axisNumbers;

    }


    GameObject createAxisDot(Vector3 position)
    {
        GameObject axisDot = GameObject.CreatePrimitive(PrimitiveType.Cylinder);



        axisDot.GetComponent<MeshRenderer>().material = axisDotMaterial; 

        axisDot.transform.localScale = new Vector3(1, 0.1f, 1) * (Mathf.Sqrt(Camera.main.transform.position.y * Camera.main.transform.position.y + Camera.main.transform.position.z * Camera.main.transform.position.z) / 40);
        axisDot.transform.position = position;
        axisDot.transform.eulerAngles = new Vector3(0, 0, 90);



        return axisDot;



    }
    List<GameObject> createAxisDotList()
    {
        for (int index = -1000; index <= 1000; index++)
        {
             
            


        }


        return axisDots;



    } // this module is not used anwhere


}

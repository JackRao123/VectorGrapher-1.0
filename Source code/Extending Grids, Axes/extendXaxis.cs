using System;
using UnityEngine;


public class extendXaxis : MonoBehaviour
{
    //extendXAxis script contains the old scripts for the axis extension stuf,f it is omitted forom the other scripts.
    //newAxisShiftFuntion moves the axis(shifts it)
    //changeaxis size changes the diameter of the axis as the user zooms in/out/


    public GameObject xAxis;
    public GameObject xCone;

    public GameObject staticXAxis; //this is the static x axis that extends past x<0, that stands still




    public float tanTheta;
    public float tanTheta1;




    public void shiftAxis()
    {
        Vector3 expandVector;
        expandVector = transform.position;


        Vector3 moveVector = 0.5f * expandVector;


        xAxis.transform.localScale = new Vector3(10, Vector3.Magnitude(expandVector), 10);

        //xAxis.transform.position = moveVector;



    }
    void shift()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 axisTipPos = transform.position;
        Vector2 axisTipScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 boxScreenPos = Camera.main.WorldToScreenPoint(xCone.transform.position);

        //Vector2 originScreenPos = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));

        //this script will be attached to the axis tip.
        //axisTipScreenPos gives the position on the screen that the tip is on.


        //consider an imaginary line that runs vertically, positioned at  (30/32) x width of the screen from the left.
        //consider another one, running horizontally, (3/36) x the height of the screen from the top.
        //consider another one, running horizontally, (1/18) x the height of the screen from the bottom.
        //consider another one, running vertically, (5/32) x width of the screen from the left.
        //these 4 lines are the boundaries for the tips of the x and y axes. the tips should be at the lines of these boundaries. 
        float boundaryTop = Screen.height * 33 / 36;
        float boundaryBottom = Screen.height * 2 / 18;
        float boundaryLeft = Screen.width * 5 / 32;
        float boundaryRight = Screen.width * 30 / 32;

        float boundaryTopDist = (boundaryTop - Camera.main.WorldToScreenPoint(transform.position).y);
        float boundaryBottomDist = -1f * (boundaryBottom - Camera.main.WorldToScreenPoint(transform.position).y);
        float boundaryLeftDist = -1f * (boundaryLeft - Camera.main.WorldToScreenPoint(transform.position).x);
        float boundaryRightDist = (boundaryRight - Camera.main.WorldToScreenPoint(transform.position).x);

        Vector3 rightPos = axisTipPos + ((boundaryRight - axisTipScreenPos.x) / (axisTipScreenPos.x - boxScreenPos.x)) * axisTipPos;
        Vector3 leftPos = axisTipPos + ((boundaryLeft - axisTipScreenPos.x) / (axisTipScreenPos.x - boxScreenPos.x)) * axisTipPos;
        Vector3 topPos = axisTipPos + ((boundaryTop - axisTipScreenPos.y) / (axisTipScreenPos.y - boxScreenPos.y)) * axisTipPos;
        Vector3 bottomPos = axisTipPos + ((boundaryBottom - axisTipScreenPos.y) / (axisTipScreenPos.y - boxScreenPos.y)) * axisTipPos;


        Vector3 pos = new Vector3(0, 0, 0);



        Vector2 pointingDirection = axisTipScreenPos - boxScreenPos;

        if (pointingDirection.x > 0 && pointingDirection.y > 0)
        {
            if (boundaryTopDist > boundaryRightDist)
            {
                pos = rightPos;
            }
            else
            {
                pos = topPos;
            }
        }

        if (pointingDirection.x > 0 && pointingDirection.y < 0)
        {
            if (boundaryRightDist > boundaryBottomDist)
            {
                pos = bottomPos;

            }
            else
            {
                pos = rightPos;

            }
        }
        if (pointingDirection.x < 0 && pointingDirection.y > 0)
        {
            if (boundaryLeftDist > boundaryTopDist)
            {
                pos = topPos;

            }
            else
            {
                pos = leftPos;
            }
        }
        if (pointingDirection.x < 0 && pointingDirection.y < 0)
        {
            if (boundaryLeftDist > boundaryBottomDist)
            {
                pos = bottomPos;
            }
            else
            {
                pos = leftPos;
            }
        }





        /*


        
        if (boundaryTopDist == Mathf.Min(boundaryTopDist, boundaryBottomDist, boundaryLeftDist, boundaryRightDist))
        {
            pos = topPos;
        }
        if (boundaryBottomDist == Mathf.Min(boundaryTopDist, boundaryBottomDist, boundaryLeftDist, boundaryRightDist))
        {
            pos = bottomPos;
            
        }
        if (boundaryRightDist == Mathf.Min(boundaryTopDist, boundaryBottomDist, boundaryLeftDist, boundaryRightDist))
        {
            pos = rightPos;
        }
        if (boundaryLeftDist == Mathf.Min(boundaryTopDist, boundaryBottomDist, boundaryLeftDist, boundaryRightDist))
        {
            pos = leftPos;

        }
        
        try
        {
            transform.position = pos;
        }
        
        catch (Exception exception)
        {
            print("error!!");
        }

        */
        transform.position = pos;
    }

    public void changeAxisSize()
    {
        float newCylinderDiameter = tanTheta * 2 * MathF.Sqrt(Camera.main.transform.position.y * Camera.main.transform.position.y + Camera.main.transform.position.z * Camera.main.transform.position.z); //localscale factora lready applied in calculation
        xAxis.transform.localScale = new Vector3(newCylinderDiameter, xAxis.transform.localScale.y, newCylinderDiameter);
        staticXAxis.transform.localScale = new Vector3(newCylinderDiameter, staticXAxis.transform.localScale.y, newCylinderDiameter);


        float newConeDiameter = tanTheta1 * 2 * MathF.Sqrt(Camera.main.transform.position.y * Camera.main.transform.position.y + Camera.main.transform.position.z * Camera.main.transform.position.z);
        xCone.transform.localScale = new Vector3(newConeDiameter, newConeDiameter, xCone.transform.localScale.y);

    }
    void newAxisShiftFunction()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        float cameraDistFromCentre = Vector3.Magnitude(cameraPos);


        if (-50000f + 2f * cameraDistFromCentre < 50000)
        {
            xAxis.transform.position = new Vector3(-50000f + 2f * cameraDistFromCentre, 0, 0); //originally was -50000f + cameradist, changed to -50000 + 2cameradist 


            xCone.transform.position = new Vector3(2f * cameraDistFromCentre, 0, 0); //same as above comment ^

        }
        else
        {
            xAxis.transform.position = new Vector3(50000, 0, 0);


            xCone.transform.position = new Vector3(100000, 0, 0);
        }


    }



    void Start()
    {
        tanTheta = 5 / MathF.Sqrt(Camera.main.transform.position.y * Camera.main.transform.position.y + Camera.main.transform.position.z * Camera.main.transform.position.z);
        tanTheta1 = 350 / MathF.Sqrt(Camera.main.transform.position.y * Camera.main.transform.position.y + Camera.main.transform.position.z * Camera.main.transform.position.z);

        //this code only runs once so its pretty much just a constant
        //tanTheta = 0.00505076272276


    }


    void Update()
    {


        if ((Input.GetAxis("Mouse ScrollWheel") != 0f) || (Input.GetMouseButton(0)) || (Input.GetMouseButton(1)))
        {


            newAxisShiftFunction();


            changeAxisSize();

        }
    }
}

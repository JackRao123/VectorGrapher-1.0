using System;
using UnityEngine;


public class extendYaxis : MonoBehaviour
{

    public GameObject yAxis;
    public GameObject yCone;
    public GameObject staticYAxis; //this is the static y axis that extends past y<0, that stands still


    public float tanTheta;
    public float tanTheta1;





    public void changeAxisSize()
    {
        float newCylinderDiameter = tanTheta * 2 * MathF.Sqrt(Camera.main.transform.position.x * Camera.main.transform.position.x + Camera.main.transform.position.y * Camera.main.transform.position.y); //localscale factora lready applied in calculation
        yAxis.transform.localScale = new Vector3(newCylinderDiameter, yAxis.transform.localScale.y, newCylinderDiameter);
        staticYAxis.transform.localScale = new Vector3(newCylinderDiameter, staticYAxis.transform.localScale.y, newCylinderDiameter);

        float newConeDiameter = tanTheta1 * 2 * MathF.Sqrt(Camera.main.transform.position.x * Camera.main.transform.position.x + Camera.main.transform.position.y * Camera.main.transform.position.y);
        yCone.transform.localScale = new Vector3(newConeDiameter, newConeDiameter, yCone.transform.localScale.y);

    }
    void newAxisShiftFunction()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        float cameraDistFromCentre = Vector3.Magnitude(cameraPos);


        if (-50000f + 2f * cameraDistFromCentre < 50000)
        {
            yAxis.transform.position = new Vector3(0, 0, -50000f + 2f * cameraDistFromCentre);


            yCone.transform.position = new Vector3(0, 0, 2f * cameraDistFromCentre);

        }
        else
        {
            yAxis.transform.position = new Vector3(0, 0, 50000);


            yCone.transform.position = new Vector3(0, 0, 100000);
        }


    }






    void Start()
    {
        tanTheta = 5 / MathF.Sqrt(Camera.main.transform.position.x * Camera.main.transform.position.x + Camera.main.transform.position.y * Camera.main.transform.position.y);
        tanTheta1 = 350 / MathF.Sqrt(Camera.main.transform.position.x * Camera.main.transform.position.x + Camera.main.transform.position.y * Camera.main.transform.position.y);

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

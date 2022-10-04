using System;
using UnityEngine;


public class extendZaxis : MonoBehaviour
{

    public GameObject zAxis;
    public GameObject zCube;
    public GameObject staticZAxis; //this is the static z axis that extends past z<0, that stands still
    //xCube is supposed to be the "xCone", i dont know why it is called xCube, but i cbf to change it, too many references.

    public float tanTheta;
    public float tanTheta1;





    public void changeAxisSize()
    {
        float newCylinderDiameter = tanTheta * 2 * MathF.Sqrt(Camera.main.transform.position.x * Camera.main.transform.position.x + Camera.main.transform.position.z * Camera.main.transform.position.z); //localscale factora lready applied in calculation
        zAxis.transform.localScale = new Vector3(newCylinderDiameter, zAxis.transform.localScale.y, newCylinderDiameter);
        staticZAxis.transform.localScale = new Vector3(newCylinderDiameter, staticZAxis.transform.localScale.y, newCylinderDiameter);


        float newConeDiameter = tanTheta1 * 2 * MathF.Sqrt(Camera.main.transform.position.x * Camera.main.transform.position.x + Camera.main.transform.position.z * Camera.main.transform.position.z);
        zCube.transform.localScale = new Vector3(newConeDiameter, newConeDiameter, zCube.transform.localScale.y);

    }
    void newAxisShiftFunction()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        float cameraDistFromCentre = Vector3.Magnitude(cameraPos);


        if (-50000f + 2f * cameraDistFromCentre < 50000)
        {
            zAxis.transform.position = new Vector3(0, -50000f + 2f * cameraDistFromCentre, 0);


            zCube.transform.position = new Vector3(0, 2f * cameraDistFromCentre, 0);

        }

        else
        {
            zAxis.transform.position = new Vector3(0, 50000, 0);


            zCube.transform.position = new Vector3(0, 100000, 0);
        }


    }






    void Start()
    {
        tanTheta = 5 / MathF.Sqrt(Camera.main.transform.position.x * Camera.main.transform.position.x + Camera.main.transform.position.y * Camera.main.transform.position.y);
        tanTheta1 = 350 / MathF.Sqrt(Camera.main.transform.position.x * Camera.main.transform.position.x + Camera.main.transform.position.y * Camera.main.transform.position.y);
        GameObject zAxis = GameObject.Find("zAxis");
        GameObject zCube = GameObject.Find("zCube");
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

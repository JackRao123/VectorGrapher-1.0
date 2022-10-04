using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class CartesianPlaneHandler : MonoBehaviour
{
    VectorPlaneHandler vectorPlaneHandlerScript = new VectorPlaneHandler();

    private void Start()
    {
        vectorPlaneHandlerScript = GameObject.FindGameObjectWithTag("InputFieldTag1").GetComponent<VectorPlaneHandler>();
    }

    public List<float> parseCartesianPlane(string inputString)
    {

        inputString = inputString.ToLower();

        //note that SubString syntax is Substring(startIndex, length);


        if (inputString.Contains("x") == false)
        {
            inputString = "0x" + inputString;
        }
        string xCoeffString = inputString.Substring(0, inputString.IndexOf("x"));

        if (inputString.Contains("y") == false)
        {
            inputString = inputString.Insert(inputString.IndexOf("x") + 1, "0y");
        }
        string yCoeffString = inputString.Substring(inputString.IndexOf("x") + 1, inputString.IndexOf("y") - inputString.IndexOf("x") - 1);

        if (inputString.Contains("z") == false)
        {
            inputString = inputString.Insert(inputString.IndexOf("y") + 1, "0z");
        }
        string zCoeffString = inputString.Substring(inputString.IndexOf("y") + 1, inputString.IndexOf("z") - inputString.IndexOf("y") - 1);


        DataTable myDataTable = new DataTable();


        float xCoeffValue = (float)Convert.ToDouble(myDataTable.Compute(xCoeffString, null));
        float yCoeffValue = (float)Convert.ToDouble(myDataTable.Compute(yCoeffString, null));
        float zCoeffValue = (float)Convert.ToDouble(myDataTable.Compute(zCoeffString, null));


        string[] planeEquationParts = inputString.Split('=');

        float constantValue = (float)Convert.ToDouble(myDataTable.Compute(planeEquationParts[1].ToString(), null));



        List<float> returnList = new List<float>() { xCoeffValue,yCoeffValue, zCoeffValue, constantValue};
        return returnList;

     }







    public GameObject createGraph(float xCoeffValue, float yCoeffValue, float zCoeffValue, float constantValue)
    {

        // https://math.stackexchange.com/questions/1503248/converting-from-cartesian-to-vector-form

        // ax + by + cz = k is equivalent to      p = lambda(c, 0, -a) + mu(0, c, -b) + (0, 0, k/c)

        float a = xCoeffValue;  
        float b = yCoeffValue;
        float c = zCoeffValue;
        float k = constantValue;

   

        return vectorPlaneHandlerScript.createGraph(c, 0, -a, 0, c, -b, 0, 0, k / c, Color.red);

        //stub, make color.red the actual color.



    }
}

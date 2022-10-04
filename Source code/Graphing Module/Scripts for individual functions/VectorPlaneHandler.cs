using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using System.Linq;


public class VectorPlaneHandler : MonoBehaviour
{
    VectorHandler vectorHandlerScript;
    MainEquationHandler mainEquationHandlerScript;

    public Material graphMaterial;

    private void Start()
    {
        vectorHandlerScript = GameObject.FindGameObjectWithTag("InputFieldTag1").GetComponent<VectorHandler>();
        mainEquationHandlerScript = GameObject.FindGameObjectWithTag("InputFieldTag1").GetComponent<MainEquationHandler>();
    }


    



    public string[] parsePlaneIntoValues(string inputString)
    {
        //this parses something like p = (a,b,c) + lambda(x,y,z) + mu(p,q,r) into its values of a,b,c etc

        string[] planeEquationParts = inputString.Split(new[] { "=", "λ", "µ" }, StringSplitOptions.RemoveEmptyEntries);

   
        if (  planeEquationParts.Count() < 4)
        {
            planeEquationParts = new string[] { planeEquationParts[0], "(0,0,0)", planeEquationParts[1], planeEquationParts[2] };

        }


        string planeName = vectorHandlerScript.mainVectorParser(planeEquationParts[0].Replace(" ", ""));
        string positionVector = vectorHandlerScript.mainVectorParser(mainEquationHandlerScript.replaceAllFunctionsAndConstants(planeEquationParts[1].Replace(" ", "").Substring(0, planeEquationParts[1].Replace(" ", "").Length - 1)));
        string directionVector1 = vectorHandlerScript.mainVectorParser(mainEquationHandlerScript.replaceAllFunctionsAndConstants(planeEquationParts[2].Replace(" ", "").Substring(0, planeEquationParts[2].Replace(" ", "").Length - 1)));
        string directionVector2 = vectorHandlerScript.mainVectorParser(mainEquationHandlerScript.replaceAllFunctionsAndConstants(planeEquationParts[3].Replace(" ", "")));


        string[] positionVectorParts = positionVector.Replace("(", "").Replace(")", "").Split(",", StringSplitOptions.RemoveEmptyEntries);
        string[] directionVector1Parts = directionVector1.Replace("(", "").Replace(")", "").Split(",", StringSplitOptions.RemoveEmptyEntries);
        string[] directionVector2Parts = directionVector2.Replace("(", "").Replace(")", "").Split(",", StringSplitOptions.RemoveEmptyEntries);



        string[] returnArray = new string[] {planeName, positionVectorParts[0] , positionVectorParts[1], positionVectorParts[2], directionVector1Parts[0], directionVector1Parts[1], directionVector1Parts[2], directionVector2Parts[0], directionVector2Parts[1], directionVector2Parts[2] };
        return returnArray;

    }


    //p = (a,b,c) + lambda(x,y,z) + mu(p,q,r)

    public GameObject createGraph(float a, float b, float c, float x, float y, float z, float p, float q, float r, Color color)
    {


   

        

        GameObject planeGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);





         Plane plane = new Plane(new Vector3(a, c,b), new Vector3(a +x,  c + z,b + y), new Vector3(a+p  , c+r, b + q));


        print($" {new Vector3(a, c, b)  },  {new Vector3(a + x, c + z, b + y)},  { new Vector3(a + p, c + r, b + q) } ");



        //planeGameObject.transform.position = new Vector3(a, b, c);

        planeGameObject.transform.up = plane.normal;
        planeGameObject.transform.localScale = new Vector3(10000, 1, 10000);


        //planeGameObject.transform.GetComponent<MeshRenderer>().material.color = Color.red;
        planeGameObject.transform.GetComponent<MeshRenderer>().material = graphMaterial;
        planeGameObject.transform.GetComponent<MeshRenderer>().material.color = color;

        planeGameObject.GetComponent<Collider>().enabled = false;

        return planeGameObject;
    }

   
}

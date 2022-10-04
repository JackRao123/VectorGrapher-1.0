using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class CartesianSphereHandler : MonoBehaviour
{

    static int counter;
    //counter iterates every time for a new 

    public GameObject highDefinitionSphere;
    public Material gridMaterial;
    public Material colorMaterial;


    

 


    public List<float> parseSphere(string inputString)
    {
        //try parse into sphere

        //  (x-a)^2 + (y-b)^2 + (z-c)^2 = r^2

        string LHS = inputString.Split("=")[0];
        string RHS = inputString.Split("=")[1];

        //lhs is the (x-a)^2 stuff, rhs is the r^2


        string[] LHSParts = LHS.Split(new[] { "^2" }, StringSplitOptions.RemoveEmptyEntries);





        string xPart = LHSParts[0].Replace("(", "").Replace(")", "");
        string yPart = LHSParts[1].Replace("(", "").Replace(")", "");
        string zPart = LHSParts[2].Replace("(", "").Replace(")", "");

        DataTable dataTable2 = new DataTable();

        float xShift = 0;
        float yShift = 0;
        float zShift = 0;


        if (xPart.Replace("x", "").Replace(" ", "") != "")
        {
            xShift = -1f * (float)Convert.ToDouble(dataTable2.Compute(xPart.Replace("x", ""), null));
        }
        if (yPart.Replace("y", "").Replace(" ", "") != "+")
        {
            yShift = -1f * (float)Convert.ToDouble(dataTable2.Compute(yPart.Replace("y", ""), null));
        }
        if (zPart.Replace("z", "").Replace(" ", "") != "+")
        {
            zShift = -1f * (float)Convert.ToDouble(dataTable2.Compute(zPart.Replace("z", ""), null));
        }


        //lets say we have (x-a)^2. THen, the xShift is a. This is the amount that the sphere is shifted from origin on the x axis.


        float radius = Mathf.Sqrt(float.Parse(RHS));

        List<float> parts = new List<float>() { xShift, zShift, yShift, radius }; //need to swap zshift and yshift 
  

     
        return parts;








    }
     

    public GameObject graphSphere(float xShift, float yShift, float zShift, float radius, Color color)
    {
       GameObject newSphere = GameObject.Instantiate(highDefinitionSphere);

        //GameObject newSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere); 


        try
        {
            newSphere.GetComponent<Collider>().enabled = false;
        }
        catch (Exception ex)
        {

        }


        /*
        Color colour = new Color();

        colour.r = 255;
        colour.g = 0;
        colour.b = 0;
        colour.a = 245;

        */


        color.a = 250;


        //r,g,b,a components.  a = opacity. 0 = invisible,  255 = solid. 

        newSphere.transform.GetComponent<MeshRenderer>().materials = new Material[2] {gridMaterial, colorMaterial };



        newSphere.transform.GetComponent<MeshRenderer>().material.color = color;


 



        newSphere.transform.position = new Vector3(xShift * 100, yShift * 100, zShift * 100);
        newSphere.transform.localScale =  new Vector3(  radius * 100, radius * 100, radius * 100);
        newSphere.transform.name = "SPHERE";





        return newSphere;














    }



}

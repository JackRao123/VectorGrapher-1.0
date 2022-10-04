using UnityEngine;
using System;
 
using System.Collections.Generic;
using System.Data;

public class VectorLineHandler : MonoBehaviour
{

    static System.Random random = new System.Random();
    public Material graphMaterial;

    VectorHandler vectorHandlerScript;
    MainEquationHandler mainEquationHandlerScript;


    // Start is called before the first frame update
    void Start()
    {
        vectorHandlerScript = GameObject.FindGameObjectWithTag("NewFieldButton").GetComponent<VectorHandler>();
        mainEquationHandlerScript = GameObject.FindGameObjectWithTag("NewFieldButton").GetComponent<MainEquationHandler>();
    }

    // Update is called once per frame
 

    public GameObject graphVectorLine(string name, float positionX, float positionY, float positionZ, float directionX, float directionY, float directionZ, Color color)
    {
        positionX = positionX * 100;
        positionY = positionY * 100;
        positionZ = positionZ * 100;
        directionX = directionX * 100;  
        directionY = directionY * 100;
        directionZ = directionZ * 100;


        GameObject vectorLine;
         
        vectorLine = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        vectorLine.GetComponent<Collider>().enabled = false;

        /*
        //a list of colors
        List<List<int>> colorList = new List<List<int>>();
        colorList.Add(new List<int> { 255, 0, 0 });
        colorList.Add(new List<int> { 0, 255, 0 });
        colorList.Add(new List<int> { 0, 0, 255 });
        colorList.Add(new List<int> { 255, 255, 0 });
        colorList.Add(new List<int> { 0, 255, 255 });
        colorList.Add(new List<int> { 255, 0, 255 });
        //red, lime, blue, yellow, cyan, magenta
        */
        vectorLine.GetComponent<Collider>().enabled = false;

       
 



        vectorLine.transform.localScale = new Vector3( 8,346400,8);



        vectorLine.transform.position = new Vector3(positionX, positionZ, positionY);

        vectorLine.transform.up = new Vector3(directionX, directionZ, directionY); //makes the vector point in the direction that its supposed to


        //int randomNum = random.Next(0, 5); // must change this if adding new colors - must change to random.Next(0,k), k = num of colors - 1
        //List<int> selectedColor = colorList[randomNum];

        vectorLine.transform.GetComponent<MeshRenderer>().material = graphMaterial;

        vectorLine.transform.GetComponent<MeshRenderer>().material.color = color;
        //vectorLine.transform.GetComponent<MeshRenderer>().material.color = new Color(selectedColor[0], selectedColor[1], selectedColor[2]); //there shall be a selection of colors, a random color will be selected every time.


        vectorLine.active = true;





        

        return vectorLine;


    }


    public string parseVectorLine(string inputString) //bad parser, only half-parses. 
    {

       


        inputString = inputString.Replace(" ", "");
        string tempString = inputString;

        string[] equationParts = inputString.Split(new[] { "=", "λ" }, StringSplitOptions.RemoveEmptyEntries); //separates the line into 3 parts- l(a) = (x,y,z) + ?(a,b,c)  is separated into l(a), (x,y,z), (a,b,c).
                                                                                                               //[0] = name, [1] = position vector, [2] = direction vector.
                                                                                                               //i.e. l(a) = (1,1,1) + ?(3,1,5)
                                                                                                               //sometimes user might put the other way round, we need to swap [1] and [2] if this is the case.
                                                                                                               //however first we have to check if they included a position vector at all.



        if (equationParts.Length == 2)
        {
            equationParts[0] = equationParts[1].Remove(equationParts[0].Length - 1);

            if (inputString.Substring(inputString.IndexOf(equationParts[1]), equationParts[1].Length) != inputString.Substring(inputString.IndexOf("λ") + 1, equationParts[1].Length))
            {
                string temp = equationParts[0];
                equationParts[0] = equationParts[1];
                equationParts[1] = temp;


            }
        }




        bool containsPositionVector = false;
         



        string positionVector = "(0,0,0)"; //sets positionVector and newpositionVectorparts to default values.
        string[] newPositionVectorParts = { "0", "0", "0" };  //position vector values, x,y,z

        if (equationParts.Length == 2) //if a position vector is defined, it sets it to the value. otherwise, it stays at {0,0,0}
        {


            positionVector = equationParts[0];

            containsPositionVector = true;
        }

      
        string equationBody = equationParts[0]; //equationParts[1] = equation

     

        //equationName = equationParts[0];
        string directionVector = equationParts[1];


        string newPositionVector = vectorHandlerScript.mainVectorParser(  mainEquationHandlerScript.  replaceAllFunctionsAndConstants(positionVector));
        print($"Positionvector = {positionVector}, newpositionvector = {newPositionVector}");
        string newDirectionVector = vectorHandlerScript.mainVectorParser(mainEquationHandlerScript.replaceAllFunctionsAndConstants(directionVector));
 


 
 

 

        string[] directionVectorParts = newDirectionVector.Replace("(", "").Replace(")", "").Split(",");
        string[] positionVectorParts = newPositionVector.Replace("(", "").Replace(")", "").Split(",");




        string outputString = $"{newPositionVector} + λ{newDirectionVector}";

        return outputString; 
    }

}

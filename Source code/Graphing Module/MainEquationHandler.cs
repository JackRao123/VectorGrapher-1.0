using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;


using System.Collections.Generic;
using System.Data;


public class MainEquationHandler : MonoBehaviour
{
    static System.Random rand = new System.Random();




    static int nonVectorCounter = 0;
    // if a graph isnt a vector, it needs a number assigned for the name - this produces the number.



    //this script, MainEquationHandler is the main script that handles all the equations
    //it is the script that the inputField data is passed to 
    //it replaces all functions and constants in a string, from a dictionary that is kept in this script
    //it determines what type of equation the input is - Vector, Sphere, plane, error, etc.
    //it then parses it enough to extract the 'information' (such as vector name, vector body, vector tail expressions) from the equation, and then passes it to the respective parser(stored in other files).
    //the parser then passes the parsed values back to this script.
    //then, this script calls the GRAPH function of the repsective type of equatino.
    //this script also keeps a dictionary of all constants, equations, and gameobjects.
    /*

    public CartesianCircleHandler cartesianCircleHandlerScript;
    public CartesianLineHandler cartesianLineHandlerScript;
    public CartesianPlaneHandler cartesianPlaneHandlerScript;
    public CartesianSphereHandler cartesianSphereHandlerScript;
   
    public VectorLineHandler vectorLineHandlerScript;
    public VectorPlaneHandler vectorPlaneHandlerScript;
    public VectorSphereHandler vectorSphereHandlerScript; */
    VectorHandler vectorHandlerScript;
    VectorLineHandler vectorLineHandlerScript;
    VectorPlaneHandler vectorPlaneHandlerScript;
    VectorSphereHandler vectorSphereHandlerScript;
    CartesianSphereHandler cartesianSphereHandlerScript;
    CartesianPlaneHandler cartesianPlaneHandlerScript;






    GameObject outputField;


    void Start()
    {

        vectorHandlerScript = GameObject.FindGameObjectWithTag("InputFieldTag1").GetComponent<VectorHandler>();
        vectorLineHandlerScript = GameObject.FindGameObjectWithTag("InputFieldTag1").GetComponent<VectorLineHandler>();
        vectorPlaneHandlerScript = GameObject.FindGameObjectWithTag("InputFieldTag1").GetComponent<VectorPlaneHandler>();
        vectorSphereHandlerScript = GameObject.FindGameObjectWithTag("InputFieldTag1").GetComponent<VectorSphereHandler>();
        cartesianSphereHandlerScript = GameObject.FindGameObjectWithTag("InputFieldTag1").GetComponent<CartesianSphereHandler>();
        cartesianPlaneHandlerScript = GameObject.FindGameObjectWithTag("InputFieldTag1").GetComponent<CartesianPlaneHandler>();

    }






    public static List<equation> equations = new List<equation>();//this holds all the equations.





    public string replaceAllFunctionsAndConstants(string inputString)
    {

        string outputString = inputString;


        foreach (equation item in equations)
        {


            if (item.type == "vector")
            {
                if (outputString.Contains(item.name))
                {
                    outputString = outputString.Replace(item.name, item.body);

                }
            }

            if (item.type == "vectorLine")
            {
                if (outputString.Contains(item.name))
                {
                    outputString = outputString.Replace(item.name, $"{item.tail} + λ{item.body}");
                }
            }



        }


        return outputString;



    }
     
Color ensureNoDuplicates(  string inputString) //ensures the user can't create lots of duplicates of a single graph from a single input field. //inputstring is what is being held in the input field shown to user
    {

        Color myColor = new Color();
        List<List<int>> colorList = new List<List<int>>();
        colorList.Add(new List<int> { 255, 0, 0 });
        colorList.Add(new List<int> { 0, 255, 0 });
        colorList.Add(new List<int> { 0, 0, 255 });
        colorList.Add(new List<int> { 255, 255, 0 });
        colorList.Add(new List<int> { 0, 255, 255 });
        colorList.Add(new List<int> { 255, 0, 255 });
        //red, lime, blue, yellow, cyan, magenta
        int randomNum = rand.Next(0, 5); // must change this if adding new colors - must change to random.Next(0,k), k = num of colors - 1
        List<int> selectedColor = colorList[randomNum];
        myColor.r = selectedColor[0] /255f;
        myColor.g = selectedColor[1] / 255f;
        myColor.b = selectedColor[2] / 255f;
        myColor.a = 1f;



        foreach (equation item in equations)
        {
            if (item.equationString.Replace(" ", "") == inputString.Replace(" ", "")) //if it exists
            {
                //Color myColor = item.vector.transform.GetComponent<Color>();
                myColor = item.vector.transform.GetComponent<MeshRenderer>().material.color;
              
                try //this is if it is a sphere
                {
                    myColor = item.vector.transform.GetComponent<MeshRenderer>().materials[1].color;
                }
                catch(Exception e)
                {
                    
                }
                  


                
                GameObject vectorToDelete = item.vector;
                DestroyImmediate(vectorToDelete, true);
                try
                {
                    GameObject vectorTipToDelete = item.vectorTip;
                    DestroyImmediate(vectorTipToDelete, true);
                }
                catch (Exception ex)
                {
                    
                }


                equations.Remove(item); //removes it from the dictionary  
                break;

            }
        }

        return myColor;

    }
    public void fieldInputChanged(string inputString)// this code will run when the input is changed
    {


        Color objectColor = ensureNoDuplicates(inputString); //THIS IS EXTREMELY NECESSARY 

        //print("the name of the input field is" + transform.name);
        //the following code determines what type of input the user has given - and then does the correct processing to it.
        //  print(equations.Count);
        //  for (int i = 0; i < equations.Count; i++)
        // {
        //     print("asdf" + equations[i].name);
        // }





        string equationType = "null";

        Regex vectorRegex = new Regex(@"[vV]\(([a-z]|[A-Z]|[0-9])+\)=");//v(a), v(A), v(1) valid. V(...) also valid. combinations of these are also valid.
        Regex vectorLineRegex = new Regex(@"[lL]\(([a-z]|[A-Z]|[0-9])+\)=");
        Regex vectorPlaneRegex = new Regex(@"[pP]\(([a-z]|[0-9])+\)=");

        //probably cant make a regex for vector sphere, because they might put something like /"(3-2.32)|v-(x,y,z)| = r", so it will be difficult to accomodate the term at the front
        //alternatively, we can have the regex attempt to match ")| = r".
        //below is an attempt. Also, we match [a-z] because they might have the radius as a vairable
        //such as |v-(x,y,z)| = k, and then they can change the value of k to change the sphere radius
        Regex vectorSphereRegex = new Regex(@"(\|[vV]\(([0-9]|[a-z])\))|(\|[vV]-)");
        //|v-(x,y,z)| is now valid, before it had to be |v(a) - (x,y,z)|
        Regex pointRegex = new Regex(@"^\(");

        /*
        if (pointRegex.IsMatch(inputString.Replace(" ", "")))
        {
            equation currentEquation = new equation();
            currentEquation.equationString = inputString;
            currentEquation


        }
        */
        if (vectorRegex.IsMatch(inputString.Replace(" ", "")))  //if the user's entered input is a VECTOR.
        {

            equation currentEquation = new equation();



            inputString = inputString.Replace(" ", "");
            string tempString = inputString;

            string[] equationParts = inputString.Split(new[] { "=", ",{" }, StringSplitOptions.RemoveEmptyEntries); //separates the vector into 3 parts- name, vector, tail vector.

            bool containsTail = false;

            string equationTail = "{0,0,0}"; //sets equationTail and newEquationTailParts to default values.
            string[] newEquationTailParts = { "0", "0", "0" };  //tail Pos values, x,y,z

            if (inputString.Contains("{")) //if a tail is defined, it sets it to the value. otherwise, it stays at {0,0,0}
            {
                equationTail = equationParts[2];
                equationTail = "{" + equationTail;
                containsTail = true;
            }

            string equationName = equationParts[0]; //equationParts[0] = name of equation.
            string equationBody = equationParts[1]; //equationParts[1] = equation
             


            foreach (equation item in equations)
            {
                if (item.equationString == inputString.Replace(" ", ""))
                {
                    currentEquation = item;
                    print("asdfdaf");

                    equations.Remove(item); //removes it from the dictionary but it doesn't actually destroy the vector.
                }
            }
            currentEquation.equationString = inputString;
            currentEquation.field = transform.name;
            currentEquation.type = "vector";
            currentEquation.name = equationName;



            string newEquationBody = replaceAllFunctionsAndConstants(equationBody); // THIS LINE IS THE SOURCE OF MOST ISSUES 11/07/2022
                                                                                    //works normally, but when you try edit a vector, because the vector already exists in the dictionary, when it is edited, this results in a 'cannot enumerate due to iteration variable change'.
                                                                                    //to counteract this, we just need to remove it from the dictionary every time it is processed. then add it back in at the end.
                                                                                    //   print(newEquationBody);
            equationBody = vectorHandlerScript.mainVectorParser(newEquationBody);
            currentEquation.body = equationBody;


            if (equationTail != "{0,0,0}")
            {

                equationTail = equationTail.Replace("{", "(").Replace("}", ")");
                equationTail = replaceAllFunctionsAndConstants(equationTail);
                equationTail = vectorHandlerScript.mainVectorParser(equationTail).Replace("{", "(").Replace("}", ")");

                newEquationTailParts = equationTail.Replace("(", "").Replace(")", "").Split(",");
            }

            currentEquation.tail = equationTail;

            bool equationFoundInDict = false;
            int currentEquationIndex = 9999;
            foreach (equation item in equations)
            {
                if (item.name == equationName)
                {
                    currentEquationIndex = equations.IndexOf(item);


                    equations[currentEquationIndex] = currentEquation;
                    equationFoundInDict = true;

                }
            }
            if (equationFoundInDict == false)
            {
                equations.Add(currentEquation);
                currentEquationIndex = equations.IndexOf(currentEquation);
            }


            GameObject vectorToDelete = equations[currentEquationIndex].vector;
            DestroyImmediate(vectorToDelete, true);
            GameObject vectorTipToDelete = equations[currentEquationIndex].vectorTip;
            DestroyImmediate(vectorTipToDelete, true);

            GameObject[] storageArray = new GameObject[2];


            try
            {
                string[] newEquationParts = equationBody.Replace("(", "").Replace(")", "").Split(",");
                storageArray = (GameObject[])vectorHandlerScript.vectorGraph(equationName, float.Parse(newEquationParts[0]), float.Parse(newEquationParts[1]), float.Parse(newEquationParts[2]), float.Parse(newEquationTailParts[0]), float.Parse(newEquationTailParts[1]), float.Parse(newEquationTailParts[2]), objectColor);

                GameObject currentVector = storageArray[0];
                GameObject currentVectorTip = storageArray[1];


                currentEquation = equations[currentEquationIndex];
                currentEquation.vector = storageArray[0];
                currentEquation.vectorTip = storageArray[1];
                // print($"equation index = {currentEquationIndex}");
                equations[currentEquationIndex] = currentEquation;

                /*
                print($"" +
                   $"EquationString : {currentEquation.equationString}\n" +
                   $"Field : {currentEquation.field}\n" +
                   $"Name : {currentEquation.name}\n" +
                   $"Body : {currentEquation.body}\n" +
                   $"Type : {currentEquation.type}\n" +
                   $"Tail : {currentEquation.tail}\n");

                print($"" +
                   $"EquationString : {equations[currentEquationIndex].equationString}\n" +
                   $"Field : {equations[currentEquationIndex].field}\n" +
                   $"Name : {equations[currentEquationIndex].name}\n" +
                   $"Body : {equations[currentEquationIndex].body}\n" +
                   $"Type : {equations[currentEquationIndex].type}\n" +
                   $"Tail : {equations[currentEquationIndex].tail}\n");


                */

            }
            catch (Exception ex)
            {

            }




            GameObject outputTextBox = ((transform.Find("Output")).Find("Text")).gameObject;
            GameObject placeholderTextBox = ((transform.Find("Output")).Find("Placeholder")).gameObject;

            outputTextBox.GetComponent<TextMeshProUGUI>().text = "= " + equationBody;


            placeholderTextBox.GetComponent<TextMeshProUGUI>().text = "";



        }


        else if (vectorLineRegex.IsMatch(inputString.Replace(" ", "")))
        {
            equationType = "vectorLine";

            equation currentEquation = new equation();



            inputString = inputString.Replace(" ", "");
            string tempString = inputString;

            string[] equationParts = inputString.Split(new[] { "=", "λ" }, StringSplitOptions.RemoveEmptyEntries); //separates the line into 3 parts- l(a) = (x,y,z) + λ(a,b,c)  is separated into l(a), (x,y,z), (a,b,c).
                                                                                                                   //[0] = name, [1] = position vector, [2] = direction vector.
                                                                                                                   //i.e. l(a) = (1,1,1) + λ(3,1,5)
                                                                                                                   //sometimes user might put the other way round, we need to swap [1] and [2] if this is the case.
                                                                                                                   //however first we have to check if they included a position vector at all.
            
          






            if (equationParts.Length == 3)
            {
                equationParts[1] = equationParts[1].Remove(equationParts[1].Length - 1);

                if (inputString.Substring(inputString.IndexOf(equationParts[2]), equationParts[2].Length) != inputString.Substring(inputString.IndexOf("λ") + 1, equationParts[2].Length))
                {
                    string temp = equationParts[1];
                    equationParts[1] = equationParts[2];
                    equationParts[2] = temp;


                }
            }




            bool containsPositionVector = false;
            //l(a) = (x,y,z) + λ(a,b,c). Here, position vector is (x,y,z), direction vector is (a,b,c).
            //for the sake of lines being able to be used with equation struct:
            //name = l(a)
            //body = λ(a,b,c) but without λ.
            //tail = (x,y,z) i.e. the position vector.



            string positionVector = "(0,0,0)"; //sets positionVector and newpositionVectorparts to default values.
            string[] newPositionVectorParts = { "0", "0", "0" };  //position vector values, x,y,z

            if (equationParts.Length == 3) //if a position vector is defined, it sets it to the value. otherwise, it stays at {0,0,0}
            {


                positionVector = equationParts[1];

                containsPositionVector = true;
            }

            string equationName = equationParts[0]; //equationParts[0] = name of equation.
            string equationBody = equationParts[1]; //equationParts[1] = equation

            foreach (equation item in equations)
            {
                if (item.name == equationName)
                {
                    currentEquation = item;

                    equations.Remove(item);
                }
            }

            equationName = equationParts[0];
            string directionVector = equationParts[2];


            string newPositionVector = vectorHandlerScript.mainVectorParser(replaceAllFunctionsAndConstants(positionVector));
            print($"Positionvector = {positionVector}, newpositionvector = {newPositionVector}");
            string newDirectionVector = vectorHandlerScript.mainVectorParser(replaceAllFunctionsAndConstants(equationParts[2]));

            currentEquation.equationString = tempString; //the equationSTring is what is actually in the input field right now - not what it has been parsed to.
            currentEquation.field = transform.name;
            currentEquation.type = "vectorLine";
            currentEquation.name = equationName;
            currentEquation.body = newDirectionVector;
            currentEquation.tail = newPositionVector;




            bool equationFoundInList = false;
            int currentEquationIndex = 9999;
            foreach (equation item in equations)
            {
                if (item.name == equationName)
                {
                    currentEquationIndex = equations.IndexOf(item);


                    equations[currentEquationIndex] = currentEquation;
                    equationFoundInList = true;

                }
            }

            if (equationFoundInList == false)
            {
                equations.Add(currentEquation);
                currentEquationIndex = equations.IndexOf(currentEquation);
            }

            GameObject lineToDelete = equations[currentEquationIndex].vector; //its not actually a 'vector', but we are using equation struct . vector because thats how its defined below - .vector represents the gameobject.
            //might change later to 'gameobject', but too many references in code
            //9:56pm 17/07/2022



            DestroyImmediate(lineToDelete, true);



            currentEquation = equations[currentEquationIndex];

            string[] directionVectorParts = newDirectionVector.Replace("(", "").Replace(")", "").Split(",");
            string[] positionVectorParts = newPositionVector.Replace("(", "").Replace(")", "").Split(",");



            currentEquation.vector = vectorLineHandlerScript.graphVectorLine(equationName, float.Parse(positionVectorParts[0]), float.Parse(positionVectorParts[1]), float.Parse(positionVectorParts[2]), float.Parse(directionVectorParts[0]), float.Parse(directionVectorParts[1]), float.Parse(directionVectorParts[2]), objectColor);


            equations[currentEquationIndex] = currentEquation;
            GameObject outputTextBox = ((transform.Find("Output")).Find("Text")).gameObject;
            GameObject placeholderTextBox = ((transform.Find("Output")).Find("Placeholder")).gameObject;

            outputTextBox.GetComponent<TextMeshProUGUI>().text = "= " + currentEquation.tail + " + λ" + currentEquation.body;


            placeholderTextBox.GetComponent<TextMeshProUGUI>().text = "";


        }
        else if (vectorSphereRegex.IsMatch(inputString.Replace(" ", "")))
        {
            equationType = "vectorSphere";


            equation currentEquation = new equation();


            currentEquation.equationString = inputString;


            currentEquation.field = transform.name;
            currentEquation.type = "vectorSphere";
            currentEquation.name = "vectorSphere" + Convert.ToString(nonVectorCounter);



            List<float> sphereParts = vectorSphereHandlerScript.parseToCartesian(inputString);
            currentEquation.vector = cartesianSphereHandlerScript.graphSphere(sphereParts[0], sphereParts[1], sphereParts[2], sphereParts[3], objectColor);

            equations.Add(currentEquation);





        }
        //now we move onto the cartesian ones - cartesiann line, cartsian plane, cartesion sphere.
        //these are harder to check. we will check them in order of easiest-> hardest.

        //the cartesian line is supposed to have two "=" 

        else if (vectorPlaneRegex.IsMatch(inputString.Replace(" ", "")))
        {
            string[] vectorPlaneStringValues = vectorPlaneHandlerScript.parsePlaneIntoValues(inputString);
            List<float> vectorPlaneValues = new List<float>();

            for (int i = 0; i < 9; i++)
            {
                vectorPlaneValues.Add(float.Parse(vectorPlaneStringValues[i + 1]));
            }






            equation currentEquation = new equation();
            currentEquation.vector = vectorPlaneHandlerScript.createGraph(vectorPlaneValues[0], vectorPlaneValues[1], vectorPlaneValues[2], vectorPlaneValues[3], vectorPlaneValues[4], vectorPlaneValues[5], vectorPlaneValues[6], vectorPlaneValues[7], vectorPlaneValues[8], objectColor);


            currentEquation.equationString = inputString;


            currentEquation.field = transform.name;
            currentEquation.type = "vectorPlane";
            //currentEquation.name = "CartesianSphere" + Convert.ToString(nonVectorCounter);






            equations.Add(currentEquation);

        }

        else if (inputString.Replace("=", "").Length == inputString.Length - 2)
        {
            equationType = "cartesianLine";
        }
        //last two types are cartesian plane: "ax+by+cz-d=0", and cartesian sphere. "(x-a)^2 + (y-b)^2 + (z-c)^2 = r^2
        //cartesian sphere can be checked by "if there are 3 of ")^2", and it can be made the last else if to ensure that other functions with 
        //3 of ")^2" will be interpretd first to prevent incorrect type determination.
        //
        //else if (//how do i do this)


        else
        {
            try
            {

                if (inputString.Replace("^2", "").Length == inputString.Length - 6)
                {


                    equation currentEquation = new equation();


                    currentEquation.equationString = inputString;


                    currentEquation.field = transform.name;
                    currentEquation.type = "cartesianSphere";
                    currentEquation.name = "cartesianSphere" + Convert.ToString(nonVectorCounter);



                    List<float> sphereParts = cartesianSphereHandlerScript.parseSphere(inputString);
                    currentEquation.vector = cartesianSphereHandlerScript.graphSphere(sphereParts[0], sphereParts[1], sphereParts[2], sphereParts[3], objectColor);

                    equations.Add(currentEquation);






                }


            }
            catch (Exception ex)
            {
                if (inputString.Replace("=", "").Length == inputString.Length - 1)
                {
                    equationType = "cartesianPlane";

                    string anotherTempString = inputString;

                    string planeEquation = replaceAllFunctionsAndConstants(inputString);

                    List<float> parsedParts = cartesianPlaneHandlerScript.parseCartesianPlane(planeEquation);




                    //the user will only be allowed to input like ax + by + cz = d, they wont be allowed to 
                    // (3-2*4)x - 3.2+35y - 14.32z = 5.3 - 15.2 + 14 - x + y -32 z 
                    //they can do stuff like (3-2)x + 14y - (3*2)z = 15, but there can only be maximum 1 of x, y , or z.

                    //string xCoefficientString = 




                    equation currentEquation = new equation();

                    currentEquation.name = $"cartesianPlane{nonVectorCounter}";
                    currentEquation.vector = cartesianPlaneHandlerScript.createGraph(parsedParts[0], parsedParts[1], parsedParts[2], parsedParts[3]); // passes all the values to grapher module.
                    currentEquation.equationString = anotherTempString;
                    currentEquation.field = transform.name;

                    equations.Add(currentEquation);

                }

            }



        }


 

    




    }


    public void removeEquationAssociatedWithDeletedField(string fieldName)// this is called from DeleteInputField script. when a field is deleted, the associated equation is deleted.
    {
        foreach (equation item in equations)
        {
            if (item.field == fieldName)
            {
                GameObject vectorToDelete = item.vector;
                DestroyImmediate(vectorToDelete, true);

                try
                {
                    GameObject vectorTipToDelete = item.vectorTip;
                    DestroyImmediate(vectorTipToDelete, true);

                }
                catch (Exception ex)
                {

                }


                equations.Remove(item);

            }
        }
    }

    //this function is called from inspector


    public void replaceWithSymbols(string fieldContent)
    {

        fieldContent = transform.GetComponent<TMP_InputField>().text;
        string initialFieldContent = fieldContent;
        Regex lambdaRegex = new Regex(@"(?i)lambda");
        Regex muRegex = new Regex(@"(?i)mu");

        fieldContent = lambdaRegex.Replace(fieldContent, "λ");


        fieldContent = muRegex.Replace(fieldContent, "µ");

        if (initialFieldContent != fieldContent)
        {


            print("FLAG!");
            transform.GetComponent<TMP_InputField>().text = fieldContent;
        }
    }

    public void checkEquationExistence(string fieldContent) // deletes vectors aren't defined in any fields i.e. if the input in one the fields been changed
    {
        //this subroutine is run every time any of the input fields have their contents changed.
        //its aim is to ensure that if the user deletes an equation, or changes it such that it becomes invalid, then the old one is no longer graphed.
        //fieldContent is what is entered in the field.




        //note - the purpose of this equation is to check if the equation exists, but since it runs whenever the text is edited, we can also use it to replace 'lambda' with its actual symbol, and the same for 'mu'.
        string initialFieldContent = fieldContent;
        Regex lambdaRegex = new Regex(@"(?i)lambda"); //(?i) means case-insensitive.
        Regex muRegex = new Regex(@"(?i)mu");

        fieldContent = lambdaRegex.Replace(fieldContent, "λ");
        //SOMETIMES UNITY CANNOT DISPLAY CERTAIN CHARACTERS, such as λ. Below is guide for potential future use
        //This is due to the encoding of the file. They may not be encoded in a way such that certain characters like λ are included, thus cannot be printed nor used in any other way by unity when it reads the source code, as it 
        //doesn't even show up in the source code.
        //to fix this, open the script in notepad, save as "UTF-8 WITH BOM" and then you can use it to display special characters like λ.


        fieldContent = muRegex.Replace(fieldContent, "µ");

        if (initialFieldContent != fieldContent) // checks if anything has changed. if it has, it replaces with new string. otherwise, no.
        {



            transform.GetComponent<TMP_InputField>().text = fieldContent;
        }



        string fieldName = transform.name;

        foreach (equation item in equations)
        {
            if (item.field == fieldName)
            {
                if (item.equationString != fieldContent)
                {
                    GameObject vectorToDelete = item.vector;
                    DestroyImmediate(vectorToDelete, true);

                    try
                    {
                        GameObject vectorTipToDelete = item.vectorTip;
                        DestroyImmediate(vectorTipToDelete, true);

                    }

                    catch (Exception ex)
                    {

                    }

                    GameObject outputTextBox = ((transform.Find("Output")).Find("Text")).gameObject;
                    GameObject placeholderTextBox = ((transform.Find("Output")).Find("Placeholder")).gameObject;
                    outputTextBox.GetComponent<TextMeshProUGUI>().text = "";
                    placeholderTextBox.GetComponent<TextMeshProUGUI>().text = "=";
                    equations.Remove(item);
                }
            }
        }









    }





}


 
public struct equation
{
    public string name;
    //names only apply for vectors, vectorlines, vectorplanes, the others just have 'null'.

    public string body;

    public string tail;

    public string equationString;
    //equationString = the value of the equation - the ENTIRE thing, not parsed. 

    public string field;
    //field = the name of the inputfield where it was created. e.g. inputField3, inputField9

    public string type;//vector, plane, etc.

    //note -  name, tail will have to be 'null' for things like spehres because they don't have them.

    public GameObject vector;
    public GameObject vectorTip;

    //'vector' will still be used if it is not a vector, vectorPlane, or vectorLine.
    //it is the gameobject that is graphed.

    //vectorTip gameobjcet will exist only for a vector. for others, it will be 'null'.
}

/*
struct vector
{
    public string name;//name should be  defined as v(a), or v(b), or v(c).. etc. or V(a), V(b).. make sure it is not allowed as anything else. anything else will either
                             // be something like a line/plane/sphere, or compile error.

    public string body;//expression for the body of the vector (parsed). something like (5,1,3)
    public string tail;//expression for the tail of the vector (parsed). something like {3,1,9}

    public string correspondingInputField; //correspondingInputField is the name of the field that the equation is entered in. 
    //this is needed for the checkEquationExistence() function, because by doing this we can find the equation that was altered.
    //also, when the user deletes an input field, we only know the input field that is deleted - we need to find the equation that corresponds to that input field so we can get rid of it.


    public float vectorX;
    public float vectorY; //vector X, Y, Z are the lengths of the vector in xyz.
    public float vectorZ;

    public float tailPosX;
    public float tailPosY; //tailpos is the coordinate of the tail of the vector
    public float tailPosZ;

    public Color color;
}
 

 
struct vectorLine
{


}
struct vectorPlane
{


}

struct vectorSphere
{
    //for things like vectorSphere that don't have unique names, their .name component will just be 'vectorSphere' - i.e. their type.
}

struct cartesianLine
{

}

struct cartesianPlane
{

}
struct cartesianCircle
{

}

struct cartesianSphere
{

}

struct constantStruct
{

}
*/

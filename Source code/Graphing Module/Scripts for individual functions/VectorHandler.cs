using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class VectorHandler : MonoBehaviour
{
    static System.Random random = new System.Random();


    public GameObject vectorTip1;

    public Material graphMaterial;
    private void Start()
    {
        // vectorTip = GameObject.Find("VectorTipPrefab");

    }

    public string subVectorParser(string inputString)
    {
        string outputString = "";
        //Console.WriteLine("Currently trying to parse " + inputString);
        //there are a few cases for VECTORS (parametric equations, planes, spheres will be elsewhere
        //case1    (a+b-c*d/e)  (a normal mathematical expression, but in brackets). --- > return x.
        //case2    Size(a,b,c) --- > return x
        //case3    Cross[(a,b,c),(d,e,f)] --- > return a vector (x,y,z)
        //case4    Dot[(a,b,c),(d,e,f)] --- > return a single number
        //case5    Proj[(a,b,c) on (d,e,f)] --- > return a vector (x,y,z)
        //case6    (a,b,c). This will occur due to the level system, but there may be a prefix like 3*5*(a,b,c), so we have to process this to turn it into (15a,15b,15c)
        //case7    (a,b,c),(d,e,f) or [(a,b,c),(d,e,f)]. This will occur due to the level system, simply return this so the other function can decrease its level by 1.
        //case8    (a,b,c) + (d,e,f,) - (g,h,i) - (j,k,l) + (m,n,o) +.... --- > return (x,y,z)
        //also note that within these, ther ecan be stuff like (a+b,c+d,e) + (g,h,i+j), so we must Int.Parse everything inside.
        //for case5, the compiler might convert something like (1,3,1) - 5(1,1,1) ->>> (1,3,1) (-5,-5,-5), without any operator. fix this later. (written 20:17 20/04/2022)

        //the following lines of code determine what sort of input it is, based on the 8 cases above.

        //makes sure the input it is processing is not a vector, but an arithmetic expression such as (3*2 + 5) -> then calculates its value
        if (inputString.Contains(",") == false)
        {

            outputString = inputString;
            try
            {
                inputString = inputString.Replace("(", "").Replace(")", "");


                outputString = (new DataTable().Compute(inputString, null)).ToString();
            }
            catch (Exception e)
            {

            }

            return outputString;
        } //case1

        else if (inputString.Contains("Size"))
        {
            double calculatedPrefixValue = 1;
            if (inputString[0].ToString() != "S")
            {
                string prefixValue = inputString.Remove(inputString.IndexOf("S"));
                if (prefixValue[prefixValue.Length - 1].ToString() == "*")
                {
                    prefixValue = prefixValue.Remove(prefixValue.Length - 1);
                    calculatedPrefixValue = float.Parse(new DataTable().Compute(prefixValue, null).ToString());
                    prefixValue = prefixValue + "*";
                    inputString = inputString.Remove(0, (prefixValue.Length));
                }
                else
                {
                    calculatedPrefixValue = float.Parse(new DataTable().Compute(prefixValue, null).ToString());
                    inputString = inputString.Remove(0, (prefixValue.Length));
                }
            }



            inputString = inputString.Replace("Size", "");
            Console.WriteLine(inputString);


            inputString = subVectorParser(inputString);
          

            string[] xyzValues = inputString.Replace("(", "").Replace(")", "").Split(",");
            float x = float.Parse(xyzValues[0]);
            float y = float.Parse(xyzValues[1]);
            float z = float.Parse(xyzValues[2]);

            double val = (Math.Sqrt(x * x + y * y + z * z));
            outputString = (val * calculatedPrefixValue).ToString();
        } //case2
        else if (inputString.Contains("Cross"))
        {
          


            float calculatedPrefixValue = 1;
            if (inputString[0].ToString() != "C")
            {
                string prefixValue = inputString.Remove(inputString.IndexOf("Cross"));
                if (prefixValue[prefixValue.Length - 1].ToString() == "*")
                {
                    prefixValue = prefixValue.Remove(prefixValue.Length - 1);
                    calculatedPrefixValue = float.Parse(new DataTable().Compute(prefixValue, null).ToString());
                    prefixValue = prefixValue + "*";
                    inputString = inputString.Remove(0, (prefixValue.Length));
                }
                else
                {
                    calculatedPrefixValue = float.Parse(new DataTable().Compute(prefixValue, null).ToString());
                    inputString = inputString.Remove(0, (prefixValue.Length));
                }
            }

            string crossProductString = inputString;

            crossProductString = crossProductString.Replace("[", "").Replace("]", "").Replace("Cross", "");

            string[] crossProductVectors = crossProductString.Split(new[] { ")," }, System.StringSplitOptions.RemoveEmptyEntries);
            List<string> convertedCrossProductVectors = new List<string>();
            foreach (string item1 in crossProductVectors)
            {


                string currentVector = item1;

                if (currentVector[currentVector.Length - 1] != ')')
                {
                    currentVector = currentVector + ")";
                }

                currentVector = subVectorParser(currentVector);
                Console.WriteLine(currentVector);

                convertedCrossProductVectors.Add(currentVector.Replace("(", "").Replace(")", ""));
            }

            string[] vector1 = convertedCrossProductVectors[0].Split(',');
            float x1 = float.Parse(vector1[0]);
            float y1 = float.Parse(vector1[1]);
            float z1 = float.Parse(vector1[2]);
            string[] vector2 = convertedCrossProductVectors[1].Split(',');
            float x2 = float.Parse(vector2[0]);
            float y2 = float.Parse(vector2[1]);
            float z2 = float.Parse(vector2[2]);
            outputString = "(" + (y1 * z2 - z1 * y2) * calculatedPrefixValue + "," + (z1 * x2 - x1 * z2) * calculatedPrefixValue + "," + (x1 * y2 - y1 * x2) * calculatedPrefixValue + ")";
        } //case3
        else if (inputString.Contains("Dot"))
        {
            

            double calculatedPrefixValue = 1;
            if (inputString[0].ToString() != "D")
            {
                string prefixValue = inputString.Remove(inputString.IndexOf("Dot"));
                if (prefixValue[prefixValue.Length - 1].ToString() == "*")
                {
                    prefixValue = prefixValue.Remove(prefixValue.Length - 1);
                    calculatedPrefixValue = float.Parse(new DataTable().Compute(prefixValue, null).ToString());
                    prefixValue = prefixValue + "*";
                    inputString = inputString.Remove(0, (prefixValue.Length));
                }
                else
                {
                    calculatedPrefixValue = float.Parse(new DataTable().Compute(prefixValue, null).ToString());
                    inputString = inputString.Remove(0, (prefixValue.Length));
                }
            }

            string dotProductString = inputString;
            dotProductString = dotProductString.Replace("[", "").Replace("]", "").Replace("Dot", "");
            string[] dotProductVectors = dotProductString.Split(new[] { ")," }, System.StringSplitOptions.RemoveEmptyEntries);
            List<string> convertedDotProductVectors = new List<string>();
            foreach (string item1 in dotProductVectors)
            {
                convertedDotProductVectors.Add(item1.Replace("(", "").Replace(")", ""));
            }

            string[] vector1 = convertedDotProductVectors[0].Split(',');
            float x1 = float.Parse(vector1[0]);
            float y1 = float.Parse(vector1[1]);
            float z1 = float.Parse(vector1[2]);
            string[] vector2 = convertedDotProductVectors[1].Split(',');
            float x2 = float.Parse(vector2[0]);
            float y2 = float.Parse(vector2[1]);
            float z2 = float.Parse(vector2[2]);

            double val = (x1 * x2 + y1 * y2 + z1 * z2);
            outputString = (val * calculatedPrefixValue).ToString(); //dotproduct returns number, not vector.
        } //case4 
        else if (inputString.Contains("Proj"))
        {
            float calculatedPrefixValue = 1;
            if (inputString[0].ToString() != "P")
            {
                string prefixValue = inputString.Remove(inputString.IndexOf("Proj"));
                if (prefixValue[prefixValue.Length - 1].ToString() == "*")
                {
                    prefixValue = prefixValue.Remove(prefixValue.Length - 1);
                    calculatedPrefixValue = float.Parse(new DataTable().Compute(prefixValue, null).ToString());
                    prefixValue = prefixValue + "*";
                    inputString = inputString.Remove(0, (prefixValue.Length));
                }
                else
                {
                    calculatedPrefixValue = float.Parse(new DataTable().Compute(prefixValue, null).ToString());
                    inputString = inputString.Remove(0, (prefixValue.Length));
                }
            }

            string projectionString = inputString;
            projectionString = projectionString.Replace("[", "").Replace("]", "").Replace(("Proj"), "");
            string[] projectionVectors = projectionString.Split(new[] { "on" }, System.StringSplitOptions.RemoveEmptyEntries);
            List<string> convertedProjectionVectors = new List<string>();
            foreach (string item1 in projectionVectors)
            {


                string currentVector = item1;

                if (currentVector[currentVector.Length - 1] != ')')
                {
                    currentVector = currentVector + ")";
                }

                currentVector = subVectorParser(currentVector);
                Console.WriteLine(currentVector);

                convertedProjectionVectors.Add(currentVector.Replace("(", "").Replace(")", ""));
            }

            string[] vector1 = convertedProjectionVectors[0].Split(',');
            float x1 = float.Parse(vector1[0]);
            float y1 = float.Parse(vector1[1]);
            float z1 = float.Parse(vector1[2]);
            string[] vector2 = convertedProjectionVectors[1].Split(',');
            float x2 = float.Parse(vector2[0]);
            float y2 = float.Parse(vector2[1]);
            float z2 = float.Parse(vector2[2]);
            float magnitudeVector1 = MathF.Sqrt(x1 * x1 + y1 * y1 + z1 * z1);
            float magnitudeVector2 = MathF.Sqrt(x2 * x2 + y2 * y2 + z2 * z2);
            outputString = "(" + (((x1 * x2 + y1 * y2 + z1 * z2) / (magnitudeVector2 * magnitudeVector2)) * x2 * calculatedPrefixValue) + "," + (((x1 * x2 + y1 * y2 + z1 * z2) / (magnitudeVector2 * magnitudeVector2)) * y2 * calculatedPrefixValue) + "," + (((x1 * x2 + y1 * y2 + z1 * z2) / (magnitudeVector2 * magnitudeVector2)) * z2 * calculatedPrefixValue) + ")";
        } //case5
        else if (inputString.Replace("(", "").Replace(")", "").Replace(",", "").Length == inputString.Length - 4)
        {
            float calculatedPrefixValue = 1;
            if (inputString[0].ToString() != "(")
            {
                string prefixValue = inputString.Remove(inputString.IndexOf("("));
                if (prefixValue[0].ToString() == "+")
                {
                    inputString = inputString.Remove(0, 1);
                    inputString = subVectorParser(inputString);
                }
                else if (prefixValue == "-")
                {
                    inputString = inputString.Insert(1, "1*");
                    inputString = subVectorParser(inputString);
                }
                else if (prefixValue[prefixValue.Length - 1].ToString() == "*")
                {
                    prefixValue = prefixValue.Remove(prefixValue.Length - 1);
                    calculatedPrefixValue = float.Parse(new DataTable().Compute(prefixValue, null).ToString());
                    prefixValue = prefixValue + "*";
                    inputString = inputString.Remove(0, (prefixValue.Length));
                }
                else
                {
                    calculatedPrefixValue = float.Parse(new DataTable().Compute(prefixValue, null).ToString());
                    inputString = inputString.Remove(0, (prefixValue.Length));
                }
            }

            string[] xyzValues = inputString.Remove(inputString.Length - 1, 1).Remove(0, 1).Split(",");
            outputString = "(" + Convert.ToSingle(new DataTable().Compute(xyzValues[0], null)) * calculatedPrefixValue + "," + Convert.ToSingle(new DataTable().Compute(xyzValues[1], null)) * calculatedPrefixValue + "," + Convert.ToSingle(new DataTable().Compute(xyzValues[2], null)) * calculatedPrefixValue + ")";
            return outputString;
        } //case6
        else if ((inputString.Replace("(", "").Replace(")", "").Replace(",", "").Length == inputString.Length - 9) || (inputString.Replace("(", "").Replace(")", "").Replace(",", "").Replace("[", "").Replace("]", "").Length == inputString.Length - 11))
        {
            outputString = inputString;
        } //case7
        else if ((inputString[inputString.Length - 1].ToString() == ")" && inputString[inputString.Length - 2].ToString() == ")" && inputString[0].ToString() == "("))
        {
            outputString = subVectorParser(inputString.Remove(inputString.Length - 1, 1).Remove(0, 1));

            return outputString;
        }
        else
        {
            inputString = inputString.Replace(" ", "");
            string[] vectorsToProcess = inputString.Split(")");
            List<string> processedVectors = new List<string>();
            float totalX = 0;
            float totalY = 0;
            float totalZ = 0;
            foreach (string vector in vectorsToProcess)
            {
                if (vector != "")
                {
                    string formattedVector = subVectorParser(vector + ")");
                    formattedVector = formattedVector.Replace("(", "").Replace(")", "");
                    string[] currentVectorCoordinates = formattedVector.Split(new[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
                    totalX = totalX + float.Parse(currentVectorCoordinates[0]);
                    totalY = totalY + float.Parse(currentVectorCoordinates[1]);
                    totalZ = totalZ + float.Parse(currentVectorCoordinates[2]);
                }
            }

            outputString = ("(" + totalX + "," + totalY + "," + totalZ + ")");
            return outputString;
        }//case8

        /*old code
        else //this is the final case. //case 5 (HAS BEEN MADE SO THAT IT CAN ONLY DO ADDITION - ENSURE WHEN PARSING ALL NEGATIVES ARE CONVERTED TO POSITIVES. FOR EXAMPLE, (1,3,2) - (1,1,9) is converted to (1,3,2) + (-1,-1,-9).
        {
			inputString = inputString.Replace(" ", "");
			string[] vectorsToSum = inputString.Split(new[] { ")+(" }, System.StringSplitOptions.RemoveEmptyEntries);
			float totalX = 0;
			float totalY = 0;
			float totalZ = 0;
			foreach (string vectorString in vectorsToSum)
			{
				string formattedVectorString = vectorString.Replace("(", "").Replace(")", "");
				string[] currentVectorCoordinates = formattedVectorString.Split(new[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
				totalX = totalX + float.Parse(currentVectorCoordinates[0]);
				totalY = totalY + float.Parse(currentVectorCoordinates[1]);
				totalZ = totalZ + float.Parse(currentVectorCoordinates[2]);
			}

			outputString = ("(" + totalX + "," + totalY + "," + totalZ + ")");
			return outputString;
		}
		*/
        return outputString;
    }


    public string mainVectorParser(string equationBody) //this module is responsible for parsing the entire equation. it calls subVectorParsre
    {
        string parsedString = "";

        //Dictionary<int, int> indexLevelDict = new Dictionary<int, int>();
        List<int> indexLevelList = new List<int>();



        //(index,value)
        //will use a system of levels. level 0 is the topmost. it goes up, level 1,2...n.
        //each function that is inside another function goes up another level.
        //"V(a) = (3,2,-8) + Cross[(3,2,1),(-3,10,3)] - 3*Proj[(1,-3,2) on (22*Dot[(3,2,1),(1,1,9)], 1,3)], {-3,1,9}";
        //(3,2,-8) is level 0. but the (1,-3,2) inside the projection is level 1. and the 22*Dot[(3,2,1),(1,1,9)] is level 2. (1,1,9) inside the dotproduct is level 3.
        // level n = number of brackets ([ or ( ) surrounding it.
        //"Cross", "Proj", "on", "Size", "Dot" are also moved up a level so that they will be included in the equaiton to parse.

        int currentLevel = 0;
        int highestLevel = 0;
        for (int index = 0; index < equationBody.Length; index++)
        {
            if ((equationBody[index].ToString() == "(") || (equationBody[index].ToString() == "["))
            {
                currentLevel = currentLevel + 1;
                if (currentLevel > highestLevel)
                {
                    highestLevel = currentLevel;
                }

                indexLevelList.Add(currentLevel);
            }
            else if ((equationBody[index].ToString() == ")") || (equationBody[index].ToString() == "]"))
            {
                indexLevelList.Add(currentLevel);
                currentLevel = currentLevel - 1;
            }
            else
            {
                indexLevelList.Add(currentLevel);
            }
        }

        for (int index = 0; index < equationBody.Length; index++)
        {
            if ((equationBody[index].ToString() == "P") || (equationBody[index].ToString() == "D") || (equationBody[index].ToString() == "C") || (equationBody[index].ToString() == "S"))
            { //Dot,Proj,Size,Cross

                //for all occurences of "Cross", "Proj", "Size", "Dot", "on", it raises their index by one, so that they get processed as an equation by the parser.
                if (equationBody.Substring(index, 5) == "Cross")
                {
                    for (int index1 = index; index1 < index + 5; index1++)
                    {
                        indexLevelList[index1] = indexLevelList[index1] + 1;
                    }
                }
                else if (equationBody.Substring(index, 4) == "Proj")
                {
                    for (int index1 = index; index1 < index + 4; index1++)
                    {
                        indexLevelList[index1] = indexLevelList[index1] + 1;
                    }
                }
                else if (equationBody.Substring(index, 4) == "Size")
                {
                    for (int index1 = index; index1 < index + 4; index1++)
                    {
                        indexLevelList[index1] = indexLevelList[index1] + 1;
                    }
                }
                else if (equationBody.Substring(index, 3) == "Dot")
                {
                    for (int index1 = index; index1 < index + 3; index1++)
                    {
                        indexLevelList[index1] = indexLevelList[index1] + 1;
                    }
                }
                else if (equationBody.Substring(index, 2) == "on")
                {
                    for (int index1 = index; index1 < index + 2; index1++)
                    {
                        indexLevelList[index1] = indexLevelList[index1] + 1;
                    }
                }
            }
        }


        //dictionay looks like [(index, level)]
        for (int level = highestLevel; level >= 0; level--)
        {
            int positionIndex = 0;
            int previousPositionIndex = -1;
            while (indexLevelList.Contains(level))
            {
                string currentStringToParse = "";
                //gets 1 string to parse (below, until *marker)
                int startIndex = 0;
                //startIndex is startAppendingToStringIndex. positionIndex is position in the string.
                try
                {
                    while (indexLevelList[startIndex] != level)
                    {
                        startIndex = startIndex + 1;
                    }
                }
                catch (Exception a)
                {
                    //nothing
                }

                int finishIndex = startIndex;
                try
                {
                    while (indexLevelList[finishIndex + 1] == level)
                    {
                        finishIndex = finishIndex + 1;
                    }
                }
                catch (Exception ex)
                {
                    //nothing
                }

                //Console.WriteLine("StartIndex = " + startIndex + "FinishIndex = " + finishIndex);
                if (startIndex == finishIndex)
                {
                    break;
                }

                currentStringToParse = equationBody.Substring(startIndex, finishIndex - startIndex + 1);
                parsedString = subVectorParser(currentStringToParse);
                equationBody = equationBody.Remove(startIndex, finishIndex - startIndex + 1).Insert(startIndex, parsedString);
                List<int> tempList = new List<int>();
                for (int index = 0; index < startIndex; index++)
                {
                    tempList.Add(indexLevelList[index]);
                }

                for (int index = startIndex; index < startIndex + parsedString.Length; index++)
                {
                    tempList.Add(indexLevelList[startIndex] - 1);
                }

                for (int index = startIndex + currentStringToParse.Length; index < indexLevelList.Count; index++)
                {
                    tempList.Add(indexLevelList[index]);
                }

                indexLevelList = tempList;


            }
        }





        return parsedString;
    }
    public void temporaryfunction()
    {


    }


    public Array vectorGraph(string vectorName, float vectorX, float vectorY, float vectorZ, float tailX, float tailY, float tailZ, Color color)
    {






        vectorX = vectorX * 100;
        vectorY = vectorY * 100;
        vectorZ = vectorZ * 100;
        tailX = tailX * 100;
        tailY = tailY * 100;
        tailZ = tailZ * 100;
        //scales them up by 100, so they can appear on the graph and not be tiny. 

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

        //'current' refers to the one that we are dealing with right now.
        GameObject currentVector;
        GameObject currentVectorTip;


        currentVector = GameObject.CreatePrimitive(PrimitiveType.Cylinder); //creates the vector
        currentVectorTip = Instantiate(vectorTip1); //instantiates the vector tip



        currentVector.GetComponent<Collider>().enabled = false;

        Vector3 directionVector = new Vector3(vectorX, vectorY, vectorZ);
        float size = Vector3.Magnitude(directionVector); // magnitude of the vector



        float vectorThickness = 10;//used to be 100
        float vectorTipScale = 700; //used to be 7000



        currentVector.transform.localScale = new Vector3(vectorThickness, size / 2 - vectorTipScale * 0.01f * 2f, vectorThickness);


        currentVector.transform.position = new Vector3(tailX + 0.5f * vectorX, tailZ + 0.5f * vectorZ, tailY + 0.5f * vectorY) - vectorTipScale * 0.01f * 2f * (new Vector3(vectorX, vectorZ, vectorY)) / Mathf.Sqrt(vectorX * vectorX + vectorY * vectorY + vectorZ * vectorZ); //position of vector in unity editor. the position corresponds to the MIDDLE of the vector.
        currentVector.transform.up = new Vector3(vectorX, vectorZ, vectorY); //makes the vector point in the direction that its supposed to


        int randomNum = random.Next(0, 5); //  change this if adding new colors -  change to random.Next(0,k), k = num of colors - 1
                                           //List<int> selectedColor = colorList[randomNum];
                                           //  currentVector.transform.GetComponent<MeshRenderer>().material.color = new Color(selectedColor[0], selectedColor[1], selectedColor[2]); //there shall be a selection of colors, a random color will be selected every time.
        currentVector.transform.GetComponent<MeshRenderer>().material = graphMaterial;
        currentVector.transform.GetComponent<MeshRenderer>().material.color = color;






        //attaches a pointy cone tip to the vector, with correct position and angle
        currentVectorTip.transform.up = new Vector3(vectorX, vectorZ, vectorY);


        currentVectorTip.transform.localScale = new Vector3(vectorTipScale, vectorTipScale, vectorTipScale);

        currentVectorTip.transform.position = new Vector3(tailX + vectorX, tailZ + vectorZ, tailY + vectorY) - vectorTipScale * 0.01f * 4f * (new Vector3(vectorX, vectorZ, vectorY)) / Mathf.Sqrt(vectorX * vectorX + vectorY * vectorY + vectorZ * vectorZ);

        currentVector.active = true;
        currentVectorTip.active = true;

        GameObject[] storageArray = new GameObject[] { currentVector, currentVectorTip };
        //we cant add the vector tip and vector gameobjects to their dictionaries as that is stored in the other file. so, we have to pass these gameobjects back, so that
        //the other script can add them to the dictionary. 
        //we need this array, so that we can return both gameobjcets.

        return storageArray;
    }
}

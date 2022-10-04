using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEditor;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;


public class IndividualGraphSettings : MonoBehaviour
{

      public GameObject graphSettingsInterface;
      GameObject Canvas;
    public Sprite noColor;
    public Sprite hasColor;




    MainEquationHandler mainVectorHandlerScript;

    AddInputField addInputFieldScript;
    bool interfaceOpen = false; // whether or not the interface is open


    private void Start()


    {
      



        Canvas = GameObject.FindGameObjectWithTag("Canvas");


        mainVectorHandlerScript = GameObject.FindGameObjectWithTag("InputFieldTag1").GetComponent<MainEquationHandler>();

        addInputFieldScript = GameObject.FindGameObjectWithTag("NewFieldButton").GetComponent<AddInputField>();


        graphSettingsInterface = GameObject.Instantiate(  graphSettingsInterface, transform.parent      );
      //  print("graphsettingsinterface + " + graphSettingsInterface.transform.name);

        graphSettingsInterface.SetActive(false);




    }

    public void openGraphSettingsInterface() //this runs when the '...' button is clicked.
    {
        interfaceOpen = true;

        graphSettingsInterface.SetActive(true);
     

        graphSettingsInterface.transform.position = Input.mousePosition;
        graphSettingsInterface.transform.parent.transform.SetAsLastSibling(); //makes it so the parent goes to the front so that the child (this) also comes to the front.
        //there might be somethign else about layers/ "bringtofront" but this will suffice.

        //the transform that opens this thing is settingsButton;
        




        List<equation> currentEquations = MainEquationHandler.equations;

        string string2 = ((transform.parent.Find("Input").Find("Text").GetComponent<TextMeshProUGUI>().text).Replace(" ", ""));
        string2 = string2.Remove(string2.Length - 1);
        foreach (equation item in currentEquations)
        {
            string string1 = item.equationString.Replace(" ", "");


            print(item.vector.transform.localScale.x);
            print(transform.parent.Find("Input").Find("Text").GetComponent<TextMeshProUGUI>().text.Replace(" ", ""));
            if (string1 == string2)
            {


                transform.parent.Find("IndividualGraphSettings(Clone)").Find("Size").Find("SizeField").Find("Text Area").Find("Placeholder") .GetComponent<TextMeshProUGUI>().text = item.vector.transform.localScale.x.ToString();
               // transform.parent.Find("IndividualGraphSettings(Clone)").Find("Color").Find("ColorDropDown").GetComponent<TMP_Dropdown>().value = something
            }


        }


    }



    
     
    private void Update()
    {

     
            if (interfaceOpen = true && Input.GetMouseButton(0) == true)
            {

                if(graphSettingsInterface != null)
            {
                Vector2 mousePos = Input.mousePosition;

                float leftBoundary = graphSettingsInterface.transform.position.x;
                float rightBoundary = graphSettingsInterface.transform.position.x + Screen.width * (graphSettingsInterface.GetComponent<RectTransform>().rect.width / Canvas.GetComponent<RectTransform>().rect.width);
                float topBoundary = graphSettingsInterface.transform.position.y;
                float bottomBoundary = graphSettingsInterface.transform.position.y - Screen.height * (graphSettingsInterface.GetComponent<RectTransform>().rect.height / Canvas.GetComponent<RectTransform>().rect.height);


                if (mousePos.x < leftBoundary || mousePos.x > rightBoundary || mousePos.y > topBoundary || mousePos.y < bottomBoundary)
                {
                    graphSettingsInterface.active = false; //makes it dissapear if you click away from it.
                    interfaceOpen = false;
                }
            }
                
            }

       




    }
   



    public void hideShowGraph()//hides/shows the graph when clicked
    {
        List<equation> currentEquations = MainEquationHandler.equations;

        print("FLAGG");
        string string2 = ((transform.parent.Find("Input").Find("Text").GetComponent<TextMeshProUGUI>().text).Replace(" ", ""));
        string2 = string2.Remove(string2.Length - 1);



        bool active = false;
        Color graphColor = Color.white; //default, doen't matter, won't show anyways if theere is no graph or if its hidden


        foreach (equation item in currentEquations)
        {
            //note - for the below, string a, when taking the string from the textbox for some reason it adds a invisible character at the end, so you have to remove the last character manually.
            /*
            string s = item.equationString.Replace(" ", "");
            string a = (transform.parent.Find("Input").Find("Text").GetComponent<TextMeshProUGUI>().text).Replace(" ", "");

            //transform : the script is supposed to be attached to the "onOffButton"
            print(s);
            print(a);

            char[] sCH = s.ToCharArray();
            char[] aCH = a.ToCharArray();

            print("ach lenght = " + aCH.Length);
            print("sch lenght = " + sCH.Length);

            foreach(char c in aCH)
            {
                print($"CurrentChar:({c})");
            }


            if (Equals(a, s))
            {
                print("??");

            }

            
            if (s.ToString()==a.ToString())
            {
                

                item.vector.SetActive(!item.vector.active);

                try
                {
                    item.vectorTip.SetActive(!item.vector.active);
                }
                catch (Exception e) { }


            }
            */
            string string1 = item.equationString.Replace(" ", "");
           

            if (string1 == string2)
            {


                item.vector.SetActive(!item.vector.active);

                try
                {
                    item.vectorTip.SetActive(!item.vectorTip.active);
                }
                catch (Exception e) { }



                if(item.vector.active == true)
                {
                    active = true;

                    if(item.type == "cartesianSphere" || item.type == "vectorSphere")
                    {
                        graphColor = item.vector.GetComponent<MeshRenderer>().materials[1].color;
                    }
                    else
                    {
                        graphColor = item.vector.GetComponent<MeshRenderer>().material.color;
                    }
                     
                }

            }


        }


        if (active == true)
        {
            transform.GetComponent<Image>().sprite = hasColor;
            transform.GetComponent<Image>().color = graphColor;

        }
        else
        {
            gameObject.GetComponent<Image>().sprite = noColor;
            transform.GetComponent<Image>().color = Color.white;
        }




    }

    public void applySettings() //applies settings. //we do not need to use graphSettingsInterface gameobject (also it will not work because this script will be called from the graphsettingsinterface object)
        //so we will have to use its own transform instead

    {
        Transform settingsTransform = transform.parent.transform;
        string size = "10";
        float floatSize = 10f;


        try
        {
              size = settingsTransform.transform.Find("Size").Find("SizeField").GetComponent<TMP_InputField>().text;
              floatSize = float.Parse(size);
        }
        catch(Exception e)
        {
            //this occurs if they left size blank.

        }
        
        TMP_Dropdown dropDown = settingsTransform.transform.Find("Color").Find("ColorDropDown").GetComponent<TMP_Dropdown>();

        string colorValue =   dropDown.options[ dropDown.value].text; //gives it as a string. alternatively, dropDown.value can be used to get it as an integer.


        Color newColor = new Color();

        switch (colorValue) //note - the colors here are not the same colors in inspector. here, you set colors from 0->1, in ther eyou set 0->255. i dont know the exact converstion but dividing by 255 seems to wor.
        {
            case "Red":
                newColor.r = 255f/255f;
                newColor.g = 0 / 255f;
                newColor.b = 0 / 255f;
                newColor.a = 255 / 255f;
                break;

            case "Yellow":
                newColor.r = 255 / 255f;
                newColor.g = 255 / 255f;
                newColor.b = 0 / 255f;
                newColor.a = 255 / 255f;
                break;
            case "Orange":
                newColor.r = 255 / 255f;
                newColor.g = 165 / 255f;
                newColor.b = 0 / 255f;
                newColor.a = 255 / 255f;
                break;
            case "Green":
                newColor.r = 0 / 255f;
                newColor.g = 255 / 255f;
                newColor.b = 0 / 255f;
                newColor.a = 255 / 255f;
                break;
            case "Light Blue":
                newColor.r = 173 / 255f;
                newColor.g = 216 / 255f;
                newColor.b = 230 / 255f;
                newColor.a = 255 / 255f;
                break;
            case "Dark Blue":
                newColor.r = 0 / 255f;
                newColor.g = 0 / 255f;
                newColor.b = 139 / 255f;
                newColor.a = 255 / 255f;
                break;
            case "Black":
                newColor.r = 0 / 255f;
                newColor.g = 0 / 255f;
                newColor.b = 0 / 255f;
                newColor.a = 255 / 255f;
                break;
            case "White":
                newColor.r = 255 / 255f;
                newColor.g = 255 / 255f;
                newColor.b = 255 / 255f;
                newColor.a = 255 / 255f;
                break;



            default:
                newColor.r = 255 / 255f;
                newColor.g = 255 / 255f;
                newColor.b = 255 / 255f;
                newColor.a = 255 / 255f; //this is white
                break;



        }

        List<equation> currentEquations = MainEquationHandler.equations;

       



        //print($"transform name = {transform.name}"); //transform.name = "Apply" when running this bit of code. 
      


        string string2 = ((transform.parent.parent.Find("Input").Find("Text").GetComponent<TextMeshProUGUI>().text).Replace(" ", ""));
        string2 = string2.Remove(string2.Length - 1);
        foreach (equation item in currentEquations)
        {
            string string1 = item.equationString.Replace(" ", "");
            
            

            if (string1 == string2)
            {
                if (item.type == "cartesianSphere" || item.type == "vectorSphere")
                {

                    newColor.a = 225f/255f;
                    item.vector.transform.GetComponent<MeshRenderer>().materials[1].color = newColor; //materials[0] is the grid thing, materials[1] is the transparent color.



                     

             
                }
                else
                {
                    item.vector.transform.GetComponent<MeshRenderer>().material.color = newColor;
                }
 

                if(item.type == "vector")
                {
                    item.vector.transform.localScale = new Vector3(floatSize, item.vector.transform.localScale.y, floatSize);
                    item.vector.active = true;
                    try
                    {
                        item.vectorTip.active = true;
                        item.vectorTip.transform.localScale = new Vector3(floatSize * 70, item.vectorTip.transform.localScale.y, floatSize * 70);
                    }
                    catch(Exception ex)
                    {

                    }
                    

                }
                else
                {
                    if(item.type == "vectorLine")
                    {
                        item.vector.transform.localScale = new Vector3(floatSize, item.vector.transform.localScale.y, floatSize);
                    }
                   
                }

                if(item.type == "cartesianPlane" || item.type == "vectorPlane")
                {
                    item.vector.transform.localScale = new Vector3(floatSize, item.vector.transform.localScale.y, floatSize);
                }



                Transform inputFieldTransform = transform.parent.parent.transform;
                string inputFieldNum = (inputFieldTransform.name).Replace("inputField", "");



                Transform onOffButtonTransform = inputFieldTransform.Find($"onOffButton.{inputFieldNum}");




                onOffButtonTransform.GetComponent<Image>().sprite = hasColor;

                Color graphColor = Color.white;

                if(item.type == "cartesianSphere" || item.type == "vectorSphere")
                {
                    graphColor = item.vector.transform.GetComponent<MeshRenderer>().materials[1].color;
                }
                else
                {
                    graphColor=item.vector.transform.GetComponent<MeshRenderer>().material.color;
                }
               

                onOffButtonTransform.GetComponent<Image>().color = graphColor;
                    


            }


        }




    }


}

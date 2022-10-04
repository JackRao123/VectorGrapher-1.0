using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEditor;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Diagnostics;
using UnityEngine.UI;


public class AddInputField : MonoBehaviour, IPointerClickHandler
{
    public GameObject newFieldButton; //the button that has 'add new'
    public GameObject inputField1;//first field
    public GameObject individualCloseButton; //the close button used for individual fields.
    public GameObject individualOnOffButton; //on/off button for individual fields
    public GameObject individualSettingsButton; //settings button for individual field.s

    public GameObject toolTip;
     
    /*
    public GameObject defaultInputField; //default field that we copy for each of the individual functions
    public GameObject defaultButton;//default button we copy
    public GameObject defaultTextBox; // default text box we copy
       */

    public GameObject equationContent; //equationContent will be the parent of all these input fields. its the 'content' of the scrollview.
    public GameObject scrollView;

    public static Dictionary<string, GameObject> fields = new Dictionary<string, GameObject>();


    GameObject functionInterface;
    GameObject outputField;

    public static float equationInputFieldTop;
    public static float equationInputFieldBottom; // Because we will add multiple input fields and panels (for the other functions) of different sizes, we will need to keep track of where the bottom of the bottommost is.
    //this is so that the next one can be placed below this.
    //this has to be static because it will be accessed from many scripts.



    




    public GameObject panelPrefab;
    public GameObject inputFieldPrefab;
    //public GameObject placeHolderPrefab;
    public GameObject textBoxPrefab;
    public GameObject buttonPrefab;
    public GameObject closeButtonPrefab;


    VectorHandler vectorHandlerScript;
    CartesianSphereHandler cartesianSphereHandlerScript;
    VectorLineHandler vectorLineHandlerScript;
    MainEquationHandler mainEquationHandlerScript;



    static bool firstFieldAdded = false;





    static int fieldNum = 1;



    public GameObject Canvas;
    /// public GameObject scrollview;
    ///public GameObject viewPort;



    //note - originally the sole purpose of this script was to add the new input fields
    //however, it was later changed to have functionality that allowed special functions (in a menu). those fuctions have to be in this script, because they, and the input fields
    //share a common static variable equationInputFieldYPosition ( above).




    private void Start()


    {
        vectorHandlerScript = GameObject.FindGameObjectWithTag("NewFieldButton").GetComponent<VectorHandler>();
        cartesianSphereHandlerScript = GameObject.FindGameObjectWithTag("NewFieldButton").GetComponent<CartesianSphereHandler>();
        vectorLineHandlerScript = GameObject.FindGameObjectWithTag("NewFieldButton").GetComponent<VectorLineHandler>();
        mainEquationHandlerScript = GameObject.FindGameObjectWithTag("NewFieldButton").GetComponent<MainEquationHandler>();

        if (firstFieldAdded == false)
        {
            equationInputFieldTop = 0;
            equationInputFieldBottom = 0;




            //fields.Add("Field1", inputField1);


            //equationInputFieldTop = equationInputFieldTop + 68;
            //equationInputFieldBottom = equationInputFieldBottom - 68;//68 is the height of the inputfield 
            //we decrease it again by -68 for the next UI elmenet

            firstFieldAdded = true;

        }









    }





    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            print($"" +
         $"Canvas localscale  = {Canvas.transform.localScale} \n" +
         $"Canvas width and height = {Canvas.GetComponent<RectTransform>().rect.width   }, {Canvas.GetComponent<RectTransform>().rect.height}\n" +
         $"Canvas transform position = {Canvas.GetComponent<RectTransform>().rect.x}, {Canvas.GetComponent<RectTransform>().rect.y}\n" +


         $"inputField1 rect width and height = {newFieldButton.GetComponent<RectTransform>().rect.width   }, {newFieldButton.GetComponent<RectTransform>().rect.height}\n" +
         $"inputField1 rect x y = {newFieldButton.GetComponent<RectTransform>().rect.x}, {newFieldButton.GetComponent<RectTransform>().rect.y}\n" +
          $"inputField1 recttransform transform position = {newFieldButton.GetComponent<RectTransform>().transform.position} \n" +
         $"inputField1 transform x y = {newFieldButton.transform.position.x}, {newFieldButton.transform.position.y}\n" +
         $"inputField1 sizedelta = {newFieldButton.GetComponent<RectTransform>().sizeDelta.x}, {newFieldButton.GetComponent<RectTransform>().sizeDelta.y}\n" +
         $"inputField1 anchoredPosition = {newFieldButton.GetComponent<RectTransform>().anchoredPosition.x}, {newFieldButton.GetComponent<RectTransform>().anchoredPosition.y}\n" +
         $"inputField1 anchor min, max = {newFieldButton.GetComponent<RectTransform>().anchorMin}, {newFieldButton.GetComponent<RectTransform>().anchorMin}\n" +


         $"Screen width, height = {Screen.width}, {Screen.height}\n" +

         $"Cursor x, y = {Input.mousePosition.x}, {Input.mousePosition.y}\n" +
         $"bottom   = {equationInputFieldBottom}, \n" +

         $"");


        }

    }
    public void createNewField() //should run when newFieldButton clicked

    {        //newInputField.AddComponent<InputField>(); we dont need to do this because TMP_Inputfield is already applied (it inherits it from prefab)
        /*
        float fieldHeight = 2 * newFieldButton.GetComponent<RectTransform>().rect.height;
        /*
        print("fieldheight = " + fieldHeight);
        print("scrollview height = " + scrollView.GetComponent<RectTransform>().rect.height);
        print("screen height = " + Screen.height);
       
         //================================================================================================================================================================================================
         /*The following code positions and sets the dimensions of the new inputbox.

        fieldHeight = (Screen.height / scrollView.GetComponent<RectTransform>().rect.height) * newFieldButton.GetComponent<RectTransform>().rect.height;


        //  float inputFieldHeight = newInputField.GetComponent<InputField>().transform.localScale.y;



        newInputField.transform.position = new Vector2(inputField1.transform.position.x, inputField1.transform.position.y - 2 * fieldHeight * (fieldNum - 1));
        newFieldButton.transform.position = new Vector2(newFieldButton.transform.position.x, newFieldButton.transform.position.y - 2 * fieldHeight);

        *///=======================================================================================================================================================================================
          //print($"{ equationInputFieldTop}, { equationInputFieldBottom}");
          // newInputField.GetComponent<RectTransform>().anchoredPosition = new Vector3(equationInputFieldTop, equationInputFieldBottom, 0);





        equationContent = GameObject.Find("EquationContent");

        GameObject newInputField = Instantiate(inputField1, equationContent.transform);




        newInputField.GetComponent<RectTransform>().anchoredPosition = new Vector3(45, equationInputFieldBottom, 0);
        newFieldButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, equationInputFieldBottom - 68, 0);
        //this is how to set the position that is seen in the editor


        newInputField.transform.name = "inputField" + fieldNum;




        newInputField.GetComponent<TMP_InputField>().text = ""; //sets the text in the new input field to ""
        newInputField.transform.Find("Output").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "";
        newInputField.transform.Find("Output").transform.Find("Placeholder").GetComponent<TextMeshProUGUI>().text = "=";


        //the closeButton
        GameObject closeButton1 = Instantiate(individualCloseButton, newInputField.transform);
        closeButton1.transform.name = $"closeButton1.{fieldNum}";
        RectTransform closeButtonRectTransform1 = closeButton1.GetComponent<RectTransform>();
        closeButtonRectTransform1.sizeDelta = new Vector2(34, 30);
        closeButtonRectTransform1.localScale = new Vector3(1, 1, 1);
        closeButtonRectTransform1.anchorMin = new Vector2(1, 1);
        closeButtonRectTransform1.anchorMax = new Vector2(1, 1);
        closeButtonRectTransform1.pivot = new Vector2(0, 1);
        closeButtonRectTransform1.anchoredPosition = new Vector3(-34,-2, 0);

        //the color on/off button
        GameObject onOffButton = Instantiate(individualOnOffButton, newInputField.transform);
        onOffButton.transform.name = $"onOffButton.{fieldNum}";
        RectTransform onOffButtonRectTransform = onOffButton.GetComponent<RectTransform>();
        onOffButtonRectTransform.sizeDelta = new Vector2(45, 32);
        onOffButtonRectTransform.localScale = new Vector3(1, 1, 1);
        onOffButtonRectTransform.anchorMin = new Vector2(0,1);
        onOffButtonRectTransform.anchorMax = new Vector2(0, 1);
        onOffButtonRectTransform.pivot = new Vector2(0, 1);
        onOffButtonRectTransform.anchoredPosition = new Vector3(-45,-2, 0);

        //the settings button
        GameObject settingsButton = Instantiate(individualSettingsButton, newInputField.transform);
        settingsButton.transform.name = $"settingsButton.{fieldNum}";
        RectTransform settingsButtonRectTransform = settingsButton.GetComponent<RectTransform>();
        settingsButtonRectTransform.sizeDelta = new Vector2(45, 32);
        settingsButtonRectTransform.localScale = new Vector3(1, 1, 1);
        settingsButtonRectTransform.anchorMin = new Vector2(0, 1);
        settingsButtonRectTransform.anchorMax = new Vector2(0, 1);
        settingsButtonRectTransform.pivot = new Vector2(0, 1);
        settingsButtonRectTransform.anchoredPosition = new Vector3(-45, -34, 0);

        
        //the tooltip button
        GameObject toolTipButton = Instantiate(toolTip, newInputField.transform);
        toolTipButton.transform.name = $"toolTipButton.{fieldNum}";
        RectTransform toolTipButtonRectTransform = toolTipButton.GetComponent<RectTransform>();
        toolTipButtonRectTransform.sizeDelta = new Vector2(34, 34);
        toolTipButtonRectTransform.localScale = new Vector3(1, 1, 1);
        toolTipButtonRectTransform.anchorMin = new Vector2(1, 0);
        toolTipButtonRectTransform.anchorMax = new Vector2(1, 0);
        toolTipButtonRectTransform.pivot = new Vector2(0, 1);
        toolTipButtonRectTransform.anchoredPosition = new Vector3(-34, 36, 0);
        toolTipButton.AddComponent<toolTipScript>();



        fields.Add((newInputField.transform.name), newInputField);



        fieldNum = fieldNum + 1; //thjis is obsolete but we'll just have it here anyways inca se we need in the future
        equationInputFieldTop = equationInputFieldTop + 68;
        equationInputFieldBottom = equationInputFieldBottom - 68;


    }

    //for all of these, first what must be done is their input sections must be added.
    //THEN, the values can be changed to execute whatever process they tryna do.

    //for their input sections, this means their UI - 
    //They will all have a "Panel" as their parent. The parent of the "Panel" will be "EquationContent".
    //this panel will basically hold all the buttons, text fields, input fields, whatever.
    //also, to determine where the "New input field" button, and the location of the current and next input fields will be, there will need to be a static counter that 



    /*
    GameObject createInputFieldGameObject()
    {
        GameObject inputField = new GameObject();

        RectTransform rectTransform = inputField.AddComponent<RectTransform>();
        CanvasRenderer canvasRenderer = inputField.AddComponent<CanvasRenderer>();
        Image image = inputField.AddComponent<Image>(); 
        TMP_InputField tmpInputField = inputField.AddComponent<TMP_InputField>();
        ContentSizeFitter contentSizeFitter = inputField.AddComponent<ContentSizeFitter>();

        //this will essentially duplicate the 


        return inputField;
    }
    */

    void moveAddFieldButton()
    {
        equationContent = GameObject.Find("EquationContent");




        newFieldButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, equationInputFieldBottom - 68, 0);
        //this is how to set the position that is seen in the editor








        equationInputFieldTop = equationInputFieldTop + 68;
        equationInputFieldBottom = equationInputFieldBottom - 68;
    }

    public void menuFunctionOne()//this is function 1. Check if two vectors/  are parallel
    {

        //naming system - $"{objectname}.{fieldnum}"


        GameObject parentPanel = Instantiate(panelPrefab, equationContent.transform);
        parentPanel.transform.name = "ParentPanel" + fieldNum;
        RectTransform parentPanelRectTransform = parentPanel.GetComponent<RectTransform>();
        parentPanelRectTransform.anchorMin = new Vector2(0, 1);
        parentPanelRectTransform.anchorMax = new Vector2(1, 1);
        parentPanelRectTransform.pivot = new Vector2(0, 1);
        parentPanelRectTransform.right = Vector3.zero;
        parentPanelRectTransform.sizeDelta = new Vector2(parentPanelRectTransform.sizeDelta.x, 170f); //4 ui elements, each 34 units tall
        parentPanelRectTransform.anchoredPosition = new Vector3(0, equationInputFieldBottom, 0);


        //==========

        GameObject titleLabel = Instantiate(textBoxPrefab, parentPanel.transform);
        titleLabel.transform.name = $"titleLabel.{fieldNum}";
        RectTransform titleLabelRectTransform = titleLabel.GetComponent<RectTransform>();
        titleLabelRectTransform.anchorMin = new Vector2(0, 1);
        titleLabelRectTransform.anchorMax = new Vector2(0, 1);
        titleLabelRectTransform.pivot = new Vector2(0, 1);
        titleLabelRectTransform.sizeDelta = new Vector2(1875, 34f);
        titleLabelRectTransform.anchoredPosition = new Vector3(0, 0, 0);
        TextMeshProUGUI titleLabelTMPUGUI = titleLabel.GetComponent<TextMeshProUGUI>();
        titleLabelTMPUGUI.text = "Check if two vectors are parallel";
        titleLabelTMPUGUI.fontStyle = FontStyles.Bold;
        titleLabelTMPUGUI.color = Color.black;






        //=======


        GameObject textBox1 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox1.transform.name = $"textBox1.{fieldNum}";
        RectTransform textBox1RectTransform = textBox1.GetComponent<RectTransform>();
        textBox1RectTransform.anchorMin = new Vector2(0, 1);
        textBox1RectTransform.anchorMax = new Vector2(0, 1);
        textBox1RectTransform.pivot = new Vector2(0, 1);
        textBox1RectTransform.right = Vector3.zero;
        textBox1RectTransform.anchoredPosition = new Vector3(0, -34, 0);
        TextMeshProUGUI textBox1TMPUGUI = textBox1.GetComponent<TextMeshProUGUI>();
        textBox1TMPUGUI.text = "Vector 1 = ";
        textBox1TMPUGUI.fontStyle = FontStyles.Normal;
        textBox1TMPUGUI.color = Color.black;




        GameObject inputField1 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField1RectTransform = inputField1.GetComponent<RectTransform>();
        inputField1.transform.name = $"InputField1.{fieldNum}";
        inputField1RectTransform.anchorMin = new Vector2(0, 1);
        inputField1RectTransform.anchorMax = new Vector2(0, 1);
        inputField1RectTransform.pivot = new Vector2(0, 1);
        inputField1RectTransform.right = Vector3.zero;     //size
        inputField1RectTransform.anchoredPosition = new Vector3(150, -34, 0);     //position
        Transform inputField1Placeholder = inputField1.transform.Find("Placeholder");
        inputField1Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z)";


        //=====================


        GameObject textBox2 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox2.transform.name = $"textBox2.{fieldNum}";
        RectTransform textBox2RectTransform = textBox2.GetComponent<RectTransform>();
        textBox2RectTransform.anchorMin = new Vector2(0, 1);
        textBox2RectTransform.anchorMax = new Vector2(0, 1);
        textBox2RectTransform.pivot = new Vector2(0, 1);
        textBox2RectTransform.right = Vector3.zero;
        textBox2RectTransform.anchoredPosition = new Vector3(0, -68, 0);
        TextMeshProUGUI textBox2TMPUGUI = textBox2.GetComponent<TextMeshProUGUI>();
        textBox2TMPUGUI.text = "Vector 2 = ";
        textBox2TMPUGUI.fontStyle = FontStyles.Normal;
        textBox2TMPUGUI.color = Color.black;




        GameObject inputField2 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField2RectTransform = inputField2.GetComponent<RectTransform>();
        inputField2.transform.name = $"InputField2.{fieldNum}";
        inputField2RectTransform.anchorMin = new Vector2(0, 1);
        inputField2RectTransform.anchorMax = new Vector2(0, 1);
        inputField2RectTransform.pivot = new Vector2(0, 1);
        inputField2RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField2RectTransform.anchoredPosition = new Vector3(150, -68, 0);     //position
        Transform inputField2Placeholder = inputField2.transform.Find("Placeholder");
        inputField2Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z)";


        //====================


        GameObject textBox3 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox3.transform.name = $"textBox3.{fieldNum}";
        RectTransform textBox3RectTransform = textBox3.GetComponent<RectTransform>();
        textBox3RectTransform.anchorMin = new Vector2(0, 1);
        textBox3RectTransform.anchorMax = new Vector2(0, 1);
        textBox3RectTransform.pivot = new Vector2(0, 1);
        textBox3RectTransform.sizeDelta = new Vector2(1875, 30f);
        textBox3RectTransform.anchoredPosition = new Vector3(0, -102, 0);
        TextMeshProUGUI textBox3TMPUGUI = textBox3.GetComponent<TextMeshProUGUI>();
        textBox3TMPUGUI.text = "Result: ";
        textBox3TMPUGUI.fontStyle = FontStyles.Normal;
        textBox3TMPUGUI.color = Color.black;



        //====================


        GameObject button = Instantiate(buttonPrefab, parentPanel.transform);
        button.transform.name = $"button.{fieldNum}";
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        buttonRectTransform.anchorMin = new Vector2(0, 1);
        buttonRectTransform.anchorMax = new Vector2(0, 1);
        buttonRectTransform.pivot = new Vector2(0, 1);
        buttonRectTransform.sizeDelta = new Vector2(270, 34f);
        buttonRectTransform.anchoredPosition = new Vector3(0, -136, 0);
        button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Calculate";
        button.GetComponent<Button>().onClick.AddListener(delegate { computeFunctionOne(parentPanel); }); //this works - it successfully passes parentPanel as a parameter.




        //=============

        GameObject closeButton = Instantiate(closeButtonPrefab, parentPanel.transform);
        closeButton.transform.name = $"closeButton.{fieldNum}";
        RectTransform closeButtonRectTransform = closeButton.GetComponent<RectTransform>();
        closeButtonRectTransform.sizeDelta = new Vector2(45, 34f);
        closeButtonRectTransform.anchoredPosition = new Vector3(-45, 0, 0);
        closeButton.transform.GetComponent<Image>().color = new Color(255, 255, 255, 255);




        //===============

        fieldNum = fieldNum + 1;

        equationInputFieldTop = equationInputFieldTop + 3 * 34;
        equationInputFieldBottom = equationInputFieldBottom - 3 * 34;
        moveAddFieldButton();


        fields.Add((parentPanel.transform.name), parentPanel);
    }
    public void menuFunctionTwo()//this is function 2. Check if two vectors/lines are perpendicular
    {
        GameObject parentPanel = Instantiate(panelPrefab, equationContent.transform);
        parentPanel.transform.name = "ParentPanel" + fieldNum;
        RectTransform parentPanelRectTransform = parentPanel.GetComponent<RectTransform>();
        parentPanelRectTransform.anchorMin = new Vector2(0, 1);
        parentPanelRectTransform.anchorMax = new Vector2(1, 1);
        parentPanelRectTransform.pivot = new Vector2(0, 1);
        parentPanelRectTransform.right = Vector3.zero;
        parentPanelRectTransform.sizeDelta = new Vector2(parentPanelRectTransform.sizeDelta.x, 170f); //4 ui elements, each 34 units tall
        parentPanelRectTransform.anchoredPosition = new Vector3(0, equationInputFieldBottom, 0);

        //==========

        GameObject titleLabel = Instantiate(textBoxPrefab, parentPanel.transform);
        titleLabel.transform.name = $"titleLabel.{fieldNum}";
        RectTransform titleLabelRectTransform = titleLabel.GetComponent<RectTransform>();
        titleLabelRectTransform.anchorMin = new Vector2(0, 1);
        titleLabelRectTransform.anchorMax = new Vector2(0, 1);
        titleLabelRectTransform.pivot = new Vector2(0, 1);
        titleLabelRectTransform.sizeDelta = new Vector2(1875, 34f);
        titleLabelRectTransform.anchoredPosition = new Vector3(0, 0, 0);
        TextMeshProUGUI titleLabelTMPUGUI = titleLabel.GetComponent<TextMeshProUGUI>();
        titleLabelTMPUGUI.text = "Check if two vectors/lines are perpendicular";
        titleLabelTMPUGUI.fontStyle = FontStyles.Bold;
        titleLabelTMPUGUI.color = Color.black;



        //========


        GameObject textBox1 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox1.transform.name = $"textBox1.{fieldNum}";
        RectTransform textBox1RectTransform = textBox1.GetComponent<RectTransform>();
        textBox1RectTransform.anchorMin = new Vector2(0, 1);
        textBox1RectTransform.anchorMax = new Vector2(0, 1);
        textBox1RectTransform.pivot = new Vector2(0, 1);
        textBox1RectTransform.sizeDelta = new Vector2(150, 34f);
        textBox1RectTransform.anchoredPosition = new Vector3(0, -34, 0);
        TextMeshProUGUI textBox1TMPUGUI = textBox1.GetComponent<TextMeshProUGUI>();
        textBox1TMPUGUI.text = "Vector 1 = ";
        textBox1TMPUGUI.fontStyle = FontStyles.Normal;
        textBox1TMPUGUI.color = Color.black;




        GameObject inputField1 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField1RectTransform = inputField1.GetComponent<RectTransform>();
        inputField1.transform.name = $"InputField1.{fieldNum}";
        inputField1RectTransform.anchorMin = new Vector2(0, 1);
        inputField1RectTransform.anchorMax = new Vector2(0, 1);
        inputField1RectTransform.pivot = new Vector2(0, 1);
        inputField1RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField1RectTransform.anchoredPosition = new Vector3(150, -34, 0);     //position
        Transform inputField1Placeholder = inputField1.transform.Find("Placeholder");
        inputField1Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z)";


        //=====================


        GameObject textBox2 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox2.transform.name = $"textBox2.{fieldNum}";
        RectTransform textBox2RectTransform = textBox2.GetComponent<RectTransform>();
        textBox2RectTransform.anchorMin = new Vector2(0, 1);
        textBox2RectTransform.anchorMax = new Vector2(0, 1);
        textBox2RectTransform.pivot = new Vector2(0, 1);
        textBox2RectTransform.sizeDelta = new Vector2(150, 34f);
        textBox2RectTransform.anchoredPosition = new Vector3(0, -68, 0);
        TextMeshProUGUI textBox2TMPUGUI = textBox2.GetComponent<TextMeshProUGUI>();
        textBox2TMPUGUI.text = "Vector 2 = ";
        textBox2TMPUGUI.fontStyle = FontStyles.Normal;
        textBox2TMPUGUI.color = Color.black;




        GameObject inputField2 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField2RectTransform = inputField2.GetComponent<RectTransform>();
        inputField2.transform.name = $"InputField2.{fieldNum}";
        inputField2RectTransform.anchorMin = new Vector2(0, 1);
        inputField2RectTransform.anchorMax = new Vector2(0, 1);
        inputField2RectTransform.pivot = new Vector2(0, 1);
        inputField2RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField2RectTransform.anchoredPosition = new Vector3(150, -68, 0);     //position
        Transform inputField2Placeholder = inputField2.transform.Find("Placeholder");
        inputField2Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z)";


        //====================


        GameObject textBox3 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox3.transform.name = $"textBox3.{fieldNum}";
        RectTransform textBox3RectTransform = textBox3.GetComponent<RectTransform>();
        textBox3RectTransform.anchorMin = new Vector2(0, 1);
        textBox3RectTransform.anchorMax = new Vector2(0, 1);
        textBox3RectTransform.pivot = new Vector2(0, 1);
        textBox3RectTransform.sizeDelta = new Vector2(1875, 34f);
        textBox3RectTransform.anchoredPosition = new Vector3(0, -102, 0);
        TextMeshProUGUI textBox3TMPUGUI = textBox3.GetComponent<TextMeshProUGUI>();
        textBox3TMPUGUI.text = "Result: ";
        textBox3TMPUGUI.fontStyle = FontStyles.Normal;
        textBox3TMPUGUI.color = Color.black;



        //====================


        GameObject button = Instantiate(buttonPrefab, parentPanel.transform);
        button.transform.name = $"button.{fieldNum}";
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        buttonRectTransform.anchorMin = new Vector2(0, 1);
        buttonRectTransform.anchorMax = new Vector2(0, 1);
        buttonRectTransform.pivot = new Vector2(0, 1);
        buttonRectTransform.sizeDelta = new Vector2(270, 34f);
        buttonRectTransform.anchoredPosition = new Vector3(0, -136, 0);
        button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Calculate";
        button.GetComponent<Button>().onClick.AddListener(delegate { computeFunctionTwo(parentPanel); });

        //=============

        GameObject closeButton = Instantiate(closeButtonPrefab, parentPanel.transform);
        closeButton.transform.name = $"closeButton.{fieldNum}";
        RectTransform closeButtonRectTransform = closeButton.GetComponent<RectTransform>();
        closeButtonRectTransform.sizeDelta = new Vector2(45, 34f);
        closeButtonRectTransform.anchoredPosition = new Vector3(-45, 0, 0);
        closeButton.transform.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        //===============



        fieldNum = fieldNum + 1;

        equationInputFieldTop = equationInputFieldTop + 3 * 34;
        equationInputFieldBottom = equationInputFieldBottom - 3 * 34;
        moveAddFieldButton();

        fields.Add((parentPanel.transform.name), parentPanel);
    }
    public void menuFunctionThree()//this is function 3. Check if a point lies on a line
    {
        GameObject parentPanel = Instantiate(panelPrefab, equationContent.transform);
        parentPanel.transform.name = "ParentPanel" + fieldNum;
        RectTransform parentPanelRectTransform = parentPanel.GetComponent<RectTransform>();
        parentPanelRectTransform.anchorMin = new Vector2(0, 1);
        parentPanelRectTransform.anchorMax = new Vector2(1, 1);
        parentPanelRectTransform.pivot = new Vector2(0, 1);
        parentPanelRectTransform.right = Vector3.zero;
        parentPanelRectTransform.sizeDelta = new Vector2(parentPanelRectTransform.sizeDelta.x, 170f); //4 ui elements, each 34 units tall
        parentPanelRectTransform.anchoredPosition = new Vector3(0, equationInputFieldBottom, 0);

        //==========

        GameObject titleLabel = Instantiate(textBoxPrefab, parentPanel.transform);
        titleLabel.transform.name = $"titleLabel.{fieldNum}";
        RectTransform titleLabelRectTransform = titleLabel.GetComponent<RectTransform>();
        titleLabelRectTransform.anchorMin = new Vector2(0, 1);
        titleLabelRectTransform.anchorMax = new Vector2(0, 1);
        titleLabelRectTransform.pivot = new Vector2(0, 1);
        titleLabelRectTransform.sizeDelta = new Vector2(1875, 34f);
        titleLabelRectTransform.anchoredPosition = new Vector3(0, 0, 0);
        TextMeshProUGUI titleLabelTMPUGUI = titleLabel.GetComponent<TextMeshProUGUI>();
        titleLabelTMPUGUI.text = "Check if a point lies on a line";
        titleLabelTMPUGUI.fontStyle = FontStyles.Bold;
        titleLabelTMPUGUI.color = Color.black;



        //========


        GameObject textBox1 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox1.transform.name = $"textBox1.{fieldNum}";
        RectTransform textBox1RectTransform = textBox1.GetComponent<RectTransform>();
        textBox1RectTransform.anchorMin = new Vector2(0, 1);
        textBox1RectTransform.anchorMax = new Vector2(0, 1);
        textBox1RectTransform.pivot = new Vector2(0, 1);
        textBox1RectTransform.sizeDelta = new Vector2(150, 34f);
        textBox1RectTransform.anchoredPosition = new Vector3(0, -34, 0);
        TextMeshProUGUI textBox1TMPUGUI = textBox1.GetComponent<TextMeshProUGUI>();
        textBox1TMPUGUI.text = "Point = ";
        textBox1TMPUGUI.fontStyle = FontStyles.Normal;
        textBox1TMPUGUI.color = Color.black;




        GameObject inputField1 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField1RectTransform = inputField1.GetComponent<RectTransform>();
        inputField1.transform.name = $"InputField1.{fieldNum}";
        inputField1RectTransform.anchorMin = new Vector2(0, 1);
        inputField1RectTransform.anchorMax = new Vector2(0, 1);
        inputField1RectTransform.pivot = new Vector2(0, 1);
        inputField1RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField1RectTransform.anchoredPosition = new Vector3(150, -34, 0);     //position
        Transform inputField1Placeholder = inputField1.transform.Find("Placeholder");
        inputField1Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z)";


        //=====================


        GameObject textBox2 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox2.transform.name = $"textBox2.{fieldNum}";
        RectTransform textBox2RectTransform = textBox2.GetComponent<RectTransform>();
        textBox2RectTransform.anchorMin = new Vector2(0, 1);
        textBox2RectTransform.anchorMax = new Vector2(0, 1);
        textBox2RectTransform.pivot = new Vector2(0, 1);
        textBox2RectTransform.sizeDelta = new Vector2(150, 34f);
        textBox2RectTransform.anchoredPosition = new Vector3(0, -68, 0);
        TextMeshProUGUI textBox2TMPUGUI = textBox2.GetComponent<TextMeshProUGUI>();
        textBox2TMPUGUI.text = "Line = ";
        textBox2TMPUGUI.fontStyle = FontStyles.Normal;
        textBox2TMPUGUI.color = Color.black;




        GameObject inputField2 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField2RectTransform = inputField2.GetComponent<RectTransform>();
        inputField2.transform.name = $"InputField2.{fieldNum}";
        inputField2RectTransform.anchorMin = new Vector2(0, 1);
        inputField2RectTransform.anchorMax = new Vector2(0, 1);
        inputField2RectTransform.pivot = new Vector2(0, 1);
        inputField2RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField2RectTransform.anchoredPosition = new Vector3(150, -68, 0);     //position
        Transform inputField2Placeholder = inputField2.transform.Find("Placeholder");
        inputField2Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z) + λ(a, b, c)";
        //inputField2.onvaluechanged GetComponent<TMPro>().onValueChanged.AddListener(delegate { computeFunctionThree(parentPanel); });

        //====================


        GameObject textBox3 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox3.transform.name = $"textBox3.{fieldNum}";
        RectTransform textBox3RectTransform = textBox3.GetComponent<RectTransform>();
        textBox3RectTransform.anchorMin = new Vector2(0, 1);
        textBox3RectTransform.anchorMax = new Vector2(0, 1);
        textBox3RectTransform.pivot = new Vector2(0, 1);
        textBox3RectTransform.sizeDelta = new Vector2(1875, 34f);
        textBox3RectTransform.anchoredPosition = new Vector3(0, -102, 0);
        TextMeshProUGUI textBox3TMPUGUI = textBox3.GetComponent<TextMeshProUGUI>();
        textBox3TMPUGUI.text = "Result: ";
        textBox3TMPUGUI.fontStyle = FontStyles.Normal;
        textBox3TMPUGUI.color = Color.black;



        //====================


        GameObject button = Instantiate(buttonPrefab, parentPanel.transform);
        button.transform.name = $"button.{fieldNum}";
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        buttonRectTransform.anchorMin = new Vector2(0, 1);
        buttonRectTransform.anchorMax = new Vector2(0, 1);
        buttonRectTransform.pivot = new Vector2(0, 1);
        buttonRectTransform.sizeDelta = new Vector2(270, 34f);
        buttonRectTransform.anchoredPosition = new Vector3(0, -136, 0);
        button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Calculate";
        button.GetComponent<Button>().onClick.AddListener(delegate { computeFunctionThree(parentPanel); });

        //=============

        GameObject closeButton = Instantiate(closeButtonPrefab, parentPanel.transform);
        closeButton.transform.name = $"closeButton.{fieldNum}";
        RectTransform closeButtonRectTransform = closeButton.GetComponent<RectTransform>();
        closeButtonRectTransform.sizeDelta = new Vector2(45, 34f);
        closeButtonRectTransform.anchoredPosition = new Vector3(-45, 0, 0);
        closeButton.transform.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        //===============



        fieldNum = fieldNum + 1;

        equationInputFieldTop = equationInputFieldTop + 3 * 34;
        equationInputFieldBottom = equationInputFieldBottom - 3 * 34;
        moveAddFieldButton();
        fields.Add((parentPanel.transform.name), parentPanel);
    }
    public void menuFunctionFour()//this is function 4. Find acute angle between two vectors/lines
    {
        GameObject parentPanel = Instantiate(panelPrefab, equationContent.transform);
        parentPanel.transform.name = "ParentPanel" + fieldNum;
        RectTransform parentPanelRectTransform = parentPanel.GetComponent<RectTransform>();
        parentPanelRectTransform.anchorMin = new Vector2(0, 1);
        parentPanelRectTransform.anchorMax = new Vector2(1, 1);
        parentPanelRectTransform.pivot = new Vector2(0, 1);
        parentPanelRectTransform.right = Vector3.zero;
        parentPanelRectTransform.sizeDelta = new Vector2(parentPanelRectTransform.sizeDelta.x, 170f); //4 ui elements, each 34 units tall
        parentPanelRectTransform.anchoredPosition = new Vector3(0, equationInputFieldBottom, 0);


        //==========

        GameObject titleLabel = Instantiate(textBoxPrefab, parentPanel.transform);
        titleLabel.transform.name = $"titleLabel.{fieldNum}";
        RectTransform titleLabelRectTransform = titleLabel.GetComponent<RectTransform>();
        titleLabelRectTransform.anchorMin = new Vector2(0, 1);
        titleLabelRectTransform.anchorMax = new Vector2(0, 1);
        titleLabelRectTransform.pivot = new Vector2(0, 1);
        titleLabelRectTransform.sizeDelta = new Vector2(1875, 34f);
        titleLabelRectTransform.anchoredPosition = new Vector3(0, 0, 0);
        TextMeshProUGUI titleLabelTMPUGUI = titleLabel.GetComponent<TextMeshProUGUI>();
        titleLabelTMPUGUI.text = "Find acute angle between two vectors/lines";
        titleLabelTMPUGUI.fontStyle = FontStyles.Bold;
        titleLabelTMPUGUI.color = Color.black;


        //========


        GameObject textBox1 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox1.transform.name = $"textBox1.{fieldNum}";
        RectTransform textBox1RectTransform = textBox1.GetComponent<RectTransform>();
        textBox1RectTransform.anchorMin = new Vector2(0, 1);
        textBox1RectTransform.anchorMax = new Vector2(0, 1);
        textBox1RectTransform.pivot = new Vector2(0, 1);
        textBox1RectTransform.sizeDelta = new Vector2(200, 34f);
        textBox1RectTransform.anchoredPosition = new Vector3(0, -34, 0);
        TextMeshProUGUI textBox1TMPUGUI = textBox1.GetComponent<TextMeshProUGUI>();
        textBox1TMPUGUI.text = "Vector/line 1 = ";
        textBox1TMPUGUI.fontStyle = FontStyles.Normal;
        textBox1TMPUGUI.color = Color.black;




        GameObject inputField1 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField1RectTransform = inputField1.GetComponent<RectTransform>();
        inputField1.transform.name = $"InputField1.{fieldNum}";
        inputField1RectTransform.anchorMin = new Vector2(0, 1);
        inputField1RectTransform.anchorMax = new Vector2(0, 1);
        inputField1RectTransform.pivot = new Vector2(0, 1);
        inputField1RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField1RectTransform.anchoredPosition = new Vector3(200, -34, 0);     //position
        Transform inputField1Placeholder = inputField1.transform.Find("Placeholder");
        inputField1Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z) or (x, y, z) + λ(a, b, c)";


        //=====================


        GameObject textBox2 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox2.transform.name = $"textBox2.{fieldNum}";
        RectTransform textBox2RectTransform = textBox2.GetComponent<RectTransform>();
        textBox2RectTransform.anchorMin = new Vector2(0, 1);
        textBox2RectTransform.anchorMax = new Vector2(0, 1);
        textBox2RectTransform.pivot = new Vector2(0, 1);
        textBox2RectTransform.sizeDelta = new Vector2(200, 34f);
        textBox2RectTransform.anchoredPosition = new Vector3(0, -68, 0);
        TextMeshProUGUI textBox2TMPUGUI = textBox2.GetComponent<TextMeshProUGUI>();
        textBox2TMPUGUI.text = "Vector/line 2 = ";
        textBox2TMPUGUI.fontStyle = FontStyles.Normal;
        textBox2TMPUGUI.color = Color.black;




        GameObject inputField2 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField2RectTransform = inputField2.GetComponent<RectTransform>();
        inputField2.transform.name = $"InputField2.{fieldNum}";
        inputField2RectTransform.anchorMin = new Vector2(0, 1);
        inputField2RectTransform.anchorMax = new Vector2(0, 1);
        inputField2RectTransform.pivot = new Vector2(0, 1);
        inputField2RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField2RectTransform.anchoredPosition = new Vector3(200, -68, 0);     //position
        Transform inputField2Placeholder = inputField2.transform.Find("Placeholder");
        inputField2Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z) or (x, y, z) + λ(a, b, c)";


        //====================


        GameObject textBox3 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox3.transform.name = $"textBox3.{fieldNum}";
        RectTransform textBox3RectTransform = textBox3.GetComponent<RectTransform>();
        textBox3RectTransform.anchorMin = new Vector2(0, 1);
        textBox3RectTransform.anchorMax = new Vector2(0, 1);
        textBox3RectTransform.pivot = new Vector2(0, 1);
        textBox3RectTransform.sizeDelta = new Vector2(1875, 34f);
        textBox3RectTransform.anchoredPosition = new Vector3(0, -102, 0);
        TextMeshProUGUI textBox3TMPUGUI = textBox3.GetComponent<TextMeshProUGUI>();
        textBox3TMPUGUI.text = "Result: ";
        textBox3TMPUGUI.fontStyle = FontStyles.Normal;
        textBox3TMPUGUI.color = Color.black;



        //====================


        GameObject button = Instantiate(buttonPrefab, parentPanel.transform);
        button.transform.name = $"button.{fieldNum}";
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        buttonRectTransform.anchorMin = new Vector2(0, 1);
        buttonRectTransform.anchorMax = new Vector2(0, 1);
        buttonRectTransform.pivot = new Vector2(0, 1);
        buttonRectTransform.sizeDelta = new Vector2(270, 34f);
        buttonRectTransform.anchoredPosition = new Vector3(0, -136, 0);
        button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Calculate";
        button.GetComponent<Button>().onClick.AddListener(delegate { computeFunctionFour(parentPanel); });

        //=============

        GameObject closeButton = Instantiate(closeButtonPrefab, parentPanel.transform);
        closeButton.transform.name = $"closeButton.{fieldNum}";
        RectTransform closeButtonRectTransform = closeButton.GetComponent<RectTransform>();
        closeButtonRectTransform.sizeDelta = new Vector2(45, 34f);
        closeButtonRectTransform.anchoredPosition = new Vector3(-45, 0, 0);
        closeButton.transform.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        //===============



        fieldNum = fieldNum + 1;

        equationInputFieldTop = equationInputFieldTop + 3 * 34;
        equationInputFieldBottom = equationInputFieldBottom - 3 * 34;
        moveAddFieldButton();

        fields.Add((parentPanel.transform.name), parentPanel);
    }
    public void menuFunctionFive()//this is function 5. Find intersection/skew/parallel of two lines
    {
        GameObject parentPanel = Instantiate(panelPrefab, equationContent.transform);
        parentPanel.transform.name = "ParentPanel" + fieldNum;
        RectTransform parentPanelRectTransform = parentPanel.GetComponent<RectTransform>();
        parentPanelRectTransform.anchorMin = new Vector2(0, 1);
        parentPanelRectTransform.anchorMax = new Vector2(1, 1);
        parentPanelRectTransform.pivot = new Vector2(0, 1);
        parentPanelRectTransform.right = Vector3.zero;
        parentPanelRectTransform.sizeDelta = new Vector2(parentPanelRectTransform.sizeDelta.x, 170f); //4 ui elements, each 34 units tall
        parentPanelRectTransform.anchoredPosition = new Vector3(0, equationInputFieldBottom, 0);


        //==========

        GameObject titleLabel = Instantiate(textBoxPrefab, parentPanel.transform);
        titleLabel.transform.name = $"titleLabel.{fieldNum}";
        RectTransform titleLabelRectTransform = titleLabel.GetComponent<RectTransform>();
        titleLabelRectTransform.anchorMin = new Vector2(0, 1);
        titleLabelRectTransform.anchorMax = new Vector2(0, 1);
        titleLabelRectTransform.pivot = new Vector2(0, 1);
        titleLabelRectTransform.sizeDelta = new Vector2(1875, 34f);
        titleLabelRectTransform.anchoredPosition = new Vector3(0, 0, 0);
        TextMeshProUGUI titleLabelTMPUGUI = titleLabel.GetComponent<TextMeshProUGUI>();
        titleLabelTMPUGUI.text = "Find intersection/skew/parallel of two lines";
        titleLabelTMPUGUI.fontStyle = FontStyles.Bold;
        titleLabelTMPUGUI.color = Color.black;


        //========


        GameObject textBox1 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox1.transform.name = $"textBox1.{fieldNum}";
        RectTransform textBox1RectTransform = textBox1.GetComponent<RectTransform>();
        textBox1RectTransform.anchorMin = new Vector2(0, 1);
        textBox1RectTransform.anchorMax = new Vector2(0, 1);
        textBox1RectTransform.pivot = new Vector2(0, 1);
        textBox1RectTransform.sizeDelta = new Vector2(200, 34f);
        textBox1RectTransform.anchoredPosition = new Vector3(0, -34, 0);
        TextMeshProUGUI textBox1TMPUGUI = textBox1.GetComponent<TextMeshProUGUI>();
        textBox1TMPUGUI.text = "Line 1 = ";
        textBox1TMPUGUI.fontStyle = FontStyles.Normal;
        textBox1TMPUGUI.color = Color.black;




        GameObject inputField1 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField1RectTransform = inputField1.GetComponent<RectTransform>();
        inputField1.transform.name = $"InputField1.{fieldNum}";
        inputField1RectTransform.anchorMin = new Vector2(0, 1);
        inputField1RectTransform.anchorMax = new Vector2(0, 1);
        inputField1RectTransform.pivot = new Vector2(0, 1);
        inputField1RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField1RectTransform.anchoredPosition = new Vector3(200, -34, 0);     //position
        Transform inputField1Placeholder = inputField1.transform.Find("Placeholder");
        inputField1Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z) + λ(a, b, c)";


        //=====================


        GameObject textBox2 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox2.transform.name = $"textBox2.{fieldNum}";
        RectTransform textBox2RectTransform = textBox2.GetComponent<RectTransform>();
        textBox2RectTransform.anchorMin = new Vector2(0, 1);
        textBox2RectTransform.anchorMax = new Vector2(0, 1);
        textBox2RectTransform.pivot = new Vector2(0, 1);
        textBox2RectTransform.sizeDelta = new Vector2(200, 34f);
        textBox2RectTransform.anchoredPosition = new Vector3(0, -68, 0);
        TextMeshProUGUI textBox2TMPUGUI = textBox2.GetComponent<TextMeshProUGUI>();
        textBox2TMPUGUI.text = "Line 2 = ";
        textBox2TMPUGUI.fontStyle = FontStyles.Normal;
        textBox2TMPUGUI.color = Color.black;




        GameObject inputField2 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField2RectTransform = inputField2.GetComponent<RectTransform>();
        inputField2.transform.name = $"InputField2.{fieldNum}";
        inputField2RectTransform.anchorMin = new Vector2(0, 1);
        inputField2RectTransform.anchorMax = new Vector2(0, 1);
        inputField2RectTransform.pivot = new Vector2(0, 1);
        inputField2RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField2RectTransform.anchoredPosition = new Vector3(200, -68, 0);     //position
        Transform inputField2Placeholder = inputField2.transform.Find("Placeholder");
        inputField2Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z) + λ(a, b, c)";


        //====================


        GameObject textBox3 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox3.transform.name = $"textBox3.{fieldNum}";
        RectTransform textBox3RectTransform = textBox3.GetComponent<RectTransform>();
        textBox3RectTransform.anchorMin = new Vector2(0, 1);
        textBox3RectTransform.anchorMax = new Vector2(0, 1);
        textBox3RectTransform.pivot = new Vector2(0, 1);
        textBox3RectTransform.sizeDelta = new Vector2(1875, 34f);
        textBox3RectTransform.anchoredPosition = new Vector3(0, -102, 0);
        TextMeshProUGUI textBox3TMPUGUI = textBox3.GetComponent<TextMeshProUGUI>();
        textBox3TMPUGUI.text = "Result: ";
        textBox3TMPUGUI.fontStyle = FontStyles.Normal;
        textBox3TMPUGUI.color = Color.black;



        //====================


        GameObject button = Instantiate(buttonPrefab, parentPanel.transform);
        button.transform.name = $"button.{fieldNum}";
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        buttonRectTransform.anchorMin = new Vector2(0, 1);
        buttonRectTransform.anchorMax = new Vector2(0, 1);
        buttonRectTransform.pivot = new Vector2(0, 1);
        buttonRectTransform.sizeDelta = new Vector2(270, 34f);
        buttonRectTransform.anchoredPosition = new Vector3(0, -136, 0);
        button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Calculate";
        button.GetComponent<Button>().onClick.AddListener(delegate { computeFunctionFive(parentPanel); });

        //=============

        GameObject closeButton = Instantiate(closeButtonPrefab, parentPanel.transform);
        closeButton.transform.name = $"closeButton.{fieldNum}";
        RectTransform closeButtonRectTransform = closeButton.GetComponent<RectTransform>();
        closeButtonRectTransform.sizeDelta = new Vector2(45, 34f);
        closeButtonRectTransform.anchoredPosition = new Vector3(-45, 0, 0);
        closeButton.transform.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        //===============



        fieldNum = fieldNum + 1;

        equationInputFieldTop = equationInputFieldTop + 3 * 34;
        equationInputFieldBottom = equationInputFieldBottom - 3 * 34;
        moveAddFieldButton();

        fields.Add((parentPanel.transform.name), parentPanel);
    }
    public void menuFunctionSix()//this is function 6. Find closest point between two lines
    {
        GameObject parentPanel = Instantiate(panelPrefab, equationContent.transform);
        parentPanel.transform.name = "ParentPanel" + fieldNum;
        RectTransform parentPanelRectTransform = parentPanel.GetComponent<RectTransform>();
        parentPanelRectTransform.anchorMin = new Vector2(0, 1);
        parentPanelRectTransform.anchorMax = new Vector2(1, 1);
        parentPanelRectTransform.pivot = new Vector2(0, 1);
        parentPanelRectTransform.right = Vector3.zero;
        parentPanelRectTransform.sizeDelta = new Vector2(parentPanelRectTransform.sizeDelta.x, 170f); //4 ui elements, each 34 units tall
        parentPanelRectTransform.anchoredPosition = new Vector3(0, equationInputFieldBottom, 0);


        //==========
        GameObject titleLabel = Instantiate(textBoxPrefab, parentPanel.transform);
        titleLabel.transform.name = $"titleLabel.{fieldNum}";
        RectTransform titleLabelRectTransform = titleLabel.GetComponent<RectTransform>();
        titleLabelRectTransform.anchorMin = new Vector2(0, 1);
        titleLabelRectTransform.anchorMax = new Vector2(0, 1);
        titleLabelRectTransform.pivot = new Vector2(0, 1);
        titleLabelRectTransform.sizeDelta = new Vector2(1875, 34f);
        titleLabelRectTransform.anchoredPosition = new Vector3(0, 0, 0);
        TextMeshProUGUI titleLabelTMPUGUI = titleLabel.GetComponent<TextMeshProUGUI>();
        titleLabelTMPUGUI.text = "Find closest point between two lines";
        titleLabelTMPUGUI.fontStyle = FontStyles.Bold;
        titleLabelTMPUGUI.color = Color.black;



        //========


        GameObject textBox1 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox1.transform.name = $"textBox1.{fieldNum}";
        RectTransform textBox1RectTransform = textBox1.GetComponent<RectTransform>();
        textBox1RectTransform.anchorMin = new Vector2(0, 1);
        textBox1RectTransform.anchorMax = new Vector2(0, 1);
        textBox1RectTransform.pivot = new Vector2(0, 1);
        textBox1RectTransform.sizeDelta = new Vector2(150, 34f);
        textBox1RectTransform.anchoredPosition = new Vector3(0, -34, 0);
        TextMeshProUGUI textBox1TMPUGUI = textBox1.GetComponent<TextMeshProUGUI>();
        textBox1TMPUGUI.text = "Line 1 = ";
        textBox1TMPUGUI.fontStyle = FontStyles.Normal;
        textBox1TMPUGUI.color = Color.black;




        GameObject inputField1 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField1RectTransform = inputField1.GetComponent<RectTransform>();
        inputField1.transform.name = $"InputField1.{fieldNum}";
        inputField1RectTransform.anchorMin = new Vector2(0, 1);
        inputField1RectTransform.anchorMax = new Vector2(0, 1);
        inputField1RectTransform.pivot = new Vector2(0, 1);
        inputField1RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField1RectTransform.anchoredPosition = new Vector3(150, -34, 0);     //position
        Transform inputField1Placeholder = inputField1.transform.Find("Placeholder");
        inputField1Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z) + λ(a, b, c)";


        //=====================


        GameObject textBox2 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox2.transform.name = $"textBox2.{fieldNum}";
        RectTransform textBox2RectTransform = textBox2.GetComponent<RectTransform>();
        textBox2RectTransform.anchorMin = new Vector2(0, 1);
        textBox2RectTransform.anchorMax = new Vector2(0, 1);
        textBox2RectTransform.pivot = new Vector2(0, 1);
        textBox2RectTransform.sizeDelta = new Vector2(150, 34f);
        textBox2RectTransform.anchoredPosition = new Vector3(0, -68, 0);
        TextMeshProUGUI textBox2TMPUGUI = textBox2.GetComponent<TextMeshProUGUI>();
        textBox2TMPUGUI.text = "Line 2 = ";
        textBox2TMPUGUI.fontStyle = FontStyles.Normal;
        textBox2TMPUGUI.color = Color.black;




        GameObject inputField2 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField2RectTransform = inputField2.GetComponent<RectTransform>();
        inputField2.transform.name = $"InputField2.{fieldNum}";
        inputField2RectTransform.anchorMin = new Vector2(0, 1);
        inputField2RectTransform.anchorMax = new Vector2(0, 1);
        inputField2RectTransform.pivot = new Vector2(0, 1);
        inputField2RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField2RectTransform.anchoredPosition = new Vector3(150, -68, 0);     //position
        Transform inputField2Placeholder = inputField2.transform.Find("Placeholder");
        inputField2Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z) + λ(a, b, c)";


        //====================


        GameObject textBox3 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox3.transform.name = $"textBox3.{fieldNum}";
        RectTransform textBox3RectTransform = textBox3.GetComponent<RectTransform>();
        textBox3RectTransform.anchorMin = new Vector2(0, 1);
        textBox3RectTransform.anchorMax = new Vector2(0, 1);
        textBox3RectTransform.pivot = new Vector2(0, 1);
        textBox3RectTransform.sizeDelta = new Vector2(1875, 34f);
        textBox3RectTransform.anchoredPosition = new Vector3(0, -102, 0);
        TextMeshProUGUI textBox3TMPUGUI = textBox3.GetComponent<TextMeshProUGUI>();
        textBox3TMPUGUI.text = "Result: ";
        textBox3TMPUGUI.fontStyle = FontStyles.Normal;
        textBox3TMPUGUI.color = Color.black;



        //====================


        GameObject button = Instantiate(buttonPrefab, parentPanel.transform);
        button.transform.name = $"button.{fieldNum}";
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        buttonRectTransform.anchorMin = new Vector2(0, 1);
        buttonRectTransform.anchorMax = new Vector2(0, 1);
        buttonRectTransform.pivot = new Vector2(0, 1);
        buttonRectTransform.sizeDelta = new Vector2(270, 34f);
        buttonRectTransform.anchoredPosition = new Vector3(0, -136, 0);
        button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Calculate";
        button.GetComponent<Button>().onClick.AddListener(delegate { computeFunctionSix(parentPanel); });

        //=============

        GameObject closeButton = Instantiate(closeButtonPrefab, parentPanel.transform);
        closeButton.transform.name = $"closeButton.{fieldNum}";
        RectTransform closeButtonRectTransform = closeButton.GetComponent<RectTransform>();
        closeButtonRectTransform.sizeDelta = new Vector2(45, 34f);
        closeButtonRectTransform.anchoredPosition = new Vector3(-45, 0, 0);
        closeButton.transform.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        //===============



        fieldNum = fieldNum + 1;

        equationInputFieldTop = equationInputFieldTop + 3 * 34;
        equationInputFieldBottom = equationInputFieldBottom - 3 * 34;
        moveAddFieldButton();
        fields.Add((parentPanel.transform.name), parentPanel);
    }
    public void menuFunctionSeven()//this is function 7.Find unit vectors perpendicular to two vectors
    {
        //naming system - $"{objectname}.{fieldnum}"


        GameObject parentPanel = Instantiate(panelPrefab, equationContent.transform);
        parentPanel.transform.name = "ParentPanel" + fieldNum;
        RectTransform parentPanelRectTransform = parentPanel.GetComponent<RectTransform>();
        parentPanelRectTransform.anchorMin = new Vector2(0, 1);
        parentPanelRectTransform.anchorMax = new Vector2(1, 1);
        parentPanelRectTransform.pivot = new Vector2(0, 1);
        parentPanelRectTransform.right = Vector3.zero;
        parentPanelRectTransform.sizeDelta = new Vector2(parentPanelRectTransform.sizeDelta.x, 170f); //4 ui elements, each 34 units tall
        parentPanelRectTransform.anchoredPosition = new Vector3(0, equationInputFieldBottom, 0);

        //==========
        GameObject titleLabel = Instantiate(textBoxPrefab, parentPanel.transform);
        titleLabel.transform.name = $"titleLabel.{fieldNum}";
        RectTransform titleLabelRectTransform = titleLabel.GetComponent<RectTransform>();
        titleLabelRectTransform.anchorMin = new Vector2(0, 1);
        titleLabelRectTransform.anchorMax = new Vector2(0, 1);
        titleLabelRectTransform.pivot = new Vector2(0, 1);
        titleLabelRectTransform.sizeDelta = new Vector2(1875, 34f);
        titleLabelRectTransform.anchoredPosition = new Vector3(0, 0, 0);
        TextMeshProUGUI titleLabelTMPUGUI = titleLabel.GetComponent<TextMeshProUGUI>();
        titleLabelTMPUGUI.text = "Find unit vectors perpendicular to two vectors";
        titleLabelTMPUGUI.fontStyle = FontStyles.Bold;
        titleLabelTMPUGUI.color = Color.black;



        //========


        GameObject textBox1 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox1.transform.name = $"textBox1.{fieldNum}";
        RectTransform textBox1RectTransform = textBox1.GetComponent<RectTransform>();
        textBox1RectTransform.anchorMin = new Vector2(0, 1);
        textBox1RectTransform.anchorMax = new Vector2(0, 1);
        textBox1RectTransform.pivot = new Vector2(0, 1);
        textBox1RectTransform.sizeDelta = new Vector2(150, 34f);
        textBox1RectTransform.anchoredPosition = new Vector3(0, -34, 0);
        TextMeshProUGUI textBox1TMPUGUI = textBox1.GetComponent<TextMeshProUGUI>();
        textBox1TMPUGUI.text = "Vector 1 = ";
        textBox1TMPUGUI.fontStyle = FontStyles.Normal;
        textBox1TMPUGUI.color = Color.black;
        ;



        GameObject inputField1 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField1RectTransform = inputField1.GetComponent<RectTransform>();
        inputField1.transform.name = $"InputField1.{fieldNum}";
        inputField1RectTransform.anchorMin = new Vector2(0, 1);
        inputField1RectTransform.anchorMax = new Vector2(0, 1);
        inputField1RectTransform.pivot = new Vector2(0, 1);
        inputField1RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField1RectTransform.anchoredPosition = new Vector3(150, -34, 0);     //position
        Transform inputField1Placeholder = inputField1.transform.Find("Placeholder");
        inputField1Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z)";


        //=====================


        GameObject textBox2 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox2.transform.name = $"textBox2.{fieldNum}";
        RectTransform textBox2RectTransform = textBox2.GetComponent<RectTransform>();
        textBox2RectTransform.anchorMin = new Vector2(0, 1);
        textBox2RectTransform.anchorMax = new Vector2(0, 1);
        textBox2RectTransform.pivot = new Vector2(0, 1);
        textBox2RectTransform.sizeDelta = new Vector2(150, 34f);
        textBox2RectTransform.anchoredPosition = new Vector3(0, -68, 0);
        TextMeshProUGUI textBox2TMPUGUI = textBox2.GetComponent<TextMeshProUGUI>();
        textBox2TMPUGUI.text = "Vector 2 = ";
        textBox2TMPUGUI.fontStyle = FontStyles.Normal;
        textBox2TMPUGUI.color = Color.black;



        GameObject inputField2 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField2RectTransform = inputField2.GetComponent<RectTransform>();
        inputField2.transform.name = $"InputField2.{fieldNum}";
        inputField2RectTransform.anchorMin = new Vector2(0, 1);
        inputField2RectTransform.anchorMax = new Vector2(0, 1);
        inputField2RectTransform.pivot = new Vector2(0, 1);
        inputField2RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField2RectTransform.anchoredPosition = new Vector3(150, -68, 0);     //position
        Transform inputField2Placeholder = inputField2.transform.Find("Placeholder");
        inputField2Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z)";


        //====================


        GameObject textBox3 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox3.transform.name = $"textBox3.{fieldNum}";
        RectTransform textBox3RectTransform = textBox3.GetComponent<RectTransform>();
        textBox3RectTransform.anchorMin = new Vector2(0, 1);
        textBox3RectTransform.anchorMax = new Vector2(0, 1);
        textBox3RectTransform.pivot = new Vector2(0, 1);
        textBox3RectTransform.sizeDelta = new Vector2(1875, 34f);
        textBox3RectTransform.anchoredPosition = new Vector3(0, -102, 0);
        TextMeshProUGUI textBox3TMPUGUI = textBox3.GetComponent<TextMeshProUGUI>();
        textBox3TMPUGUI.text = "Result: ";
        textBox3TMPUGUI.fontStyle = FontStyles.Normal;
        textBox3TMPUGUI.color = Color.black;



        //====================


        GameObject button = Instantiate(buttonPrefab, parentPanel.transform);
        button.transform.name = $"button.{fieldNum}";
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        buttonRectTransform.anchorMin = new Vector2(0, 1);
        buttonRectTransform.anchorMax = new Vector2(0, 1);
        buttonRectTransform.pivot = new Vector2(0, 1);
        buttonRectTransform.sizeDelta = new Vector2(270, 34f);
        buttonRectTransform.anchoredPosition = new Vector3(0, -136, 0);
        button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Calculate";
        button.GetComponent<Button>().onClick.AddListener(delegate { computeFunctionSeven(parentPanel); });

        //=============

        GameObject closeButton = Instantiate(closeButtonPrefab, parentPanel.transform);
        closeButton.transform.name = $"closeButton.{fieldNum}";
        RectTransform closeButtonRectTransform = closeButton.GetComponent<RectTransform>();
        closeButtonRectTransform.sizeDelta = new Vector2(45, 34f);
        closeButtonRectTransform.anchoredPosition = new Vector3(-45, 0, 0);
        closeButton.transform.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        //===============



        fieldNum = fieldNum + 1;

        equationInputFieldTop = equationInputFieldTop + 3 * 34;
        equationInputFieldBottom = equationInputFieldBottom - 3 * 34;
        moveAddFieldButton();

        fields.Add((parentPanel.transform.name), parentPanel);

    }
    public void menuFunctionEight()//this is function 8. Find area of triangle bounded by 3 points
    {
        //naming system - $"{objectname}.{fieldnum}"


        GameObject parentPanel = Instantiate(panelPrefab, equationContent.transform);
        parentPanel.transform.name = "ParentPanel" + fieldNum;
        RectTransform parentPanelRectTransform = parentPanel.GetComponent<RectTransform>();
        parentPanelRectTransform.anchorMin = new Vector2(0, 1);
        parentPanelRectTransform.anchorMax = new Vector2(1, 1);
        parentPanelRectTransform.pivot = new Vector2(0, 1);
        parentPanelRectTransform.right = Vector3.zero;
        parentPanelRectTransform.sizeDelta = new Vector2(parentPanelRectTransform.sizeDelta.x, 204f); //5 ui elements, each 34 units tall
        parentPanelRectTransform.anchoredPosition = new Vector3(0, equationInputFieldBottom, 0);


        //==========
        GameObject titleLabel = Instantiate(textBoxPrefab, parentPanel.transform);
        titleLabel.transform.name = $"titleLabel.{fieldNum}";
        RectTransform titleLabelRectTransform = titleLabel.GetComponent<RectTransform>();
        titleLabelRectTransform.anchorMin = new Vector2(0, 1);
        titleLabelRectTransform.anchorMax = new Vector2(0, 1);
        titleLabelRectTransform.pivot = new Vector2(0, 1);
        titleLabelRectTransform.sizeDelta = new Vector2(1875, 34f);
        titleLabelRectTransform.anchoredPosition = new Vector3(0, 0, 0);
        TextMeshProUGUI titleLabelTMPUGUI = titleLabel.GetComponent<TextMeshProUGUI>();
        titleLabelTMPUGUI.text = "Find area of triangle bounded by 3 points";
        titleLabelTMPUGUI.fontStyle = FontStyles.Bold;
        titleLabelTMPUGUI.color = Color.black;



        //========


        GameObject textBox1 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox1.transform.name = $"textBox1.{fieldNum}";
        RectTransform textBox1RectTransform = textBox1.GetComponent<RectTransform>();
        textBox1RectTransform.anchorMin = new Vector2(0, 1);
        textBox1RectTransform.anchorMax = new Vector2(0, 1);
        textBox1RectTransform.pivot = new Vector2(0, 1);
        textBox1RectTransform.sizeDelta = new Vector2(150, 34f);
        textBox1RectTransform.anchoredPosition = new Vector3(0, -34, 0);
        TextMeshProUGUI textBox1TMPUGUI = textBox1.GetComponent<TextMeshProUGUI>();
        textBox1TMPUGUI.text = "Point 1 = ";
        textBox1TMPUGUI.fontStyle = FontStyles.Normal;
        textBox1TMPUGUI.color = Color.black;



        GameObject inputField1 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField1RectTransform = inputField1.GetComponent<RectTransform>();
        inputField1.transform.name = $"InputField1.{fieldNum}";
        inputField1RectTransform.anchorMin = new Vector2(0, 1);
        inputField1RectTransform.anchorMax = new Vector2(0, 1);
        inputField1RectTransform.pivot = new Vector2(0, 1);
        inputField1RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField1RectTransform.anchoredPosition = new Vector3(150, -34, 0);     //position
        Transform inputField1Placeholder = inputField1.transform.Find("Placeholder");
        inputField1Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z)";


        //=====================


        GameObject textBox2 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox2.transform.name = $"textBox2.{fieldNum}";
        RectTransform textBox2RectTransform = textBox2.GetComponent<RectTransform>();
        textBox2RectTransform.anchorMin = new Vector2(0, 1);
        textBox2RectTransform.anchorMax = new Vector2(0, 1);
        textBox2RectTransform.pivot = new Vector2(0, 1);
        textBox2RectTransform.sizeDelta = new Vector2(150, 34f);
        textBox2RectTransform.anchoredPosition = new Vector3(0, -68, 0);
        TextMeshProUGUI textBox2TMPUGUI = textBox2.GetComponent<TextMeshProUGUI>();
        textBox2TMPUGUI.text = "Point 2 = ";
        textBox2TMPUGUI.fontStyle = FontStyles.Normal;
        textBox2TMPUGUI.color = Color.black;




        GameObject inputField2 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField2RectTransform = inputField2.GetComponent<RectTransform>();
        inputField2.transform.name = $"InputField2.{fieldNum}";
        inputField2RectTransform.anchorMin = new Vector2(0, 1);
        inputField2RectTransform.anchorMax = new Vector2(0, 1);
        inputField2RectTransform.pivot = new Vector2(0, 1);
        inputField2RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField2RectTransform.anchoredPosition = new Vector3(150, -68, 0);     //position
        Transform inputField2Placeholder = inputField2.transform.Find("Placeholder");
        inputField2Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z)";


        //====================


        GameObject textBox3 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox3.transform.name = $"textBox3.{fieldNum}";
        RectTransform textBox3RectTransform = textBox3.GetComponent<RectTransform>();
        textBox3RectTransform.anchorMin = new Vector2(0, 1);
        textBox3RectTransform.anchorMax = new Vector2(0, 1);
        textBox3RectTransform.pivot = new Vector2(0, 1);
        textBox3RectTransform.sizeDelta = new Vector2(1875, 34f);
        textBox3RectTransform.anchoredPosition = new Vector3(0, -102, 0);
        TextMeshProUGUI textBox3TMPUGUI = textBox3.GetComponent<TextMeshProUGUI>();
        textBox3TMPUGUI.text = "Point 3 = ";
        textBox3TMPUGUI.fontStyle = FontStyles.Normal;
        textBox3TMPUGUI.color = Color.black;



        GameObject inputField3 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField3RectTransform = inputField3.GetComponent<RectTransform>();
        inputField3.transform.name = $"InputField3.{fieldNum}";
        inputField3RectTransform.anchorMin = new Vector2(0, 1);
        inputField3RectTransform.anchorMax = new Vector2(0, 1);
        inputField3RectTransform.pivot = new Vector2(0, 1);
        inputField3RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField3RectTransform.anchoredPosition = new Vector3(150, -102, 0);     //position
        Transform inputField3Placeholder = inputField3.transform.Find("Placeholder");
        inputField3Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z)";


        //============

        GameObject textBox4 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox4.transform.name = $"textBox4.{fieldNum}";
        RectTransform textBox4RectTransform = textBox4.GetComponent<RectTransform>();
        textBox4RectTransform.anchorMin = new Vector2(0, 1);
        textBox4RectTransform.anchorMax = new Vector2(0, 1);
        textBox4RectTransform.pivot = new Vector2(0, 1);
        textBox4RectTransform.sizeDelta = new Vector2(1875, 34f);
        textBox4RectTransform.anchoredPosition = new Vector3(0, -136, 0);
        TextMeshProUGUI textBox4TMPUGUI = textBox4.GetComponent<TextMeshProUGUI>();
        textBox4TMPUGUI.text = "Result: ";
        textBox4TMPUGUI.fontStyle = FontStyles.Normal;
        textBox4TMPUGUI.color = Color.black;


        //====================


        GameObject button = Instantiate(buttonPrefab, parentPanel.transform);
        button.transform.name = $"button.{fieldNum}";
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        buttonRectTransform.anchorMin = new Vector2(0, 1);
        buttonRectTransform.anchorMax = new Vector2(0, 1);
        buttonRectTransform.pivot = new Vector2(0, 1);
        buttonRectTransform.sizeDelta = new Vector2(270, 34f);
        buttonRectTransform.anchoredPosition = new Vector3(0, -170, 0);
        button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Calculate";
        button.GetComponent<Button>().onClick.AddListener(delegate { computeFunctionEight(parentPanel); });

        //=============

        GameObject closeButton = Instantiate(closeButtonPrefab, parentPanel.transform);
        closeButton.transform.name = $"closeButton.{fieldNum}";
        RectTransform closeButtonRectTransform = closeButton.GetComponent<RectTransform>();
        closeButtonRectTransform.sizeDelta = new Vector2(45, 34f);
        closeButtonRectTransform.anchoredPosition = new Vector3(-45, 0, 0);
        closeButton.transform.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        //===============



        fieldNum = fieldNum + 1;

        equationInputFieldTop = equationInputFieldTop + 4 * 34;
        equationInputFieldBottom = equationInputFieldBottom - 4 * 34;
        moveAddFieldButton();
        fields.Add((parentPanel.transform.name), parentPanel);
    }
    public void menuFunctionNine()//this is function 9. Find intersections of circle with line
    {
        GameObject parentPanel = Instantiate(panelPrefab, equationContent.transform);
        parentPanel.transform.name = "ParentPanel" + fieldNum;
        RectTransform parentPanelRectTransform = parentPanel.GetComponent<RectTransform>();
        parentPanelRectTransform.anchorMin = new Vector2(0, 1);
        parentPanelRectTransform.anchorMax = new Vector2(1, 1);
        parentPanelRectTransform.pivot = new Vector2(0, 1);
        parentPanelRectTransform.right = Vector3.zero;
        parentPanelRectTransform.sizeDelta = new Vector2(parentPanelRectTransform.sizeDelta.x, 170f); //4 ui elements, each 34 units tall
        parentPanelRectTransform.anchoredPosition = new Vector3(0, equationInputFieldBottom, 0);


        //==========
        GameObject titleLabel = Instantiate(textBoxPrefab, parentPanel.transform);
        titleLabel.transform.name = $"titleLabel.{fieldNum}";
        RectTransform titleLabelRectTransform = titleLabel.GetComponent<RectTransform>();
        titleLabelRectTransform.anchorMin = new Vector2(0, 1);
        titleLabelRectTransform.anchorMax = new Vector2(0, 1);
        titleLabelRectTransform.pivot = new Vector2(0, 1);
        titleLabelRectTransform.sizeDelta = new Vector2(1875, 34f);
        titleLabelRectTransform.anchoredPosition = new Vector3(0, 0, 0);
        TextMeshProUGUI titleLabelTMPUGUI = titleLabel.GetComponent<TextMeshProUGUI>();
        titleLabelTMPUGUI.text = "Find intersections of circle with line";
        titleLabelTMPUGUI.fontStyle = FontStyles.Bold;
        titleLabelTMPUGUI.color = Color.black;


        //========


        GameObject textBox1 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox1.transform.name = $"textBox1.{fieldNum}";
        RectTransform textBox1RectTransform = textBox1.GetComponent<RectTransform>();
        textBox1RectTransform.anchorMin = new Vector2(0, 1);
        textBox1RectTransform.anchorMax = new Vector2(0, 1);
        textBox1RectTransform.pivot = new Vector2(0, 1);
        textBox1RectTransform.sizeDelta = new Vector2(110, 34f);
        textBox1RectTransform.anchoredPosition = new Vector3(0, -34, 0);
        TextMeshProUGUI textBox1TMPUGUI = textBox1.GetComponent<TextMeshProUGUI>();
        textBox1TMPUGUI.text = "Line = ";
        textBox1TMPUGUI.fontStyle = FontStyles.Normal;
        textBox1TMPUGUI.color = Color.black;




        GameObject inputField1 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField1RectTransform = inputField1.GetComponent<RectTransform>();
        inputField1.transform.name = $"InputField1.{fieldNum}";
        inputField1RectTransform.anchorMin = new Vector2(0, 1);
        inputField1RectTransform.anchorMax = new Vector2(0, 1);
        inputField1RectTransform.pivot = new Vector2(0, 1);
        inputField1RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField1RectTransform.anchoredPosition = new Vector3(110, -34, 0);     //position
        Transform inputField1Placeholder = inputField1.transform.Find("Placeholder");
        inputField1Placeholder.GetComponent<TextMeshProUGUI>().text = "(x, y, z) + λ(a, b, c)";


        //=====================


        GameObject textBox2 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox2.transform.name = $"textBox2.{fieldNum}";
        RectTransform textBox2RectTransform = textBox2.GetComponent<RectTransform>();
        textBox2RectTransform.anchorMin = new Vector2(0, 1);
        textBox2RectTransform.anchorMax = new Vector2(0, 1);
        textBox2RectTransform.pivot = new Vector2(0, 1);
        textBox2RectTransform.sizeDelta = new Vector2(110, 34f);
        textBox2RectTransform.anchoredPosition = new Vector3(0, -68, 0);
        TextMeshProUGUI textBox2TMPUGUI = textBox2.GetComponent<TextMeshProUGUI>();
        textBox2TMPUGUI.text = "Circle : ";
        textBox2TMPUGUI.fontStyle = FontStyles.Normal;
        textBox2TMPUGUI.color = Color.black;



        GameObject inputField2 = Instantiate(inputFieldPrefab, parentPanel.transform);
        RectTransform inputField2RectTransform = inputField2.GetComponent<RectTransform>();
        inputField2.transform.name = $"InputField2.{fieldNum}";
        inputField2RectTransform.anchorMin = new Vector2(0, 1);
        inputField2RectTransform.anchorMax = new Vector2(0, 1);
        inputField2RectTransform.pivot = new Vector2(0, 1);
        inputField2RectTransform.sizeDelta = new Vector2(1875f, 34f);     //size
        inputField2RectTransform.anchoredPosition = new Vector3(110, -68, 0);     //position
        Transform inputField2Placeholder = inputField2.transform.Find("Placeholder");
        inputField2Placeholder.GetComponent<TextMeshProUGUI>().text = "(x-a)<sup>2</sup> + (y-b)<sup>2</sup> + (z-c)<sup>2</sup>  = r<sup>2</sup>";


        //====================


        GameObject textBox3 = Instantiate(textBoxPrefab, parentPanel.transform);
        textBox3.transform.name = $"textBox3.{fieldNum}";
        RectTransform textBox3RectTransform = textBox3.GetComponent<RectTransform>();
        textBox3RectTransform.anchorMin = new Vector2(0, 1);
        textBox3RectTransform.anchorMax = new Vector2(0, 1);
        textBox3RectTransform.pivot = new Vector2(0, 1);
        textBox3RectTransform.sizeDelta = new Vector2(1875, 34f);
        textBox3RectTransform.anchoredPosition = new Vector3(0, -102, 0);
        TextMeshProUGUI textBox3TMPUGUI = textBox3.GetComponent<TextMeshProUGUI>();
        textBox3TMPUGUI.text = "Result: ";
        textBox3TMPUGUI.fontStyle = FontStyles.Normal;
        textBox3TMPUGUI.color = Color.black;



        //====================


        GameObject button = Instantiate(buttonPrefab, parentPanel.transform);
        button.transform.name = $"button.{fieldNum}";
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        buttonRectTransform.anchorMin = new Vector2(0, 1);
        buttonRectTransform.anchorMax = new Vector2(0, 1);
        buttonRectTransform.pivot = new Vector2(0, 1);
        buttonRectTransform.sizeDelta = new Vector2(270, 34f);
        buttonRectTransform.anchoredPosition = new Vector3(0, -136, 0);
        button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Calculate";
        button.GetComponent<Button>().onClick.AddListener(delegate { computeFunctionNine(parentPanel); });

        //=============

        GameObject closeButton = Instantiate(closeButtonPrefab, parentPanel.transform);
        closeButton.transform.name = $"closeButton.{fieldNum}";
        RectTransform closeButtonRectTransform = closeButton.GetComponent<RectTransform>();
        closeButtonRectTransform.sizeDelta = new Vector2(45, 34f);
        closeButtonRectTransform.anchoredPosition = new Vector3(-45, 0, 0);
        closeButton.transform.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        //===============



        fieldNum = fieldNum + 1;

        equationInputFieldTop = equationInputFieldTop + 3 * 34;
        equationInputFieldBottom = equationInputFieldBottom - 3 * 34;
        moveAddFieldButton();
        fields.Add((parentPanel.transform.name), parentPanel);
    }


    List<string> splitVectorLine(string inputString)//this function also exists in MainEquationHandler, but it is hard to be modularised because it must access lots of data held in Mainequationhandler script.
    { //very simplified version of the vector line parser in MainEquationHandler
      //simply splits it into direction and position vector parts.




        inputString = inputString.Replace(" ", "");
        string tempString = inputString;

        string[] equationParts = inputString.Split("λ", StringSplitOptions.RemoveEmptyEntries); //separates the line into 3 parts- l(a) = (x,y,z) + λ(a,b,c)  is separated into l(a), (x,y,z), (a,b,c).
                                                                                                //[0] = name, [1] = position vector, [2] = direction vector.
                                                                                                //i.e. l(a) = (1,1,1) + λ(3,1,5)
                                                                                                //sometimes user might put the other way round, we need to swap [1] and [2] if this is the case.
                                                                                                //however first we have to check if they included a position vector at all.






        string positionVector = vectorHandlerScript.mainVectorParser(equationParts[0].Substring(0, equationParts[0].Length - 1));
        string directionVector = vectorHandlerScript.mainVectorParser(equationParts[1]);


        string[] positionVectorParts = positionVector.Replace("(", "").Replace(")", "").Split(",", StringSplitOptions.RemoveEmptyEntries);
        string[] directionVectorParts = directionVector.Replace("(", "").Replace(")", "").Split(",", StringSplitOptions.RemoveEmptyEntries);


        List<string> vectorPartsList = new List<string>(); //we need this list to return it out of the function.
        vectorPartsList.Add(positionVectorParts[0]);
        vectorPartsList.Add(positionVectorParts[1]);
        vectorPartsList.Add(positionVectorParts[2]);
        vectorPartsList.Add(directionVectorParts[0]);
        vectorPartsList.Add(directionVectorParts[1]);
        vectorPartsList.Add(directionVectorParts[2]);
        //0,1,2 = position vector, 3,4,5 = direction vecotr.
        return vectorPartsList;



    }

    public void computeFunctionOne(GameObject parentPanel)
    {
        string fieldNum = parentPanel.transform.name.Substring(parentPanel.transform.name.Length - 1, 1);

        string vector1 = mainEquationHandlerScript.replaceAllFunctionsAndConstants((parentPanel.transform.Find($"InputField1.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", ""));
        string vector2 = mainEquationHandlerScript.replaceAllFunctionsAndConstants((parentPanel.transform.Find($"InputField2.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", ""));


        vector1 = vectorHandlerScript.mainVectorParser(vector1);
        vector2 = vectorHandlerScript.mainVectorParser(vector2);




        //doubles are used in VectorHandler. 
        if (Math.Abs(Math.Round(decimal.Parse(vectorHandlerScript.mainVectorParser($"Dot[{vector1},{vector2}]/(Size({vector1})*Size{vector2})")), 9)) == 1)
        {
            parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = "Parallel";

        }
        else
        {
            parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = "Not parallel";
        }





    }
    public void computeFunctionTwo(GameObject parentPanel)
    {
        string fieldNum = parentPanel.transform.name.Substring(parentPanel.transform.name.Length - 1, 1);
        print("fieldNum = " + fieldNum);
        string vector1 = mainEquationHandlerScript.replaceAllFunctionsAndConstants(parentPanel.transform.Find($"InputField1.{fieldNum}").GetComponent<TMP_InputField>().text) ;
        string vector2 = mainEquationHandlerScript.replaceAllFunctionsAndConstants(parentPanel.transform.Find($"InputField2.{fieldNum}").GetComponent<TMP_InputField>().text) ;


        vector1 = vectorHandlerScript.mainVectorParser(vector1);
        vector2 = vectorHandlerScript.mainVectorParser(vector2);




        if (Math.Abs(Math.Round(decimal.Parse(vectorHandlerScript.mainVectorParser($"Dot[{vector1},{vector2}]/(Size({vector1})*Size{vector2})")), 9)) == 0)
        {
            parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = "Perpendicular";

        }
        else
        {
            parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = "Not perpendicular";
        }
    }
    public void computeFunctionThree(GameObject parentPanel)
    {
        string fieldNum = parentPanel.transform.name.Substring(parentPanel.transform.name.Length - 1, 1);
        string point = mainEquationHandlerScript.replaceAllFunctionsAndConstants((parentPanel.transform.Find($"InputField1.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", ""));
        string line = mainEquationHandlerScript.replaceAllFunctionsAndConstants((parentPanel.transform.Find($"InputField2.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", ""));

        print("point = " + point);

        point = vectorHandlerScript.mainVectorParser(point);
        line = line.Replace(" ", "");

        string[] pointParts = point.Replace("(", "").Replace(")", "").Split(",", StringSplitOptions.RemoveEmptyEntries);

        double pointX = float.Parse(pointParts[0]);
        double pointY = float.Parse(pointParts[1]);
        double pointZ = float.Parse(pointParts[2]);

        List<string> parts = splitVectorLine(line);

        double posVecX = float.Parse(parts[0]);
        double posVecY = float.Parse(parts[1]);
        double posVecZ = float.Parse(parts[2]);
        double dirVecX = float.Parse(parts[3]);
        double dirVecY = float.Parse(parts[4]);
        double dirVecZ = float.Parse(parts[5]);



        double lambda = (pointX - posVecX) / dirVecX;



        if (Math.Round(pointY, 9) == Math.Round(posVecY + lambda * dirVecY, 9) && Math.Round(pointZ, 9) == Math.Round(posVecZ + lambda * dirVecZ, 9))
        {
            //it lies on the line
            parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = "Lies on line.";
        }
        else
        {
            //it doesn't.


            parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = "Does not lie on line.";
        }









    }
    public void computeFunctionFour(GameObject parentPanel)
    {
        string fieldNum = parentPanel.transform.name.Substring(parentPanel.transform.name.Length - 1, 1);

        string vector1 = mainEquationHandlerScript.replaceAllFunctionsAndConstants((parentPanel.transform.Find($"InputField1.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", ""));
        string vector2 = mainEquationHandlerScript.replaceAllFunctionsAndConstants((parentPanel.transform.Find($"InputField2.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", ""));

        print(vector1);

        if (vector1.Contains("λ"))
        {
            
            vector1 = vector1.Substring(vector1.IndexOf("λ") + 1, vector1.Length - vector1.IndexOf("λ") - 1);
        }
        if (vector2.Contains("λ"))
        {
            vector2 = vector2.Substring(vector2.IndexOf("λ") + 1, vector2.Length - vector2.IndexOf("λ") -1 );
        }

        vector1 = vectorHandlerScript.mainVectorParser(vector1);
        vector2 = vectorHandlerScript.mainVectorParser(vector2);

        double thetaRadians = Math.Acos(float.Parse(vectorHandlerScript.mainVectorParser($"Dot[{vector1},{vector2}]/(Size({vector1})*Size{vector2})")));

        double thetaDegrees = thetaRadians * 180 / Math.PI;


        parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = $"Degrees = {Math.Round(thetaDegrees, 9)}, Radians = {Math.Round(thetaRadians, 9)}";




    }
    public void computeFunctionFive(GameObject parentPanel)
    {
        //finds the intersection of two lines, if they intersect.
        //if they parallel, displays 'parallel'. if skew, displays 'skew.

        //two lines
        //l1 = (a,b,c) + lambda(x,y,z)
        //l2 = (d,e,f) + mu(p,q,r)
        string fieldNum = parentPanel.transform.name.Substring(parentPanel.transform.name.Length - 1, 1);

        string line1 = mainEquationHandlerScript.replaceAllFunctionsAndConstants((parentPanel.transform.Find($"InputField1.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", ""));
        string line2 = mainEquationHandlerScript.replaceAllFunctionsAndConstants((parentPanel.transform.Find($"InputField2.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", ""));

        print(line1);


        List<string> line1Parts = splitVectorLine(line1);
        List<string> line2Parts = splitVectorLine(line2);

        string tempDirVector1 = $"({line1Parts[3]},{line1Parts[4]},{line1Parts[5]})";
        string tempDirVector2 = $"({line2Parts[3]},{line2Parts[4]},{line2Parts[5]})";


        if (Math.Abs(Math.Round(decimal.Parse(vectorHandlerScript.mainVectorParser($"Dot[{tempDirVector1},{tempDirVector2}]/(Size({tempDirVector1})*Size{tempDirVector2})")), 9)) == 1)
        {
            //then its parallel
            parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = "The two lines are parallel.";
            return;
        }



        float a = float.Parse(line1Parts[0]);
        float b = float.Parse(line1Parts[1]);
        float c = float.Parse(line1Parts[2]);
        float x = float.Parse(line1Parts[3]);
        float y = float.Parse(line1Parts[4]);
        float z = float.Parse(line1Parts[5]);
        float d = float.Parse(line2Parts[0]);
        float e = float.Parse(line2Parts[1]);
        float f = float.Parse(line2Parts[2]);
        float p = float.Parse(line2Parts[3]);
        float q = float.Parse(line2Parts[4]);
        float r = float.Parse(line2Parts[5]);


        //we get 3 simultaneous equations, we need to find the value for all of them,and if they are equal
        //if they are not equal the line is skew 
        print((q * z - r * y));
        float mu1 = (e * x - x * b - y * d + a * y) / (p * y - x * q);
        float mu2 = (f * y - c * y + b * z - e * z) / (q * z - r * y);
        float mu3 = (f * x - c * x + a * z - z * d) / (p * z - r * x);


        List<float> mus = new List<float>();

        if (float.IsNaN(mu1) == false)
        {
            mus.Add(mu1);
        }
        if (float.IsNaN(mu2) == false)
        {
            mus.Add(mu2);
        }
        if (float.IsNaN(mu3) == false)
        {
            mus.Add(mu3);
        }


        bool allMuEqual = true;
        foreach (float num in mus)
        {
            foreach (float num1 in mus)
            {
                if (num != num1)
                {
                    allMuEqual = false;
                }

            }
        }


        //if allMuEqual = true [exlucindg the NaN] then that means they intersect.
        print($"mu1 = {mu1}, mu2 = {mu2}, mu3 = {mu3}");
        bool skew = true;

        if (allMuEqual == true)
        {
            skew = false;

            string intersectionPoint = vectorHandlerScript.mainVectorParser($"  ({d},{e},{f}) + {mus[0]}*({p},{q} ,{r})   ");

            //then display the intersection point on the textbox

            parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = $"Intersection point is at {intersectionPoint}";

        }
        else
        {
            //display 'skew'.
            parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = "The two lines are skew.";
        }



    }
    public void computeFunctionSix(GameObject parentPanel)
    {
        //finds the closest point between two lines



        //first run a function to check if they intersect. if they do, display "intersect, mininmum distance =0 "
        //also check if they parallel

        //l1 = (a,b,c) + lambda(x,y,z)
        //l2 = (d,e,f) + mu(p,q,r)
        string fieldNum = parentPanel.transform.name.Substring(parentPanel.transform.name.Length - 1, 1);
        string line1 = mainEquationHandlerScript.replaceAllFunctionsAndConstants((parentPanel.transform.Find($"InputField1.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", ""));
        string line2 = mainEquationHandlerScript.replaceAllFunctionsAndConstants((parentPanel.transform.Find($"InputField2.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", ""));


        List<string> line1Parts = splitVectorLine(line1);
        List<string> line2Parts = splitVectorLine(line2);



        string tempDirVector1 = $"({line1Parts[3]},{line1Parts[4]},{line1Parts[5]})";
        string tempDirVector2 = $"({line2Parts[3]},{line2Parts[4]},{line2Parts[5]})";





        float a = float.Parse(line1Parts[0]);
        float b = float.Parse(line1Parts[1]);
        float c = float.Parse(line1Parts[2]);
        float x = float.Parse(line1Parts[3]);
        float y = float.Parse(line1Parts[4]);
        float z = float.Parse(line1Parts[5]);
        float d = float.Parse(line2Parts[0]);
        float e = float.Parse(line2Parts[1]);
        float f = float.Parse(line2Parts[2]);
        float p = float.Parse(line2Parts[3]);
        float q = float.Parse(line2Parts[4]);
        float r = float.Parse(line2Parts[5]);





        if (Math.Abs(Math.Round(decimal.Parse(vectorHandlerScript.mainVectorParser($"Dot[{tempDirVector1},{tempDirVector2}]/(Size({tempDirVector1})*Size{tempDirVector2})")), 9)) == 1)
        {
            //then its parallel
            string stringToParse = $" Size(    ({d}   - {a} ,{e}-{b},{f}-{c} ) - Proj[({d}   - {a} ,{e}-{b},{f}-{c} )  on ( {x},{y},{z})]) ".Replace(" ", "");
            print(stringToParse);
            float shortestDistance = float.Parse(vectorHandlerScript.mainVectorParser(stringToParse));



            parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = $"The two lines are parallel. Shortest distance = {shortestDistance}.";
            return;
        }




        float alpha = p * (d - a) + q * (e - b) + r * (f - c);
        float beta = x * (d - a) + y * (e - b) + z * (f - c);
        float gamma = p * x + q * y + r * z;
        float delta = x * x + y * y + z * z;
        float epsilon = p * p + q * q + r * r;

        float mu = (alpha * delta - beta * gamma) / (gamma * gamma - epsilon * delta);
        float lambda = (beta + mu * gamma) / delta;



        string pointOnL1 = vectorHandlerScript.mainVectorParser($"  ({a},{b},{c}) + {lambda}*({x},{y} ,{z})   ");
        string pointOnL2 = vectorHandlerScript.mainVectorParser($"  ({d},{e},{f}) + {mu}*({p},{q} ,{r})   ");

        string distance = vectorHandlerScript.mainVectorParser($"Size({pointOnL1} - {pointOnL2})");

        parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = $"Shortest distance = {distance}, between {pointOnL1} on l1 and {pointOnL2} on l2.";
    }
    public void computeFunctionSeven(GameObject parentPanel)
    {
        string fieldNum = parentPanel.transform.name.Substring(parentPanel.transform.name.Length - 1, 1);
        string vector1 = mainEquationHandlerScript.replaceAllFunctionsAndConstants((parentPanel.transform.Find($"InputField1.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", ""));
        string vector2 = mainEquationHandlerScript.replaceAllFunctionsAndConstants((parentPanel.transform.Find($"InputField2.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", ""));

        


        vector1 = vectorHandlerScript.mainVectorParser(vector1);
        vector2 = vectorHandlerScript.mainVectorParser(vector2);

        string perpVector1 = vectorHandlerScript.mainVectorParser($"(1/Size(Cross[{vector1},{vector2}])) * Cross[{vector1},{vector2}]".Replace(" ",""));
        string perpVector2 = vectorHandlerScript.mainVectorParser($"-(1/Size(Cross[{vector1},{vector2}])) * Cross[{vector1},{vector2}]".Replace(" ", ""));

        parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = $" = {perpVector1} and {perpVector2}";
    }
    public void computeFunctionEight(GameObject parentPanel)
    {
        string fieldNum = parentPanel.transform.name.Substring(parentPanel.transform.name.Length - 1, 1);
        string pointA = (parentPanel.transform.Find($"InputField1.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", "");
        string pointB = (parentPanel.transform.Find($"InputField2.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", "");
        string pointC = (parentPanel.transform.Find($"InputField3.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", "");
        pointA = pointA.Replace(" ", "");
        pointB = pointB.Replace(" ", "");
        pointC = pointC.Replace(" ", "");

        pointA = vectorHandlerScript.mainVectorParser(pointA);
        pointB = vectorHandlerScript.mainVectorParser(pointB);
        pointC = vectorHandlerScript.mainVectorParser(pointC);

        //we have 3 points, so we have two vectors that go from one point to another, and that same one point to the other one.
        //we can then calculate the area.
        string vector1 = vectorHandlerScript.mainVectorParser($"{pointB} - {pointA}");
        string vector2 = vectorHandlerScript.mainVectorParser($"{pointC} - {pointA}");

        double thetaRadians = Math.Acos(float.Parse(vectorHandlerScript.mainVectorParser($"Dot[{vector1},{vector2}]/(Size({vector1})*Size{vector2})")));
        //0.5absin(c) = area
        double area = 0.5f * float.Parse(vectorHandlerScript.mainVectorParser($"Size({vector1})*Size({vector2})")) * Math.Sin(thetaRadians);

        area = Math.Round(area, 9);

        parentPanel.transform.Find($"textBox4.{fieldNum}").GetComponent<TextMeshProUGUI>().text = $" = {area}u<sup>2<sup>";
    }
    void computeFunctionNine(GameObject parentPanel)
    {
        //find intersections of a circle with a line.
        //(x,y,z) = (d,e,f) + lambda(p,q,r)
        //circle : (x-a)^2 + (y-b)^2 + (z-c)^2 = R^2


        //(x,y,z) = (d,e,f) + lambda(p,q,r)
        //(x-a)^2 + (y-b)^2 + (z-c)^2 = R^2
        //get these from the inputpanel

        string fieldNum = parentPanel.transform.name.Substring(parentPanel.transform.name.Length - 1, 1).Replace(" ", "");
        string line =   mainEquationHandlerScript.replaceAllFunctionsAndConstants(   (parentPanel.transform.Find($"InputField1.{fieldNum}").GetComponent<TMP_InputField>().text));
        line = line.Replace(" ", "");


        

        string[] linePartsArr = line.Split("λ");
        

        if (linePartsArr[0].Replace(" ","") == "")
        {
            linePartsArr = new string[2] { "(0,0,0)", linePartsArr[1] };

        }

        
        linePartsArr[0] = linePartsArr[0].Substring(0, linePartsArr[0].Length - 1); 

        linePartsArr[0] = vectorHandlerScript.mainVectorParser(linePartsArr[0]);
        linePartsArr[1] = vectorHandlerScript.mainVectorParser(linePartsArr[1]);

        line = $"{linePartsArr[0]} + λ{linePartsArr[1]}".Replace(" ", "");
        

        print("LINE====" + line);
        string sphere = (parentPanel.transform.Find($"InputField2.{fieldNum}").GetComponent<TMP_InputField>().text).Replace(" ", "");

        List<string> lineParts = splitVectorLine(line);


        float d = float.Parse(lineParts[0]);
        float e = float.Parse(lineParts[1]);
        float f = float.Parse(lineParts[2]);
        float p = float.Parse(lineParts[3]);
        float q = float.Parse(lineParts[4]);
        float r = float.Parse(lineParts[5]);

        List<float> sphereParts = cartesianSphereHandlerScript.parseSphere(sphere);
        float a = sphereParts[0];
        float b = sphereParts[1];
        float c = sphereParts[2];
        float R = sphereParts[3];

        float ayy = p * p + q * q + r * r;
        float bee = 2 * p * (d - a) + 2 * q * (e - b) + 2 * r * (f - c);
        float cee = (d - a) * (d - a) + (e - b) * (e - b) + (f - c) * (f - c) - R * R; //R = radius

        float discriminant = bee * bee - 4 * ayy * cee;
        if (discriminant < 0)
        {
            //0 intesrction
            parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = $"No point of intersection.";
            return;

        }



        float lambda1 = (-bee + Mathf.Sqrt(bee * bee - 4 * ayy * cee)) / (2 * ayy);
        float lambda2 = (-bee - Mathf.Sqrt(bee * bee - 4 * ayy * cee))/ (2 * ayy);

        string point1 = vectorHandlerScript.mainVectorParser($"  ({d},{e},{f}) + {lambda1}*({p},{q} ,{r})   ".Replace(" ", ""));
        string point2 = vectorHandlerScript.mainVectorParser($"  ({d},{e},{f}) + {lambda2}*({p},{q} ,{r})   ".Replace(" ", ""));

        if (discriminant == 0)
        {
            parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = $"Singular point of intersection at {point1}.";
        }
        else
        {
            parentPanel.transform.Find($"textBox3.{fieldNum}").GetComponent<TextMeshProUGUI>().text = $"Points of intersection at {point1} and {point2}.";


        }






    }


}

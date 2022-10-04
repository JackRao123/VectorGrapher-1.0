using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEditor;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Diagnostics;
public class OpenCloseFunctionInterface : MonoBehaviour, IPointerClickHandler
{
    AddInputField addInputFieldScript;

   



    GameObject functionInterface;
    public GameObject Canvas;



    bool plusButtonClicked = false; // whether or not the user is clicking the "+" button.
    private void Start()
    {

        addInputFieldScript = GameObject.FindGameObjectWithTag("NewFieldButton").GetComponent<AddInputField>();


        functionInterface = GameObject.FindGameObjectWithTag("FunctionInterface");
        functionInterface.active = false;






    }
    // Start is called before the first frame update
    private void Update()
    {

        if ((Input.GetMouseButton(0) == true || Input.GetMouseButton(1) == true) && functionInterface.active == true && plusButtonClicked == false)
        {
            float leftBoundary = functionInterface.transform.position.x;
            float rightBoundary = functionInterface.transform.position.x + Screen.width * (functionInterface.GetComponent<RectTransform>().rect.width / Canvas.GetComponent<RectTransform>().rect.width);
            float topBoundary = functionInterface.transform.position.y;
            float bottomBoundary = functionInterface.transform.position.y - Screen.height * (functionInterface.GetComponent<RectTransform>().rect.height / Canvas.GetComponent<RectTransform>().rect.height);

            Vector2 mousePos = Input.mousePosition;

            if (mousePos.x < leftBoundary || mousePos.x > rightBoundary || mousePos.y > topBoundary || mousePos.y < bottomBoundary)
            {
                functionInterface.active = false; //makes it dissapear if you click away from it.
            }
            //after this, the user can select one of the options in the menu.
            //the idividual subroutines for these will be in this script. 
            //this script will be attachecd to each of the buttons.
            //they will be run from each of the buttons.
            //once they are selected. functionInterface.active = false; will be run from each of those subroutines, to close the menu.
        }

        /*
        if(Input.GetMouseButton(1) == true)
        {
            print($"" +
         $"Canvas localscale  = {Canvas.transform.localScale} \n" +
         $"Canvas width and height = {Canvas.GetComponent<RectTransform>().rect.width   }, {Canvas.GetComponent<RectTransform>().rect.height}\n" +
         $"Canvas transform position = {Canvas.GetComponent<RectTransform>().rect.x}, {Canvas.GetComponent<RectTransform>().rect.y}\n" +

         $"scrollview localscale  = {scrollview.transform.localScale}\n" +
         $"scrollview width and height = {scrollview.GetComponent<RectTransform>().rect.width   }, {scrollview.GetComponent<RectTransform>().rect.height}\n" +
         $"scrollview transform position = {scrollview.GetComponent<RectTransform>().rect.x}, {scrollview.GetComponent<RectTransform>().rect.y}\n" +

         $"viewPort localscale  = {viewPort.transform.localScale}\n" +
         $"viewPort width and height = {viewPort.GetComponent<RectTransform>().rect.width   }, {viewPort.GetComponent<RectTransform>().rect.height}\n" +
         $"viewPort transform position = {viewPort.GetComponent<RectTransform>().rect.x}, {viewPort.GetComponent<RectTransform>().rect.y}\n" +

         $"functionInterface localscale  = {functionInterface.transform.localScale}\n" +
         $"functionInterface width and height = {functionInterface.GetComponent<RectTransform>().rect.width   }, {functionInterface.GetComponent<RectTransform>().rect.height}\n" +
         $"functionInterface rect transform position = {functionInterface.GetComponent<RectTransform>().rect.x}, {functionInterface.GetComponent<RectTransform>().rect.y}\n" +
         $"functionInterface transform position = {functionInterface.transform.position.x}, {functionInterface.transform.position.y}\n" +

         $"Screen width, height = {Screen.width}, {Screen.height}\n" +

         $"Cursor x, y = {Input.mousePosition.x}, {Input.mousePosition.y}\n" +

         $"");

        }
        */

        //NOTES 
        //Canvas width and height will be the resolution. in this case, 1080p.
        //anything.GetComponent<RectTransform>().rect.width and .height is the height in pixels that it would have IF THE SCREEN IS 1080x1920 PIXELS. To find the 
        //RATIO of width or height of component, to the width or height of the screen, use uielement.GetComponent<RectTransform>().rect.width/Canvas.GetComponent<RectTransform>().rect.width
        //Coordinates of cursor, and width/height of screen will depend on the size of the window. for example, in unity editor it isnt 1080x1920, its like 800 by 400 something.
        //the above must be considered when positioning elements in both global/screen space and respective to a parent object.
        //for cursor, (0,0) is the bottom left corner.



    }


    public void OnPointerClick(PointerEventData eventData) // runs when left/right click the + button.
    {

        plusButtonClicked = true;
        if (eventData.button == PointerEventData.InputButton.Left) //if it was left clicked
        {

            //print("Left clicked;");

            addInputFieldScript.createNewField();

        }

        if (eventData.button == PointerEventData.InputButton.Right) //if it was right clicked
        {

            //print("Right clicked;");
            openFunctionSelectionInterface();


            //here we open the menu for the user to select function


        }


        plusButtonClicked = false;

    }


    void openFunctionSelectionInterface() // opens an interface that allows the user to select what function they want ( this subroutine runs only when they right click.
    {

        functionInterface.active = true;
        functionInterface.transform.position = Input.mousePosition;





    }


}

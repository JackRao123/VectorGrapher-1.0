using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEditor;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Diagnostics;



public class FileButton : MonoBehaviour 
{

    public GameObject fileButton;
    public GameObject fileButtonInterfaces;
    public GameObject Canvas;


    bool interfaceOpen = false; // whether or not the interface is open

    private void Start()
    {
        fileButtonInterfaces.active = false;
    }
    public void openInterface()  
    {

         
        interfaceOpen = true;
        fileButtonInterfaces.active = true;
      
    }

    private void Update()
    {
        if(interfaceOpen = true && Input.GetMouseButton(0) == true)
        {
             
            Vector2 mousePos = Input.mousePosition;

            float leftBoundary = fileButtonInterfaces.transform.position.x;
            float rightBoundary = fileButtonInterfaces.transform.position.x + Screen.width * (fileButtonInterfaces.GetComponent<RectTransform>().rect.width / Canvas.GetComponent<RectTransform>().rect.width);
            float topBoundary = fileButtonInterfaces.transform.position.y;
            float bottomBoundary = fileButtonInterfaces.transform.position.y - Screen.height * (fileButtonInterfaces.GetComponent<RectTransform>().rect.height / Canvas.GetComponent<RectTransform>().rect.height);


            if (mousePos.x < leftBoundary || mousePos.x > rightBoundary || mousePos.y > topBoundary || mousePos.y < bottomBoundary)
            {
                fileButtonInterfaces.active = false; //makes it dissapear if you click away from it.
                interfaceOpen = false;
            }
        }
    }
}

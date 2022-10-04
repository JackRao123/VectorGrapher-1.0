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


public class DeleteInputField : MonoBehaviour
{
    AddInputField addInputFieldScript;

    GameObject addInputFieldButton;
    MainEquationHandler mainEquationHandlerScript;
    private void Start()
    {
        addInputFieldScript = GameObject.FindGameObjectWithTag("NewFieldButton").GetComponent<AddInputField>();
        //setting the script to the one attached to the first input field
        //doesn't really matter because the variable we want is static across all instances of AddInputField.cs

        addInputFieldButton = GameObject.FindGameObjectWithTag("NewFieldButton");
        mainEquationHandlerScript = GameObject.FindGameObjectWithTag("InputFieldTag1").GetComponent<MainEquationHandler>();




    }

    public void deleteInputField()
    {
        Transform parentPanel = transform.parent;
        string parentPanelName = parentPanel.name;
        float parentPanelHeight = parentPanel.gameObject.GetComponent<RectTransform>().rect.height;


        Dictionary<string, GameObject> localFields = AddInputField.fields;



        bool passedParentPanel = false;
        foreach(string item in localFields.Keys)
        {
            if(localFields[item].GetComponent<RectTransform>().anchoredPosition.y    < parentPanel.GetComponent<RectTransform>().anchoredPosition.y )
            {
                localFields[item].GetComponent<RectTransform>().anchoredPosition = localFields[item].GetComponent<RectTransform>().anchoredPosition + new Vector2(0,parentPanelHeight);   
            }
        }

        addInputFieldButton.GetComponent<RectTransform>().anchoredPosition = addInputFieldButton.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, parentPanelHeight);
        DestroyImmediate(parentPanel.gameObject, true); // gets rid of the parent panel.
        localFields.Remove(parentPanelName);

        
        

 



        AddInputField.fields = localFields;


        AddInputField.equationInputFieldTop = AddInputField.equationInputFieldTop - parentPanelHeight;
        AddInputField.equationInputFieldBottom = AddInputField.equationInputFieldBottom + parentPanelHeight;

        if (parentPanelName.Contains("inputField"))
        {
            mainEquationHandlerScript.removeEquationAssociatedWithDeletedField(parentPanelName);

        }
    }




}

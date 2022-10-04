using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;


public class toolTipScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject toolTipPopUpPrefab;

    GameObject toolTipPopUp;


    
    
    void Start()
    {
        // I added this in case I forgot to set the tooltip object

        toolTipPopUpPrefab = GameObject.FindGameObjectWithTag("ttpfb");


          toolTipPopUp = Instantiate(toolTipPopUpPrefab, transform);



        if (toolTipPopUp != null)
        {
            toolTipPopUp.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("TOOLTIP ON");
        // Same here
        if (toolTipPopUp != null)
        {

            transform.parent.transform.SetAsLastSibling();
            transform.SetAsLastSibling();


            toolTipPopUp.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("TOOLTIP OFF");
        // and same here
        if (toolTipPopUp != null)
        {
            toolTipPopUp.SetActive(false);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;


public class hidePlaneYZ : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject YZPlane;


    public Sprite hiddenIcon;
    public Sprite shownIcon;


    void Start()
    {


        /*
        GameObject XZPlane = GameObject.Find("yzPlane");




        XZPlane.SetActive(false);
        gameObject.GetComponent<Image>().sprite = hiddenIcon;
        */

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void hideYZPlane()
    {

#pragma warning disable CS0618 // Type or member is obsolete
        YZPlane.SetActive(!YZPlane.active);
#pragma warning restore CS0618 // Type or member is obsolete



        if (YZPlane.active)
        {
            gameObject.GetComponent<Image>().sprite = shownIcon;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = hiddenIcon;
        }



    }

}





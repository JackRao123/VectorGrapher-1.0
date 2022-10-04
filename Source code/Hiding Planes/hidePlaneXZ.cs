using UnityEngine;
using UnityEngine.UI;


public class hidePlaneXZ : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject XZPlane;

    public Sprite hiddenIcon;
    public Sprite shownIcon;

    void Start()
    {



 






    }

    // Update is called once per frame
    void Update()
    {

    }

    public void hideXZPlane()
    {


        //XZPlane.GetComponent<Renderer>().enabled = !(XZPlane.GetComponent<Renderer>().enabled);


        //we cannot use setactive(false), because that makes the thing DISSAPEAR, and thus the rigidbody disapears and our scroll function doesnt work anymore. 
        //so we need to use renderer=off.



        //XZPlane.GetComponent<Renderer>().enabled = false;


        XZPlane.SetActive(!XZPlane.active);
        //however, setting it inactive causes all the children to be inactive as well, which is what we want.
        //will need a bigger grid, that stays invisible


        if (XZPlane.active)
        {
            gameObject.GetComponent<Image>().sprite = shownIcon;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = hiddenIcon;
        }


    }

}





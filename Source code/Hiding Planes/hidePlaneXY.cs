using UnityEngine;
using UnityEngine.UI;


public class hidePlaneXY : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject XYPlane;
    public Sprite hiddenIcon;
    public Sprite shownIcon;
    void Start()
    {





 



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void hideXYPlane()
    {

        XYPlane.SetActive(!XYPlane.active);

        if (XYPlane.active)
        {
            gameObject.GetComponent<Image>().sprite = shownIcon;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = hiddenIcon;
        }

    }

}





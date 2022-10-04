using UnityEngine;
using UnityEngine.UI;

public class adjustVectorThickness : MonoBehaviour
{

    public GameObject slider;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void adjustVectorThicknessFunction()
    {
        float sliderValue = slider.GetComponent<Slider>().value;
        print(sliderValue);

    }
}

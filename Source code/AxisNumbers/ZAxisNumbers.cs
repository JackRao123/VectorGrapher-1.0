using System;
using System.Collections.Generic;
using UnityEngine;
public class ZAxisNumbers : MonoBehaviour
{
    static List<GameObject> axisNumbers = new List<GameObject>();
    static List<GameObject> axisDots = new List<GameObject>();
    bool axisNumbersAndDotsCreated = false;

    public Material axisDotMaterial;
    private void Start()
    {
        spawnAxisNumbersAndDotsInRange();
        adjustAxisNumbersToFaceCamera();
  

    }

    public void spawnAxisNumbersAndDotsInRange()
    {
        float increment = 1;
        float lowerRange;
        float upperRange;
        float lowerRangeNumber;
        float upperRangeNumber;

        float renderAngle = 0.1974f;
        float renderDistance = Mathf.Sqrt(Camera.main.transform.position.x * Camera.main.transform.position.x + Camera.main.transform.position.z * Camera.main.transform.position.z) / Mathf.Tan(renderAngle);
        lowerRange = Camera.main.transform.position.y - renderDistance;
        upperRange = Camera.main.transform.position.y + renderDistance;

        float cameraDistFromYAxis = Mathf.Sqrt(Camera.main.transform.position.x * Camera.main.transform.position.x + Camera.main.transform.position.z * Camera.main.transform.position.z);

        int decimalPlaces = 0;

        switch (cameraDistFromYAxis)
        {
            case < 0.15f:
                increment = 0.0001f;
                decimalPlaces = 4;
                break;

            case < 1.5f:
                increment = 0.001f;
                decimalPlaces = 3;
                break;

            case < 15f:
                increment = 0.01f;
                decimalPlaces = 2;
                break;

            case < 150f:
                increment = 0.1f;
                decimalPlaces = 1;
                break;

            case < 1500:
                increment = 1;
                decimalPlaces = 0;
                break;

            case < 3000:
                increment = 2;
                decimalPlaces = 0;
                break;

            case < 7500:
                increment = 5;
                decimalPlaces = 0;
                break;
            case < 15000:
                increment = 10;
                decimalPlaces = 0;
                break;
            case < 30000:
                increment = 20;
                decimalPlaces = 0;
                break;

            case < 75000:
                increment = 50;
                decimalPlaces = 0;
                break;

            case >= 75000:
                increment = 100;
                decimalPlaces = 0;
                break;
        }

        if (lowerRange < 0)
        {
            lowerRangeNumber = -Mathf.Ceil(-lowerRange / 100 / increment) * increment;
        }
        else
        {
            lowerRangeNumber = Mathf.Ceil(lowerRange / 100 / increment) * increment;
        }

        if (upperRange < 0)
        {
            upperRangeNumber = -Mathf.Ceil(-upperRange / 100 / increment) * increment;
        }
        else
        {
            upperRangeNumber = Mathf.Ceil(upperRange / 100 / increment) * increment;
        }

        List<GameObject> newAxisNumbers = new List<GameObject>();
        List<GameObject> newAxisDots = new List<GameObject>();

        if (lowerRangeNumber < -1000)
        {
            lowerRangeNumber = -1000;
        }
        if (upperRangeNumber > 1000)
        {
            upperRangeNumber = 1000;
        }
       // print($"Increment = {increment }\n, LowerRange = {lowerRange}\n, UpperRange = {upperRange}\n, LowerRangeNumber = {lowerRangeNumber}\n, UpperRangeNumber = {upperRangeNumber}");
        for (float i = lowerRangeNumber; i <= upperRangeNumber; i = i + increment)
        {
            float displayNumber = (float)Math.Round(i, decimalPlaces);
            newAxisNumbers.Add(createAxisNumber((displayNumber).ToString(), new Vector3(50 * increment, i * 100, 0)));
            newAxisDots.Add(createAxisDot(new Vector3(0, i * 100, 0)));

        }

        foreach (GameObject item in axisNumbers)
        {
            DestroyImmediate(item, true);
        }

        foreach (GameObject item in axisDots)
        {

            DestroyImmediate(item, true);

        }

        axisNumbers = newAxisNumbers;
        axisDots = newAxisDots;
    }




    public void adjustAxisNumbersToFaceCamera() //adjusts the axis numbers to face the camera
    {
        if (axisNumbersAndDotsCreated == false)
        {
            spawnAxisNumbersAndDotsInRange();
            axisNumbersAndDotsCreated = true;
        }
        else
        {
            float newScale = 0.001f * Mathf.Sqrt(Camera.main.transform.position.x * Camera.main.transform.position.x + Camera.main.transform.position.z * Camera.main.transform.position.z);
            float cameraZ = Camera.main.transform.position.y;

            foreach (GameObject item in axisNumbers)
            {
                item.GetComponent<TextMesh>().transform.LookAt(Camera.main.transform.position);
                item.transform.localScale = new Vector3(-newScale, newScale, newScale);
            }

        }
    }
    public void adjustAxisDotSize()
    {
        if (axisNumbersAndDotsCreated == false)
        {
            spawnAxisNumbersAndDotsInRange();
            axisNumbersAndDotsCreated = true;
        }
        else
        {
            float newScale = 0.01010152f * Mathf.Sqrt(Camera.main.transform.position.x * Camera.main.transform.position.x + Camera.main.transform.position.z * Camera.main.transform.position.z); //localscale factora lready applied in calculation
            foreach (GameObject item in axisDots)
            {
                item.transform.localScale = new Vector3(-newScale, newScale * 0.1f, newScale);
            }
        }
    }


    GameObject createAxisNumber(string axisNumberText, Vector3 position)
    {
        GameObject axisNumber = new GameObject();

        axisNumber.AddComponent<TextMesh>();

        var textMesh = axisNumber.GetComponent<TextMesh>();

        textMesh.text = axisNumberText;
        textMesh.color = Color.black;
        textMesh.fontSize = 300;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;

        axisNumber.transform.position = position;
        float scale = 0.3f;
        axisNumber.transform.localScale = new Vector3(-scale, scale, scale);

        axisNumber.transform.LookAt(new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z));

        return axisNumber;
    }

    GameObject createAxisDot(Vector3 position)
    {
        GameObject axisDot = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        axisDot.GetComponent<MeshRenderer>().material = axisDotMaterial;
        axisDot.transform.localScale = new Vector3(1, 0.1f, 1) * (Mathf.Sqrt(Camera.main.transform.position.x * Camera.main.transform.position.x + Camera.main.transform.position.z * Camera.main.transform.position.z) / 40);
        axisDot.transform.position = position;
        axisDot.transform.eulerAngles = new Vector3(0, 0, 0);

        return axisDot;
    }
}

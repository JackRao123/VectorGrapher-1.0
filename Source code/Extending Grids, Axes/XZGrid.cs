using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using System.Diagnostics;

using UnityEngine.Events;
using UnityEngine.EventSystems;

public class XZGrid : MonoBehaviour 
{
     public GameObject gridParent;

    public Material mat;



      List<GameObject> grids = new List<GameObject>();

    void DrawLine(Vector3 start, Vector3 end, Color color, float thickness)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = mat;
        //lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.startWidth = thickness;  
        lr.endWidth = thickness;
         

        myLine.transform.SetParent(gridParent.transform);
         
    }


    GameObject drawGrid(float scale, Color color, float thickness) //if scale = 1, thickness should be 2 or 3. Note - gap = 100 means scale = 1
    {

        Vector3[] positions = createGridArray(  Mathf.RoundToInt( 100 * scale));

        GameObject newLine = new GameObject();
        newLine.AddComponent<LineRenderer>();
        LineRenderer lr = newLine.GetComponent<LineRenderer>();
        lr.material = mat;
        lr.positionCount = positions.Length;
        lr.SetPositions(positions);


        lr.startWidth = thickness;
        lr.endWidth = thickness;

        newLine.transform.name = $"{scale}x{scale}GRID";
        newLine.transform.SetParent(gridParent.transform);

        return newLine;

    }


    private void Start()
    {
       
 



        Stopwatch sw = Stopwatch.StartNew();
        sw.Start();

        grids.Add(drawGrid(0.1f, Color.red, 0.3f));
        grids.Add(drawGrid(1, Color.red, 3f));
        grids.Add(drawGrid(10, Color.red, 30f));

      



        sw.Stop();
        print($"Took {sw.ElapsedMilliseconds}ms");



 
    }



    Vector3[] createGridArray(int gap ) // gap is the gap between the lines.  since the grid and worldspace are not the same, a gap of 100 means it draws the 1x1 grid.(the grid with spacing of 1 'unit', but 100 unity worldspace units)
    {
        List<Vector3> positions = new List<Vector3>();

        for (int x = -100000; x < 100000; x = x + 2*gap)
        {
            positions.Add(new Vector3(x,0,-100000));
            positions.Add(new Vector3(x, 0, 100000));
            positions.Add(new Vector3(x + gap, 0, 100000));
            positions.Add(new Vector3(x + gap, 0, -100000));
        }

        positions.Add(new Vector3(100000, 0, -100000));
        positions.Add(new Vector3(100000, 0, 100000));

        positions.Add(new Vector3(-100000, 0, 100000));

        for (int z = -100000; z < 100000; z = z + 2 * gap)
        {
            positions.Add(new Vector3(-100000, 0, z));
            positions.Add(new Vector3(100000, 0, z));
            positions.Add(new Vector3(100000, 0, z + gap));
            positions.Add(new Vector3(-100000 , 0, z + gap));
        }

        positions.Add(new Vector3(-100000, 0, 100000));
        positions.Add(new Vector3(100000, 0, 100000));

        Vector3[] positionsArray = positions.ToArray();
        return positionsArray;

       
    }

   



 
    public void hideShowGrid()
    {

         




        gridParent.SetActive(!gridParent.active);
        

        
    }

    private void Update()
    {
        showGridsBasedOnDistance();
    }


    bool firstRun = true; // true for the first time the showGridsBasedOnDistance is run. 
    //this is necessary so that the grids hide the first time, because they will initially all be showing (the "&&grids. active = false" -> initially they will all be active = true, so they code will not run).

    void showGridsBasedOnDistance()
    {
       

        float distanceFromXYPlane = Math.Abs(Camera.main.transform.position.y);


        if (distanceFromXYPlane < 300 && (grids[0].active == false || firstRun == true))
        {
            grids[0].active = true;
            grids[1].active = false;
            grids[2].active = false;
        }
        else
        {
            if (distanceFromXYPlane < 3000 && 300<  distanceFromXYPlane  && (grids[1].active == false || firstRun == true))
            {
                grids[0].active = false;
                grids[1].active = true;
                grids[2].active = false;
            }
            else
            {
                if (   3000 < distanceFromXYPlane && (grids[2].active == false || firstRun == true))
                {
                    grids[0].active = false;
                    grids[1].active = false;
                    grids[2].active = true;
                }
            }
        }

        firstRun = false;
        
    }


}

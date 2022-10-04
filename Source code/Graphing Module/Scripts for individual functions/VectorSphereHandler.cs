using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class VectorSphereHandler : MonoBehaviour
{

   


    public List<float> parseToCartesian(string inputString) //parses vector sphere to cartesian sphere
    {
        inputString = inputString.Replace(" ", "");
        string LHS = inputString.Split("=")[0];
        string RHS = inputString.Split("=")[1];

        //lhs is the |v(a) - (x,y,z)| stuff, rhs is the r 

        LHS = LHS.Replace("|", "");




        string[] LHSParts = LHS.Split(new[] { ")-(" }, StringSplitOptions.RemoveEmptyEntries);
        if (LHSParts.Length != 2)
        {
            LHSParts = LHS.Split(new[] { "-(" }, StringSplitOptions.RemoveEmptyEntries);
        }

        string centreVector = LHSParts[1].Replace(")","").Replace("(", "").Replace(" ","");

        string[] xyz = centreVector.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        List<float> returnParts = new List<float>() { float.Parse(xyz[0]), float.Parse(xyz[1]), float.Parse(xyz[2]), float.Parse(RHS)};



        return returnParts;


    }
}
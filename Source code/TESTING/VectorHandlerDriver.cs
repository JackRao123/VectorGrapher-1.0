using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class VectorHandlerDriver : MonoBehaviour
{
    VectorHandler vectorHandlerScript;
    void Start()
    {
        vectorHandlerScript = GameObject.FindGameObjectWithTag("NewFieldButton").GetComponent<VectorHandler>();

        driver();

    }



    void driver()
    {
        var inputToTest = new List<string>()
                    {
                        "(3,2,9)",
                        "((3,2,9))",
                        "(1,5,3) + (3,2,1) - (9,3,2) - (15,3,5)",
                        "Cross[(1,2,3),(-3,1,5)]",
                        "Proj[(3,2,9)on(1,5,3)]",
                        "Size(1,2,3)",
                        "Dot[(1,5,3),(-3,2,1)]",
                        "1+3-4*5/1.5*3-42",
                        "((((3,5,1)))) + ((((Cross[((3,1,5)),(1,-3,2)])))) - (1,5,9) - Proj[(-3,-3,-3)on((((((1.3,2,1))))))]",
                        "Cross[Proj[(1,32,1)on(-3.1,5,2)],((-3.2,15.3,-99) + (1,5,3) - (Dot[(3,1,5),(1,9,9)],Size(1,1,1),-3  ))]",
                        "Cross[Proj[(1,32,1)on(-3.1,5,2)],((-3.2,15.3,-99) + ( ((((3,5,1)))) + ((((Cross[((3,1,5)),(1,-3,2)])))) - (1,5,9) - Proj[(-3,-3,-3)on((((((1.3,2,1))))))] ) - (Dot[(3,1,5), Cross[Proj[(1,32,1)on(-3.1,5,2)],((-3.2,15.3,-99) + (1,5,3) - (Dot[(3,1,5),(1,9,9)],Size(1,1,1),-3  ))] ],Size(1,1,1),-3  ))]",
                        "Cross[Proj[(Cross[Proj[(1,32,1)on(-3.1,5,2)],((-3.2,15.3,-99) + ( ((((3,5,1)))) + ((((Cross[((3,1,5)),(1,-3,2)])))) - (1,5,9) - Proj[(-3,-3,-3)on((((((1.3,2,1))))))] ) - (Dot[(3,1,5), Cross[Proj[(Cross[Proj[(Cross[Proj[(1,32,1)on(-3.1,5,2)],((-3.2,15.3,-99) + ( ((((3,5,1)))) + ((((Cross[((3,1,5)),(1,-3,2)])))) - (1,5,9) - Proj[(-3,-3,-3)on((((((1.3,2,1))))))] ) - (Dot[(3,1,5), Cross[Proj[(1,32,1)on(-3.1,5,2)],((-3.2,15.3,-99) + (1,5,3) - (Dot[(3,1,5),(1,9,9)],Size(1,1,1),-3  ))] ],Size(Cross[Proj[(Cross[Proj[(1,32,1)on(-3.1,5,2)],((-3.2,15.3,-99) + ( ((((3,5,1)))) + ((((Cross[((3,1,5)),(1,-3,2)])))) - (1,5,9) - Proj[(-3,-3,-3)on((((((1.3,2,1))))))] ) - (Dot[(3,1,5), Cross[Proj[(1,32,1)on(-3.1,5,2)],((-3.2,15.3,-99) + (1,5,3) - (Dot[(3,1,5),(1,9,9)],Size(1,1,1),-3  ))] ],Size(1,1,1),-3  ))])on(-3.1,5,2)],((-3.2,15.3,-99) + (1,5,3) - (Dot[(3,1,5),(1,9,9)],Size(1,1,1),-3  ))]),-3  ))])on(-3.1,5,2)],((-3.2,15.3,-99) + (1,5,3) - (Dot[(3,1,5),(1,9,9)],Size(1,1,1),-3  ))])on(-3.1,5,2)],((-3.2,15.3,-99) + (1,5,3) - (Dot[(3,1,5),(1,9,9)],Size(1,1,1),-3  ))] ],Size(Cross[Proj[(Cross[Proj[(1,32,1)on(-3.1,5,2)],((-3.2,15.3,-99) + ( ((((3,5,1)))) + ((((Cross[((3,1,5)),(1,-3,2)])))) - (1,5,9) - Proj[(-3,-3,-3)on((((((1.3,2,1))))))] ) - (Dot[(3,1,5), Cross[Proj[(1,32,1)on(-3.1,5,2)],((-3.2,15.3,-99) + (1,5,3) - (Dot[(3,1,5),(1,9,9)],Size(1,1,1),-3  ))] ],Size(1,1,1),-3  ))])on(-3.1,5,2)],((-3.2,15.3,-99) + (1,5,3) - (Dot[(3,1,5),(1,9,9)],Size(1,1,1),-3  ))]),-3  ))])on(-3.1,5,2)],((-3.2,15.3,-99) + (1,5,3) - (Dot[(3,1,5),(1,9,9)],Size(1,1,1),-3  ))]"
                         

                    };



        string testResult = "";
        foreach(string item in inputToTest)
        {
            string input = item.Replace(" " , "");
            try
            {
                string output = vectorHandlerScript.mainVectorParser(input);
                testResult = testResult + $"Input = {input}.                                        Output = {output} \n";
            }
            catch(Exception e)
            {
                string errorType = e.GetType().Name;
                testResult = testResult + $"Input = {input}.                                        Output = {errorType} \n";

            }
            
            


            






        }
        print(testResult);

    }

     
}

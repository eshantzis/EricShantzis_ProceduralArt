using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EricInstructions_03 : MonoBehaviour
{
    public bool rebuild;

    public string[] paperColor;
    public string[] paperShape;

    public string[] smallShapes;

    void Start()
    {

    }

    void Update()
    {
        if (rebuild)
        {

            string output = "Choose a piece of ";

            string pColor = paperColor[Random.Range(0, paperColor.Length)];

            output += pColor + " paper.\nThen draw a large ";

            string pShape = paperShape[Random.Range(0, paperShape.Length)];

            output += pShape + " on the paper. \nThen, within that shape, draw a ";

            string bShape = smallShapes[Random.Range(0, smallShapes.Length)];

            output += bShape + " .\n";

            Debug.Log(output);

            rebuild = false;
        }
    }
}
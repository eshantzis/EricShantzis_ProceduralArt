using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleBasedInstructions : MonoBehaviour
{
    public bool rebuild;

    public string[] paperColor;

    public string[] doodleShape;
    public int minNumber;
    public int maxNumber;

    public string[] positionOnPaper;

    void Start()
    {

    }

    void Update()
    {
        if (rebuild)
        {

            string output = "Choose a piece of";

            string pColor = paperColor[Random.Range(0, paperColor.Length)];

            output += pColor + " paper.\nThen choose ";

            int howMany = Random.Range(minNumber, maxNumber);
            string sShape = doodleShape[Random.Range(0, doodleShape.Length)];

            output += howMany + " " + sShape + " doodles.\nPlace them ";

            string pPosition = positionOnPaper[Random.Range(0, positionOnPaper.Length)];

            output += pPosition + " of the paper.\nThen choose ";

            howMany *= 2;
            sShape = doodleShape[Random.Range(0, doodleShape.Length)];

            output += howMany + " " + sShape + " doodles.\nPlace them ";

            pPosition = positionOnPaper[Random.Range(0, positionOnPaper.Length)];

            output += pPosition + " of the paper.\n";

            Debug.Log(output);

            rebuild = false;
        }
    }
}
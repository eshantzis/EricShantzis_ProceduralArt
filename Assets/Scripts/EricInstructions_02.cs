using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EricInstructions_02 : MonoBehaviour
{
    public bool rebuild;

    public string[] facialExpression;

    public string[] thingOnFace;

    public string[] faceResponse;

    void Start()
    {

    }

    void Update()
    {
        if (rebuild)
        {

            string output = "Draw a ";

            string fExpression = facialExpression[Random.Range(0, facialExpression.Length)];

            output += fExpression + " face.\nThen add ";

            string fThing = thingOnFace[Random.Range(0, thingOnFace.Length)];

            output += fThing + ".\nAt the bottom write: ";

            string fResponse = faceResponse[Random.Range(0, faceResponse.Length)];

            output += fResponse;

            Debug.Log(output);

            rebuild = false;
        }
    }
}
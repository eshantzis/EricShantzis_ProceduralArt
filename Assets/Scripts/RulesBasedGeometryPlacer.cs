using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesBasedGeometryPlacer : MonoBehaviour
{
    public GameObject thing;
public GameObject otherThing;

public Vector3 position;
public Vector3 scale = Vector3.one;
public Vector3 rotation;
void Start()
{

}

// Update is called once per frame
void Update()
{
    thing.transform.position = position;
    thing.transform.localScale = scale;
    thing.transform.eulerAngles = rotation;

    otherThing.transform.position = position * -1;
    otherThing.transform.localScale = scale * -1;
    otherThing.transform.eulerAngles = rotation * -1;
}
}

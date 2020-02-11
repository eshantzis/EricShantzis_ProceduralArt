using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnObject : MonoBehaviour
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 jiggle = new Vector3(position.x, 
            position.y, 
            position.z);
        this.transform.position = position;
        this.transform.localEulerAngles = rotation;
        this.transform.localScale = scale;
    }
}

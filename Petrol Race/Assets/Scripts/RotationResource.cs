using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RotationResource : MonoBehaviour
{
    public float speed = 3.0f;
    public Vector3 rotateAround;
    public bool RotateAround;
    // Update is called once per frame
    void Update()
    {
        if (!RotateAround)
        {
            transform.Rotate(rotateAround, speed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(transform.parent.position, speed * Time.deltaTime);
        }
    }
}

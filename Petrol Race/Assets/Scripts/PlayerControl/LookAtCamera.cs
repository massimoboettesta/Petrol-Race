﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LookAtCamera : MonoBehaviour
{
    private Camera cam;
    private void Awake()
    {
        cam = Camera.main;

    }

    private void Update()
    {
        if(cam!=null)
        transform.eulerAngles = cam.transform.eulerAngles;
    }
}

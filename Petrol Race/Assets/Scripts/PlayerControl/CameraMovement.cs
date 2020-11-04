﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private bool isCamMoving;
    public float zoomSensitivity= 30.0f;
    public float speed = 3.0f;
    public float edgeSize = 20.0f;

    public float minZoom;
    public float maxZoom;
    private Camera camRef;

    void MoveCam()
    {
        Vector3 camPos = transform.position;
        if (Input.mousePosition.x > Screen.width - edgeSize)
        {
            isCamMoving = true;
            camPos.x += speed * Time.deltaTime;
        }
        else if (Input.mousePosition.x < edgeSize)
        {
            isCamMoving = true;
            camPos.x -= speed * Time.deltaTime;
        }

        else if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            isCamMoving = true;
            camPos.z += speed * Time.deltaTime;
        }
        else if (Input.mousePosition.y < edgeSize)
        {
            isCamMoving = true;
            camPos.z -= speed * Time.deltaTime;
        }
        else
        {
            isCamMoving = false;
        }
        if(isCamMoving)
        transform.position = camPos;
    }
    private void ZoomInOut()
    {
        float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel");
        if (ScrollWheelChange != 0&&ZoomCheck(ScrollWheelChange<0))
        {                                            //If the scrollwheel has changed
            float R = ScrollWheelChange * 15;                                   //The radius from current camera
            float PosX = camRef.transform.eulerAngles.x + 90;              //Get up and down
            float PosY = -1 * (camRef.transform.eulerAngles.y - 90);       //Get left to right
            PosX = PosX / 180 * Mathf.PI;                                       //Convert from degrees to radians
            PosY = PosY / 180 * Mathf.PI;                                       //^
            float X = R * Mathf.Sin(PosX) * Mathf.Cos(PosY);                    //Calculate new coords
            float Z = R * Mathf.Sin(PosX) * Mathf.Sin(PosY);                    //^
            float Y = R * Mathf.Cos(PosX);                                      //^
            float CamX = camRef.transform.position.x;                      //Get current camera postition for the offset
            float CamY = camRef.transform.position.y;                      //^
            float CamZ = camRef.transform.position.z;                      //^
            camRef.transform.position = new Vector3(CamX + X, CamY + Y, CamZ + Z);//Move the main camera
            
        }
    }

    private bool ZoomCheck(bool goingIN)
    {

        float distance;

        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = Vector3.down;

        RaycastHit hit;
       
        if (Physics.Raycast(ray, out hit, 1000))
        {
            
            distance = Vector3.Distance(transform.position,hit.point);

            if (distance > minZoom && !goingIN)
            {
                return true;
            } 
            else if(distance < maxZoom && goingIN)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    private void Awake()
    {
        camRef = GetComponent<Camera>();
    }
   

    // Update is called once per frame
    void LateUpdate()
    {

        MoveCam();
        ZoomInOut();
    }
}

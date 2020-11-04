using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private bool isCamMoving;
    public float speed = 3.0f;
    public float edgeSize = 20.0f;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MoveCam();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 2f;
    public bool moveCamera = false;
    public bool rotateCamera = false;

    void Start()
    {
        //Camera.main.transform.rotation *= Quaternion.Euler(0, 0, 180);
        //Camera.main.transform.Rotate(0, 0 * Time.deltaTime, 180);
        Invoke("StartCameraMove", 1);
    }

    void Update()
    {
        if (Camera.main.transform.rotation.z > 0 && rotateCamera)
        {
            transform.RotateAround(Camera.main.transform.position, Vector3.forward, 200 * Time.deltaTime);
        }
        else
        {
            if (moveCamera)
            {
                transform.rotation = Quaternion.identity;
                transform.position += Vector3.right * Time.deltaTime * speed;
            }
        }
    }

    public void StartCameraMove()
    {
        moveCamera = true;
    }

    public void StartRotateCamera()
    {
        rotateCamera = true;
    }
}

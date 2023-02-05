using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 2f;
    public bool moveCamera = false;
    public bool rotateCamera = false;
    public bool liftCamera = false;

    void Start()
    {
        //Camera.main.transform.rotation *= Quaternion.Euler(0, 0, 180);
        //Camera.main.transform.Rotate(0, 0 * Time.deltaTime, 180);
    }

    void Update()
    {
        if (Camera.main.transform.rotation.z > 0 && rotateCamera)
        {
            transform.RotateAround(Camera.main.transform.position, Vector3.forward, 200 * Time.deltaTime);
        }
        else if (Camera.main.transform.position.y < 0.6 && liftCamera) {
            transform.position += new Vector3(0, 0.1f, 0);
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

    public void StartLiftCamera() {
        liftCamera = true;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject logo;

    public GameObject joystickCanvas;
    public GameObject introCanvas;
    public GameObject successCanvas;
    public GameObject startScreenCanvas;
    public GameObject deathCanvas;

    public CameraMove cameraMove;

    bool startEvadingLogo = false;
    float logoOpacity = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startEvadingLogo && logoOpacity > 0) {
            logoOpacity -= 0.01f;
            logo.GetComponent<SpriteRenderer>().color = new Color(255,255,255,logoOpacity);
        }
    }

    public void ClickStart() {
        // Invoke("StartCameraMove", 1);
        startEvadingLogo = true;

        Invoke("ShowIntroCanvas", 1);
        Invoke("HideStartScreenCanvas", 1);
        Invoke("StartRotateCamera", 4);
        Invoke("HideIntroCanvas", 4);
        Invoke("StartLiftCamera", 6);
        Invoke("StartMoveCamera", 8);
    }

    public void ShowIntroCanvas() {
        introCanvas.SetActive(true);
    }

    public void HideIntroCanvas() {
        introCanvas.SetActive(false);
    }
    
    public void ShowDeathCanvas() {
        deathCanvas.SetActive(true);
    }

    public void HideDeathCanvas() {
        deathCanvas.SetActive(false);
    }
        
    public void ShowSuccessCanvas() {
        successCanvas.SetActive(true);
    }

    public void HideSuccessCanvas() {
        successCanvas.SetActive(false);
    }

    public void ShowStartScreenCanvas() {
        startScreenCanvas.SetActive(true);
    }

    public void HideStartScreenCanvas() {
        startScreenCanvas.SetActive(false);
    }

    public void StartRotateCamera() {
        cameraMove.StartRotateCamera();
    }

    public void StartMoveCamera() {
        cameraMove.StartCameraMove();
    }

    public void StartLiftCamera() {
        cameraMove.StartLiftCamera();
    }
}

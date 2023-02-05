using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject logo;

    public GameObject mole;

    public GameObject joystickCanvas;
    public GameObject introCanvas;
    public GameObject successCanvas;
    public GameObject startScreenCanvas;
    public GameObject deathCanvas;

    public GameObject startSound;
    public GameObject gameSound;
    public GameObject deathSound;

    public CameraMove cameraMove;
    public Tree tree;

    bool startEvadingLogo = false;
    float logoOpacity = 1;

    bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        EnableStartSound();
        StartCoroutine(StartGameMusic());
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
        startEvadingLogo = true;

        Invoke("ShowIntroCanvas", 1);
        Invoke("HideStartScreenCanvas", 1);
        Invoke("EnableGameSound", 1);
        Invoke("StartRotateCamera", 4);
        Invoke("HideIntroCanvas", 4);
        Invoke("StartLiftCamera", 6);
        Invoke("StartMoveCamera", 8);
        Invoke("EnableJoystickCanvas", 8);
        Invoke("StartGrowRoot", 9f);
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

    public void StartGrowRoot()
    {
        tree.StartGrowingRoots();
    }

    public void EnableJoystickCanvas()
    {
        joystickCanvas.SetActive(true);
    }

    public void EnableStartSound()
    {
        startSound.SetActive(true);
        gameSound.SetActive(false);
        deathSound.SetActive(false);
    }

    public void EnableGameSound()
    {
        if (!gameStarted)
        {
            startSound.SetActive(false);
            gameSound.SetActive(true);
            deathSound.SetActive(false);
        }
    }

    public void EnableDeathSound()
    {
        startSound.SetActive(false);
        gameSound.SetActive(false);
        deathSound.SetActive(true);
    }

    public void GameOver()
    {
        mole.SetActive(false);
        joystickCanvas.SetActive(false);
        EnableDeathSound();
        ShowDeathCanvas();
    }

    public void ClickRestartGame()
    {
        Debug.Log("test");
        SceneManager.LoadScene("GameScene");
    }

    IEnumerator StartGameMusic()
    {
        yield return new WaitForSeconds(4);

        if (!gameStarted)
        {
            EnableGameSound();
        }
    }
}

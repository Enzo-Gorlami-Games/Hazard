using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimedSceneManager : MonoBehaviour, TimerInterface
{
    private const string WIN_TEXT = "YOU WIN!";
    private const string LOSE_TEXT = "YOU LOST!";

    private bool isSceneSafe;
    private GameObject player;
    private GameObject fpsCamera;
    private Button button;

    [SerializeField] string nextLevel;
    [SerializeField] CountdownTimer countDownTimer;
    [SerializeField] List<Stateful> hazardousObjects;
    [SerializeField] Button lose_button;
    [SerializeField] TextMeshProUGUI winLoseIndicator;
    [SerializeField] TextMeshProUGUI buttonText;


    public void onTimeOut()
    {
        isSceneSafe = scheckScene();
        stopPlayerMovement();
        Cursor.lockState = CursorLockMode.None;


        if (isSceneSafe)
        {
            winLoseIndicator.text = WIN_TEXT;
            button.onClick.AddListener(winOnClick);
            buttonText.text = "Next Level";
            
        }
        else
        {
            winLoseIndicator.text = LOSE_TEXT;
            button.onClick.AddListener(loseOnClick);
        }
        winLoseIndicator.enabled = true;
        button.gameObject.SetActive(true);
        
    }

    private void loseOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void winOnClick()
    {
        SceneManager.LoadScene(nextLevel);
    }

    private void stopPlayerMovement()
    {
        player.GetComponent<KeyboardMover>().enabled = false; // disabling keyboard mover
        fpsCamera.GetComponent<MouseLookAround>().enabled = false; // disabling player look
    }

    private bool scheckScene()
    {
        foreach (Stateful stateful in hazardousObjects)
        {
            if (stateful.getState())
            {
                return false;
            }
        }
        return true;
    }

    void Start()
    {
        winLoseIndicator.enabled = false;
        button = lose_button;
        button.gameObject.SetActive(false);
        countDownTimer.subscribe(this);
        player = GameObject.FindGameObjectWithTag("Player");
        fpsCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (scheckScene())
        {
            onTimeOut();
            countDownTimer.stopTime();
        }
    }

}

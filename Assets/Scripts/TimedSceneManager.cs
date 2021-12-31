using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimedSceneManager : MonoBehaviour, TimerInterface
{
    private const string WIN_TEXT = "YOU WIN!";
    private const string LOSE_TEXT = "YOU LOST!";
    private const string WIN_BUTTON_TEXT = "NEXT LEVEL";
    private const string LOSE_BUTTON_TEXT = "RESTART LEVEL";

    private bool isSceneSafe;
    private GameObject player;
    private GameObject camera;

    [SerializeField] string nextLevel;
    [SerializeField] private CountdownTimer countDownTimer;
    [SerializeField] List<Stateful> hazardousObjects;
    [SerializeField] Button button;
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
            buttonText.text = WIN_BUTTON_TEXT;
            button.onClick.AddListener(WinOnClick);
        }
        else
        {
            winLoseIndicator.text = LOSE_TEXT;
            buttonText.text = LOSE_BUTTON_TEXT;
            button.onClick.AddListener(LoseOnClick);
        }
        winLoseIndicator.enabled = true;
        button.gameObject.SetActive(true);
        
    }

    private void WinOnClick()
    {
        SceneManager.LoadScene(nextLevel);
    }

    private void LoseOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void stopPlayerMovement()
    {
        player.GetComponent<KeyboardMover>().enabled = false; // disabling keyboard mover
        camera.GetComponent<MouseLookAround>().enabled = false; // disabling player look
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
        button.gameObject.SetActive(false);
        countDownTimer.subscribe(this);
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (isSceneSafe)
        {
            winLoseIndicator.text = WIN_TEXT;
            //winLoseIndicator.enabled = true;
        }
    }

}

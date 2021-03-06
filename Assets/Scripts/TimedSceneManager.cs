using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimedSceneManager : MonoBehaviour, TimerInterface
{
    public string WIN_TEXT = "YOU WIN!";
    public string LOSE_TEXT = "YOU LOST!";

    private bool isSceneSafe;
    private bool isAudioPlayed;
    private GameObject player;
    private GameObject fpsCamera;
    private Button button;
    private AudioManager audioManager;

    [SerializeField] string nextLevel;
    [SerializeField] CountdownTimer countDownTimer;
    [SerializeField] List<Stateful> statefulObjects;
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
            if (!isAudioPlayed)
            {
                audioManager.Play("Win Sound");
                isAudioPlayed = true;
            }
            winLoseIndicator.text = WIN_TEXT;
            button.onClick.AddListener(winOnClick);
            buttonText.text = "Next Level";
            
        }
        else
        {
            if (!isAudioPlayed)
            {
                audioManager.Play("Lose Sound");
                Debug.Log("Found audioManager");
                isAudioPlayed = true;
            }
            winLoseIndicator.text = LOSE_TEXT;
            button.onClick.AddListener(loseOnClick);
        }
        winLoseIndicator.enabled = true;
        button.gameObject.SetActive(true);
        audioManager.Stop("Ticking Clock");
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
        foreach (Stateful stateful in statefulObjects)
        {
            // if one object is in an illegal state
            if (!stateful.getState())
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
        audioManager = FindObjectOfType<AudioManager>();
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

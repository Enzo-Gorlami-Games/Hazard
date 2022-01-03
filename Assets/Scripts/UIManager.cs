using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] CountdownTimer timer;
    [SerializeField] GameObject player;
    [SerializeField] GameObject fpsCamera;
    [SerializeField] List<string> instructions;
    [SerializeField] TextMeshProUGUI instructionDisplay;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] Button nextButton;

    private int index;
    private int size;
    private bool isPaused;

    public void instructionsOnClick()
    {
        ++index;
        if (index == size)
        {
            startScene();
        }
        else
        {
            setInstructionUI();
        }
    }

    public void startScene()
    {
        nextButton.gameObject.SetActive(false);
        instructionDisplay.enabled = false;
        enablePlayerMovement();
        timer.startTime();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void pauseScene()
    {
        timer.stopTime();
        Cursor.lockState = CursorLockMode.None;
        disablePlayerMovement();
    }

    private void enablePlayerMovement()
    {
        player.GetComponent<KeyboardMover>().enabled = true; // enabling keyboard mover
        fpsCamera.GetComponent<MouseLookAround>().enabled = true; // enabling player look
    }

    private void disablePlayerMovement()
    {
        player.GetComponent<KeyboardMover>().enabled = false; // disabling keyboard mover
        fpsCamera.GetComponent<MouseLookAround>().enabled = false; // disabling player look
    }

    private void setInstructionUI()
    {
        instructionDisplay.text = instructions[index];
        if (index == size - 1)
        {
            buttonText.text = "Start Level";
        }
        else
        {
            buttonText.text = "Next";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer.stopTime();
        disablePlayerMovement();
        index = 0;
        size = instructions.Count;
        setInstructionUI();
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                startScene();
            }
            else
            {
                pauseScene();
            }
        }    
    }
}

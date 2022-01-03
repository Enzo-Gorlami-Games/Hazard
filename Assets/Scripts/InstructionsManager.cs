using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsManager : MonoBehaviour
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

    public void onClick()
    {
        ++index;
        if (index == size)
        {
            startScene();
        }
        else
        {
            setText();
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

    private void setText()
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
        setText();
        Cursor.lockState = CursorLockMode.None;
    }
}

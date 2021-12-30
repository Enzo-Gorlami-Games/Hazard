using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TimedSceneManager : MonoBehaviour, TimerInterface
{

    [SerializeField] string nextLevel;
    [SerializeField] private CountdownTimer countDownTimer;
    [SerializeField] List<Stateful> hazardousObjects;
    [SerializeField] Text winLoseIndicator;

    private const string WIN_TEXT = "You Win!";
    private const string LOSE_TEXT = "You Lose!";

    private bool isSceneSafe()
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

    public void onTimeOut()
    {
        if (isSceneSafe())
        {
            winLoseIndicator.text = WIN_TEXT;
        }
        else
        {
            winLoseIndicator.text = LOSE_TEXT;
        }
        winLoseIndicator.enabled = true;
    }

    void Start()
    {
        winLoseIndicator.enabled = false;
        countDownTimer.subscribe(this);
    }

    void Update()
    {
        if (isSceneSafe())
        {
            winLoseIndicator.text = WIN_TEXT;
            winLoseIndicator.enabled = true;
        }
    }

}

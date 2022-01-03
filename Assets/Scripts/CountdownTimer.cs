using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    private float currentTime = 0f;
    private LinkedList<TimerInterface> subscribers;
    private bool timeStopped;
    private bool audioPlayed;
    private bool audioStopped;

    [SerializeField] float startingTime = 10f;
    [SerializeField] Text countDownText;
    [SerializeField] AudioManager audioManager;


    // constructor
    public CountdownTimer(float time, Text countDownText)
    {
        setStartingTime(time);
        setCountdownText(countDownText);
        currentTime = startingTime;
    }

    // ===== Setters & Getters ===== //
    public float getCurrentTime()
    {
        return currentTime;
    }

    public void setStartingTime(float time)
    {
        this.startingTime = time;
    }

    public void setCountdownText(Text cdt)
    {
        this.countDownText = cdt;
    }

    public void stopTime()
    {
        timeStopped = true;
    }

    public void startTime()
    {
        timeStopped = false;
    }
    // called once per frame
    void Update()
    {
        if (!timeStopped)
        {
            currentTime -= 1 * Time.deltaTime;
            countDownText.text = currentTime.ToString("F1");
        }

        if (currentTime <= 10)
        {
            countDownText.color = Color.red;
            if(!audioPlayed)
            {
                audioManager.Play("Ticking Clock");
                audioPlayed = true;
            }
        }

        if (currentTime <= 0)
        {
            currentTime = 0;
            if (!audioStopped)
            {
                audioManager.Stop("Ticking Clock");
                audioStopped = true;
            }
            timeOut();
        }
    }

    // called on scene load
    void Start()
    {
        if (subscribers == null)
        {
            subscribers = new LinkedList<TimerInterface>();
        }
        currentTime = startingTime;
    }

    public void subscribe(TimerInterface subscriber)
    {
        if (subscribers == null)
        {
            subscribers = new LinkedList<TimerInterface>();
        }
        subscribers.AddLast(subscriber);
    }

    private void timeOut()
    {
        if (subscribers.Count > 0)
        {
            foreach (TimerInterface sub in subscribers)
            {
                sub.onTimeOut();
            }
            GetComponent<CountdownTimer>().enabled = false;
        }

    }

}

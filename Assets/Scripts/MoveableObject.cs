using System;
using UnityEngine;

public class MoveableObject : Stateful
{
    private string movingMsg = "put down";
    private string staticMsg = "pick up";
    private bool isMoving;

    private GameObject playerCamera;
    private Transform parent;

    public override string getOpenMsg()
    {
        return staticMsg;
    }

    public override bool getState()
    {
        return isMoving;
    }

    public override string getCloseMsg()
    {
        return movingMsg;
    }

    public override void switchState()
    {
        if (isMoving)
        {
            gameObject.transform.parent = parent;
        }
        else
        {
            gameObject.transform.parent = playerCamera.transform;

        }
        isMoving = !isMoving;
    }

    public override void displayState()
    {
        return;
    }

    private void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        parent = transform.parent;
    }
}

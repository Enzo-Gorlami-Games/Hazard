using System;
using UnityEngine;

public class MoveableObject : Stateful
{
    private string movingMsg = "put down";
    private string staticMsg = "pick up";
    private bool isMoving;

    private GameObject camera;
    private Transform parent;

    public override string getOpenMsg()
    {
        return movingMsg;
    }

    public override bool getState()
    {
        return isMoving;
    }

    public override string getCloseMsg()
    {
        return staticMsg;
    }

    public override void switchState()
    {
        if (isMoving)
        {
            gameObject.transform.parent = parent;
        }
        else
        {
            gameObject.transform.parent = camera.transform;

        }
        isMoving = !isMoving;
    }

    public override void displayState()
    {
        return;
    }

    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        parent = transform.parent;
        Debug.Log(parent);
    }
}

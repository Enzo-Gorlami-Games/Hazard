using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveableObject : Stateful
{
    private string movingMsg = "put down";
    private string staticMsg = "pick up";
    public bool isMoving;

    private Transform parent;

    [SerializeField] GameObject dest;

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

        isMoving = !isMoving;
    }

    public override void displayState()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (isMoving)
        {
            rigidbody.useGravity = false;
            rigidbody.transform.parent = dest.transform;
            transform.parent = dest.transform;
        }
        else
        {
            this.transform.parent = parent;
            rigidbody.transform.parent = parent;
            rigidbody.useGravity = true;
        }
        
    }

    private void Start()
    {
        parent = transform.parent;
    }
}

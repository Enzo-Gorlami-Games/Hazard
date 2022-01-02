using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveableObject : Stateful
{
    private string movingMsg = "put down";
    private string staticMsg = "pick up";
    public bool isMoving;

    private Transform parent;
    private Rigidbody rigidbody;

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
        
        if (isMoving)
        {
            rigidbody.useGravity = false;
            rigidbody.freezeRotation = true;
            rigidbody.transform.parent = dest.transform;
        }
        else
        {
            rigidbody.freezeRotation = false;
            rigidbody.useGravity = true;
            rigidbody.transform.parent = parent;
            
        }
        
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        parent = transform.parent;
    }

    private void Update()
    {
        if (isMoving)
        {
            if(Vector3.Distance(rigidbody.transform.position, dest.transform.position) > .5)
            {
                rigidbody.position = dest.transform.position;
                displayState();
            }
        }
    }
}

using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveableObject : Stateful
{
    private string movingMsg = "put down";
    private string staticMsg = "pick up";
    private bool isMoving;

    private Transform parent;
    private Rigidbody rb;
    private Quaternion originalRotationValue;
    private AudioManager audioManager;

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
            rb.useGravity = false;
            rb.freezeRotation = true;
            rb.isKinematic = true;
            rb.transform.parent = dest.transform;
            rb.transform.rotation = originalRotationValue;

            audioManager.Play("Pickup Sound");
        }
        else
        {
            rb.freezeRotation = false;
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.transform.parent = parent;
            rb.transform.rotation = originalRotationValue;
            rb.drag = 10;

            audioManager.Play("Putdown Sound");
            
        }
        
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        parent = transform.parent;
        originalRotationValue = transform.rotation;
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (isMoving)
        {
            if(Vector3.Distance(rb.transform.position, dest.transform.position) > .5)
            {
                rb.position = dest.transform.position;
                displayState();
            }
        }
    }
}

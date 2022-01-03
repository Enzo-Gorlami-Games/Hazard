using System;
using UnityEngine;

public class LocationPlate : Stateful
{
    public bool isSatisfied;

    [SerializeField] string satisfyingTag;

    public override void displayState()
    {
        if (isSatisfied)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }
        
    public override void switchState()
    {
        isSatisfied = !isSatisfied;
    }

    public override bool getState()
    {
        return isSatisfied;
    }

    public override string getCloseMsg()
    {
        throw new NotImplementedException();
    }

    public override string getOpenMsg()
    {
        throw new NotImplementedException();
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == satisfyingTag)
        {
            switchState();
            displayState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == satisfyingTag)
        {
            switchState();
            displayState();
        }
    }


}
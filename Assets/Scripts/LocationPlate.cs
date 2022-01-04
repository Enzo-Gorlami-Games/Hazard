using System;
using UnityEngine;

public class LocationPlate : Stateful
{
    public bool isSatisfied;

    [SerializeField] string satisfyingTag;

    public override void displayState()
    {
        throw new NotImplementedException();
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
            isSatisfied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == satisfyingTag)
        {
            isSatisfied = false;
        }
    }


}
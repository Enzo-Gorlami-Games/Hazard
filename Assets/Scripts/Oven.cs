using UnityEngine;
using System.Collections;

public class Oven : Stateful
{
    [SerializeField] bool isSafe;

    private string openMsg = "turn off flame";
    private string closeMsg = "turn on flame";

    public override void displayState()
    {
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("Particle"))
            {
                ParticleSystem childPartical = child.gameObject.GetComponent<ParticleSystem>();
                var em = childPartical.emission;
                if (!isSafe)
                {   
                    em.enabled = true;
                }
                else
                {
                    em.enabled = false;
                }

            }
        }
    }
    public override bool getState()
    {
        return isSafe;
    }
    public override void switchState()
    {
        isSafe = !isSafe;
    }

    public override string getCloseMsg()
    {
        return closeMsg;
    }

    public override string getOpenMsg()
    {
        return openMsg;
    }
}

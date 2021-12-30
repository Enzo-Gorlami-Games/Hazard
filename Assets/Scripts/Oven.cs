using UnityEngine;
using System.Collections;

public class Oven : Stateful
{
    [SerializeField] private bool isHazardous;

    private string openMsg = "turn on flame";
    private string closeMsg = "turn off flame";

    public override void displayState()
    {
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("Particle"))
            {
                ParticleSystem childPartical = child.gameObject.GetComponent<ParticleSystem>();
                var em = childPartical.emission;
                if (isHazardous)
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
        return isHazardous;
    }
    public override void switchState()
    {
        isHazardous = !isHazardous;
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

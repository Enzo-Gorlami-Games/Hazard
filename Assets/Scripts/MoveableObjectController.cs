using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Stateful))]
public class MoveableObjectController : MonoBehaviour
{
    private const string COLLIDE_TAG = "Player";

    [SerializeField] Text actionMsg;

    public float reachRange = 1.8f;
    private Stateful statefulObject;
    private bool playerEntered;
    private string actionMsgTxt = "Press F to ";
    



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == COLLIDE_TAG)
        {
            playerEntered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == COLLIDE_TAG)
        {
            Debug.Log("Player exit");
            playerEntered = false;
            actionMsg.enabled = false;
        }
    }

    private string getGuiMsg(bool isHazard)
    {
        string rtnMsg = actionMsgTxt;
        rtnMsg += isHazard ? statefulObject.getCloseMsg() : statefulObject.getOpenMsg();
        return rtnMsg;
    }


    void Start()
    {
        statefulObject = GetComponent<Stateful>();
        actionMsg.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerEntered)
        {
            Debug.Log("Player Entered");
            actionMsgTxt = getGuiMsg(statefulObject.getState());
            actionMsg.text = actionMsgTxt;
            actionMsg.enabled = true ;
            if (Input.GetKeyUp(KeyCode.F))
            {
                statefulObject.switchState();
                statefulObject.displayState();
            }
        }
        else actionMsg.enabled = false;
        actionMsgTxt = "Press F to ";
    }
}

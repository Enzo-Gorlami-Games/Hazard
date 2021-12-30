using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Stateful))]
public class MoveableObjectController : MonoBehaviour
{
    private const string COLLIDE_TAG = "Player";

    [SerializeField] private MessageController actionMsg;
    public float reachRange = 1.8f;

    private bool playerEntered;
    private string actionMsgTxt = "Press F to ";
    private Stateful statefulObject;



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
            playerEntered = false;
            actionMsg.hide();
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
    }
    // Update is called once per frame
    void Update()
    {
        if (playerEntered)
        {
            Debug.Log("Player Entered");
            actionMsgTxt = getGuiMsg(statefulObject.getState());
            actionMsg.setMsg(actionMsgTxt);
            actionMsg.show();
            if (Input.GetKeyUp(KeyCode.F))
            {
                statefulObject.switchState();
                statefulObject.displayState();
            }
        }
        else actionMsg.hide();
        actionMsgTxt = "Press F to ";
    }
}

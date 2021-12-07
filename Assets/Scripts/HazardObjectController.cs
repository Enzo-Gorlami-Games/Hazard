using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Stateful))]
public class HazardObjectController : MonoBehaviour
{
    [SerializeField] private MessageController actionMsg;
    public float reachRange = 1.8f;

    private Stateful statefulObject;
    private Camera fpsCam;
    private GameObject player;


    private bool playerEntered;
    private string actionMsgTxt = "Press E to ";

    private int rayLayerMask;

    // Use this for initialization
    void Start()
    {
        // initialize relevant game objects
        player = GameObject.FindGameObjectWithTag("Player");
        fpsCam = Camera.main;

        if(fpsCam == null)
        {
            Debug.LogError("A camera tagged 'MainCamera is missiing");
        }

        statefulObject = GetComponent<Stateful>();
        statefulObject.displayState();

        LayerMask iRayLM = LayerMask.NameToLayer("InteractRaycast");
        rayLayerMask = 1 << iRayLM.value;

        actionMsg.hide();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)     //player has collided with trigger
        {
            Debug.Log("Player Entered");
            playerEntered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)     //player has exited trigger
        {
            Debug.Log("Player Exited");
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

    // Update is called once per frame
    void Update()
    {
        if(playerEntered)
        {
            //var ray = fpsCam.ScreenPointToRay(Input.mousePosition);
            //center point of viewport in World space.
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            if(Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, reachRange, rayLayerMask))
            {
                actionMsgTxt = getGuiMsg(statefulObject.getState());
                actionMsg.setMsg(actionMsgTxt);
                actionMsg.show();
                if(Input.GetKeyUp(KeyCode.F))
                {
                    statefulObject.switchState();
                    statefulObject.displayState();
                }
            }
        }
        else actionMsg.hide();
        actionMsgTxt = "Press F to ";
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Stateful))]
public class HazardObjectController : MonoBehaviour
{
    [SerializeField] Text actionMsg;

    public float reachRange = 1.8f;
    private Stateful statefulObject;
    private Camera fpsCam;
    private GameObject player;


    private bool playerEntered;
    private string actionMsgTxt = "Press F to ";

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

        actionMsg.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)     //player has collided with trigger
        {
            playerEntered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)     //player has exited trigger
        {
            playerEntered = false;
            actionMsg.gameObject.SetActive(false);
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
        if (playerEntered)
        {
            actionMsgTxt = getGuiMsg(statefulObject.getState());
            actionMsg.text = actionMsgTxt;
            actionMsg.gameObject.SetActive(true);
            if (Input.GetKeyUp(KeyCode.F))
            {
                statefulObject.switchState();
                statefulObject.displayState();
            }
        }
        else actionMsg.gameObject.SetActive(false);
        actionMsgTxt = "Press F to ";
    }
}

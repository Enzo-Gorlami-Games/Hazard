using UnityEngine;
using System.Collections;

public class StatefulObjectController : MonoBehaviour
{

	private const string ACTION_KEY = "F";

	public float reachRange = 1.8f;

	private Stateful stateful;
	private Camera fpsCam;
	private GameObject player;

	private bool playerEntered;
	private bool showInteractMsg;
	private GUIStyle guiStyle;
	private string msg;

	private int rayLayerMask;
		

	void Start()
	{
		//Initialize moveDrawController if script is enabled.
		player = GameObject.FindGameObjectWithTag("Player");

		fpsCam = Camera.main;
		if (fpsCam == null)	//a reference to Camera is required for rayasts
		{
			Debug.LogError("A camera tagged 'MainCamera' is missing.");
		}

		stateful = GetComponent<Stateful>();

		//the layer used to mask raycast for interactable objects only
		LayerMask iRayLM = LayerMask.NameToLayer("InteractRaycast");
		rayLayerMask = 1 << iRayLM.value;  

		//setup GUI style settings for user prompts
		setupGui();
	}
		
	void OnTriggerEnter(Collider other) // - V
	{		
		if (other.gameObject == player)		//player has collided with trigger
		{			
			playerEntered = true;
		}
	}

	void OnTriggerExit(Collider other)
	{		
		if (other.gameObject == player)		//player has exited trigger
		{			
			playerEntered = false;
			showInteractMsg = false;		
		}
	}



	void Update()
	{		
		if (playerEntered)
		{	

			//center point of viewport in World space.
			Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0f));
			RaycastHit hit;

			//if raycast hits a collider on the rayLayerMask
			if (Physics.Raycast(rayOrigin,fpsCam.transform.forward, out hit,reachRange,rayLayerMask))
			{
				Stateful statefulObject = null;
				//is the object of the collider player is looking at the same as me?
				if (!isEqualToParent(hit.collider, out statefulObject))
				{   //it's not so return;
					return;
				}
					
				if (statefulObject != null)		//hit object must have MoveableDraw script attached
				{
					showInteractMsg = true;
					msg = getGuiMsg(stateful);	//need current state for message.
											
					if (Input.GetKeyDown(KeyCode.F))
					{
						stateful.switchState();
						stateful.displayState();
						msg = getGuiMsg(stateful);
					}
				}
			}
			else
			{
				showInteractMsg = false;
			}
		}

	}

	//is current gameObject equal to the gameObject of other.  check its parents
	private bool isEqualToParent(Collider other, out Stateful draw)
	{
		draw = null;
		bool rtnVal = false;
		try
		{
			int maxWalk = 6;
			draw = other.GetComponent<Stateful>();

			GameObject currentGO = other.gameObject;
			for(int i=0;i<maxWalk;i++)
			{
				if (currentGO.Equals(this.gameObject))
				{
					rtnVal = true;
					if (draw == null)
					{
						draw = currentGO.GetComponentInParent<Stateful>();
					}
					break;			//exit loop early.
				}

				//not equal to if reached this far in loop. move to parent if exists.
				if (currentGO.transform.parent != null)		//is there a parent
				{
					currentGO = currentGO.transform.parent.gameObject;
				}
			}
		} 
		catch (System.Exception e)
		{
			Debug.Log(e.Message);
		}
			
		return rtnVal;
	}
		

	#region GUI Config

	//configure the style of the GUI
	private void setupGui()
	{
		guiStyle = new GUIStyle();
		guiStyle.fontSize = 30;
		guiStyle.fontStyle = FontStyle.Bold;
		guiStyle.normal.textColor = Color.white;
		msg = "Press F to Open";
	}

	private string getGuiMsg(Stateful statefulObject)
    {
		string res = "Press " + ACTION_KEY + " to "; 
        if (statefulObject.getState())
        {
			res += statefulObject.getCloseMsg();
        }
        else
        {
			res += statefulObject.getOpenMsg();
		}
		return res;
    }

	void OnGUI()
	{
		if (showInteractMsg)  //show on-screen prompts to user for guide.
		{
			GUI.Label(new Rect (50,Screen.height - 50,200,50), msg,guiStyle);
		}
	}
	//End of GUI Config --------------
	#endregion
}

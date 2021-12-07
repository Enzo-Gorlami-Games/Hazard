using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageController : MonoBehaviour
{
    [SerializeField] Text message;

    public void show()
    {
        message.enabled = true;
    }

    public void hide()
    {
        message.enabled = false;
    }

    public void setMsg(string msgTxt)
    {
        message.text = msgTxt;
    }
}

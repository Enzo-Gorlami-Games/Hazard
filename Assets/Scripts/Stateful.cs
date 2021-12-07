/// <summary>
///  AUTHOR: Tal Zichlinsky
/// 
///  This abstract class can be implemented by any object who wishes to manage
///  its binary state and to indicate it to other game objects or, visually, to the player
///  
/// </summary>

using UnityEngine;
using System;

[Serializable]
public abstract class Stateful : MonoBehaviour
{
    public abstract void displayState();
    public abstract bool getState();
    public abstract void switchState();
    public abstract string getOpenMsg();
    public abstract string getCloseMsg();
}

/// <summary>
///  AUTHOR: Tal Zichlinsky
/// 
///  This abstract class can be implemented by any object who wishes to manage
///  its binary state and to indicate it to other game objects or, visually, to the player
///  
/// </summary>

using System;
using UnityEngine;

[Serializable]
public abstract class IMoveableObject : MonoBehaviour
{
    public abstract bool getState();
    public abstract void switchState();
    public abstract string getStaticMsg();
    public abstract string getMovingMsg();
}


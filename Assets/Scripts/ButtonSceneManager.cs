// AUTHOR: Tal Zichlinsky

// This script can be attached to a button and on pressing that button,
// nextScene will be loaded

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneManager : MonoBehaviour
{

    public void loadScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }
}
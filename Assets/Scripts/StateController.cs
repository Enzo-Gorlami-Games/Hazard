using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class StateController : MonoBehaviour
{
    [SerializeField] KeyCode actionKey; 
    private void OnTriggerEnter(Collider collision)
    {

        // Exit if object has no state manager
        Stateful sm = collision.GetComponent<Stateful>();
        if (sm == null) return;

        if (Input.GetKeyDown(actionKey))
        {
            sm.switchState();
            sm.displayState();
        }
    }
}

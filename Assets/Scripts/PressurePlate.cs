using UnityEngine;
using System.Collections.Generic;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Animator animator;
    private int counter;

    private void OnTriggerEnter(Collider other)
    {
        counter++;
    }

    private void OnTriggerExit(Collider other)
    {
        counter--;
    }

    void Update()
    {
        if (counter > 0)
        {
            animator.SetBool("isOpen_Obj_1", true);
        }
        else
        {
            animator.SetBool("isOpen_Obj_1", false);
        }
    }
}

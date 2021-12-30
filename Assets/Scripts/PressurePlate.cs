using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Animator animator;
    private bool collision;

    private void OnTriggerEnter(Collider other)
    {
        collision = true;
    }

    private void OnTriggerExit(Collider other)
    {
        collision = false;
    }

    void Update()
    {
        if (collision)
        {
            animator.SetBool("isOpen_Obj_1", true);
        }
        else
        {
            animator.SetBool("isOpen_Obj_1", false);
        }
    }
}

using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] GameObject door;
    private Animator animator;
    private bool collision;

    // Use this for initialization
    void Start()
    {
        animator = door.GetComponent<Animator>();
    }

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
        Debug.Log(collision);
    }
}

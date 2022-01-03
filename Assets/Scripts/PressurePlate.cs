/// <summary>
/// AUTHOR: Tal Zichlinsky
/// 
/// This class serves as the basic operator for a pressure plate that 
/// changes the boolean state of an animator
/// 
/// </summary>
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string booleanExpression;
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
            animator.SetBool(booleanExpression, true);
        }
        else
        {
            animator.SetBool(booleanExpression, false);
        }
    }
}

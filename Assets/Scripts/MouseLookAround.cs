using UnityEngine;
using System.Collections;

public class MouseLookAround : MonoBehaviour
{
    [SerializeField] float XLookSpeed = 1f;
    [SerializeField] float YLookSpeed = 1f;

    [SerializeField] Transform PlayerBody;

    float xRotation = 0f;

    private const float MAX_Y_ROTATION = 90;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * XLookSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * YLookSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -MAX_Y_ROTATION, MAX_Y_ROTATION);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        PlayerBody.Rotate(Vector3.up*mouseX);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public bool inverted;

    public Transform playerBody;

    float xRotaion = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Camera follows the mouse, mouse is locked to center of screen when running and can't look all the way up and around
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        if (inverted)
        {
            xRotaion += mouseY;
        } else
        {
            xRotaion -= mouseY;
        }
        
        xRotaion = Mathf.Clamp(xRotaion, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotaion, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

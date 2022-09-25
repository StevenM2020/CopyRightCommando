using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensX = 1;
    [SerializeField] private float sensY = 1;

    public Transform cam;
    [SerializeField] Transform orientation;

    float mouseX = 1f;
    float mouseY = 1f;

    float multiplier = 0.8f;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        //Locks and hides cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();

        //Moves camera
        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    //A method that gets mouse input and stores it for later
    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }
}

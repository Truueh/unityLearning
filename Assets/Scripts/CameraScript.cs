using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float sensitivity = 4.0f;

    public GameObject cam;
    private float targetDistance;

    private float minTurnAngle = -45.0f;
    private float maxTurnAngle = 0.0f;
    private float rotX;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        targetDistance = Vector3.Distance(transform.position, cam.transform.position);
    }

    void Update()
    {
        // set camera distance
        if (Input.mouseScrollDelta.y < 0)
            targetDistance = Mathf.Min(targetDistance + 1, 12);
        else if (Input.mouseScrollDelta.y > 0)
            targetDistance = Mathf.Max(targetDistance - 1, 5);

        // get the mouse inputs
        float y = Input.GetAxis("Mouse X") * sensitivity;
        rotX += Input.GetAxis("Mouse Y") * sensitivity;

        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
        transform.position = cam.transform.position - (transform.forward * targetDistance);
    }
}
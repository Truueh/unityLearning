using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float sensitivity = 100;
    public GameObject player;
    public KeyCode lookAroundModeKey = KeyCode.Mouse1;

    private float targetDistance;
    private float minTurnAngle = -45.0f;
    private float maxTurnAngle = 0.0f;
    private float rotX;

    private Vector3 initialCameraRotation = Vector3.zero;
    private float initialCameraDistance;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        targetDistance = Vector3.Distance(transform.position, player.transform.position);
    }

    void Update()
    {
        // set camera distance
        if (Input.mouseScrollDelta.y < 0)
            targetDistance = Mathf.Min(targetDistance + 1, 12);
        else if (Input.mouseScrollDelta.y > 0)
            targetDistance = Mathf.Max(targetDistance - 1, 5);

        // get the mouse inputs
        float y = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        rotX += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);

        if (Input.GetKey(lookAroundModeKey))
        {
            // Save initial camera rotation
            if (initialCameraRotation == Vector3.zero)
            {
                initialCameraRotation = this.transform.eulerAngles;
                initialCameraDistance = targetDistance;
            }

            // Rotate and translate camera
            transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
            transform.position = player.transform.position - (transform.forward * targetDistance);
        }
        else
        {
            // ...
            if (initialCameraRotation != Vector3.zero)
            {
                transform.eulerAngles = initialCameraRotation;
                targetDistance = initialCameraDistance;
                initialCameraRotation = Vector3.zero;
            }

            // Rotate and translate camera
            transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y, 0);
            transform.position = player.transform.position - (transform.forward * targetDistance);

            // Rotate player
            player.transform.Rotate(Vector3.up * y);
        }
    }
}
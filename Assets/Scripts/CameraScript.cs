using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float sensitivity = 100;
    public GameObject player;
    public KeyCode lookAroundModeKey = KeyCode.Mouse1;

    private float targetDistance;
    private float minTurnAngle = -35.0f;
    private float maxTurnAngle = 10f;
    private float cameraDistanceOffset = 3;
    private float rotX;

    private bool lookAroundMode;

    ObjectSelection objSelection;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        targetDistance = Vector3.Distance(transform.position, player.transform.position);
        objSelection = player.GetComponent<ObjectSelection>();
    }

    void Update()
    {
        // set camera distance
        if (!objSelection.LockMode)
        {
            if (Input.mouseScrollDelta.y < 0)
                targetDistance = Mathf.Min(targetDistance + 1, 12);
            else if (Input.mouseScrollDelta.y > 0)
                targetDistance = Mathf.Max(targetDistance - 1, 5);
        }

        // get the mouse inputs
        float y = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        rotX += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);

        // Check current camera mode
        if (Input.GetKey(lookAroundModeKey))
        {
            lookAroundMode = true;

            // Rotate and translate camera
            transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
            transform.position = player.transform.position - (transform.forward * targetDistance) + new Vector3(transform.forward.x * cameraDistanceOffset, 0, transform.forward.z * cameraDistanceOffset);
        }
        else
        {
            if (lookAroundMode)
            {
                // Solution to look around mode:
                // get camera out of player children
                // make player look at camera
                // turn player 180 degrees
                // get camera back to player children
                transform.parent = null;
                Vector3 angles = player.transform.rotation.eulerAngles;
                player.transform.LookAt(transform);
                player.transform.rotation = Quaternion.Euler(angles.x, player.transform.eulerAngles.y, angles.z);
                transform.parent = player.transform;
                lookAroundMode = false;
            }

            // Rotate and translate camera
            transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y, 0);
            transform.position = player.transform.position - (transform.forward * targetDistance) + new Vector3(transform.forward.x * cameraDistanceOffset, 0, transform.forward.z * cameraDistanceOffset);

            // Rotate player
            player.transform.Rotate(Vector3.up * y);
        }
    }
}
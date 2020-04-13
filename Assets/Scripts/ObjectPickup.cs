using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    private ObjectSelection playerObjSelection;
    private GameObject selectedObject;
    private float distanceOffset;
    private GameObject Cam;

    private void Start()
    {
        playerObjSelection = GetComponent<ObjectSelection>();
        Cam = playerObjSelection.Cam;
        distanceOffset = 5f;
    }

    void Update()
    {
        // Assign value to target object
        selectedObject = playerObjSelection.selectedObject;

        // Mouse wheel controls for controlling distance of target distance
        if (playerObjSelection.LockMode)
        {
            if (Input.mouseScrollDelta.y > 0)
                distanceOffset = Mathf.Min(distanceOffset + 1, 12);
            else if (Input.mouseScrollDelta.y < 0)
                distanceOffset = Mathf.Max(distanceOffset - 1, 5);
        }      

        // Control target object if it exists
        if (selectedObject != null)
        {
            if (playerObjSelection.LockMode)
            {
                // Set x, y and z values for target object position (relative to player look direction & position)
                float x, y, z;

                x = transform.forward.x * distanceOffset;
                y = Cam.transform.forward.y * distanceOffset + 3;
                z = transform.forward.z * distanceOffset;

                // Lock rotation on x axis to avoid glitches
                float originalXAng = selectedObject.transform.eulerAngles.x;
                selectedObject.transform.LookAt(transform);
                selectedObject.transform.eulerAngles = new Vector3(originalXAng, selectedObject.transform.eulerAngles.y, selectedObject.transform.eulerAngles.z);

                // Set target object position
                selectedObject.transform.position = transform.position - new Vector3(x, -y, z);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    public GameObject Cam;
    [HideInInspector]
    public GameObject selectedObject;
    public bool LockMode;
    public KeyCode selectionKey = KeyCode.E;

    void Update()
    {
        if (!LockMode)
        {
            // Save raycast hit information to a variable
            RaycastHit hit;

            // If player is looking at an object
            if (Physics.Raycast(transform.position, -transform.forward + new Vector3(0, Cam.transform.forward.y + 0.1f, 0), out hit, 20f))
            {
                // Deselect objects logic
                if (selectedObject != null && hit.collider.transform.gameObject != selectedObject)
                    DeselectObject();

                // If the object is pickupable
                if (hit.collider.transform.gameObject.layer == 9)
                {
                    // save object that was hit
                    SelectObject(hit.collider.transform.gameObject);

                    // Update lock mode
                    if (Input.GetKeyDown(selectionKey) && selectedObject != null)
                        LockObject();
                }
            }
            else
            {
                if (selectedObject != null)
                    DeselectObject();
            }
        }

        if (Input.GetKeyUp(selectionKey))
            UnlockObject();
    }

    private void LockObject()
    {
        LockMode = true;
    }

    private void UnlockObject()
    {
        LockMode = false;
    }

    private void SelectObject(GameObject obj)
    {
        selectedObject = obj;

        // Show outline
        MeshRenderer renderer = selectedObject.GetComponent<MeshRenderer>();
        renderer.material.SetFloat("_OutlineWidth", 0.05f);
    }

    private void DeselectObject()
    {
        // Hide outline
        MeshRenderer renderer = selectedObject.GetComponent<MeshRenderer>();
        renderer.material.SetFloat("_OutlineWidth", 0f);
        print("test");

        selectedObject = null;
    }
}
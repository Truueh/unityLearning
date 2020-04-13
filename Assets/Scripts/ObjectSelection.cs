using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    public GameObject Cam;
    public Material SelectedObjectMaterial;
    [HideInInspector]
    public GameObject selectedObject;
    private Material originalSelectedObjectMaterial;
    public bool LockMode;

    // Update is called once per frame
    void Update()
    {
        SelectObject();
    }

    private void SelectObject()
    {
        if (!LockMode)
        {
            // Save raycast hit information to a variable
            RaycastHit hit;

            // If player is looking at an object
            if (Physics.Raycast(transform.position, -transform.forward + new Vector3(0, Cam.transform.forward.y + 0.1f, 0), out hit, 20f))
            {
                // Deselect objects logic
                if (selectedObject != null)
                {
                    if (hit.collider.transform.gameObject != selectedObject)
                    {
                        selectedObject.GetComponent<MeshRenderer>().material = originalSelectedObjectMaterial;
                    }
                }

                // If the object is pickupable
                if (hit.collider.transform.gameObject.layer == 9) // if hit object is pickupable
                {
                    // save object material
                    selectedObject = hit.collider.transform.gameObject;
                    if (selectedObject.GetComponent<MeshRenderer>().material.name != SelectedObjectMaterial.name + " (Instance)")
                        originalSelectedObjectMaterial = selectedObject.GetComponent<MeshRenderer>().material;

                    // Update selected object's properties to be selected visually
                    hit.collider.transform.gameObject.GetComponent<MeshRenderer>().material = SelectedObjectMaterial;

                    // Update lock mode
                    if (Input.GetKeyDown(KeyCode.E))
                        LockMode = true;
                }
            }
            else
            {
                // Deselect objects logic
                if (selectedObject != null)
                {
                    selectedObject.GetComponent<MeshRenderer>().material = originalSelectedObjectMaterial;
                }

                // Reset selected object
                selectedObject = null;
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
            LockMode = false;
    }
}
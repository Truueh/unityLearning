using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    public GameObject Cam;
    public Material SelectedObjectMaterial;
    private GameObject selectedObject;
    private Material originalSelectedObjectMaterial;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward + new Vector3(0, Cam.transform.forward.y + 0.1f, 0), out hit, Mathf.Infinity))
        {
            if (selectedObject != null)
            {
                if (hit.collider.transform.gameObject != selectedObject)
                {
                    selectedObject.GetComponent<MeshRenderer>().material = originalSelectedObjectMaterial;
                }
            }

            if (hit.collider.transform.gameObject.layer == 9) // if hit object is pickupable
            {
                // save object material
                selectedObject = hit.collider.transform.gameObject;
                if (selectedObject.GetComponent<MeshRenderer>().material.name != SelectedObjectMaterial.name + " (Instance)")
                    originalSelectedObjectMaterial = selectedObject.GetComponent<MeshRenderer>().material;

                hit.collider.transform.gameObject.GetComponent<MeshRenderer>().material = SelectedObjectMaterial;
            }      
        }
        else
        {
            if (selectedObject != null)
            {
                selectedObject.GetComponent<MeshRenderer>().material = originalSelectedObjectMaterial;
            }
        }
    }
}
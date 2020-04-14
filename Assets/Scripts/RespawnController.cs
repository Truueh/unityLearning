using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public enum PositionMode { initial, specific }
    public PositionMode positionMode = PositionMode.initial;

    [HideInInspector]
    public Vector3 specificPosition;
    [HideInInspector]
    public Vector3 spawnPosition;

    public GameObject boundary1;
    public GameObject boundary2;
    private Vector3 minPos;
    private Vector3 maxPos;
    public float respawnDelay = 1;
    private bool respawning;
    public AudioClip respawnSound;

    void Start()
    {
        // Take correct respawn position
        if (positionMode == PositionMode.specific)
            spawnPosition = specificPosition;
        else
            spawnPosition = transform.position;

        // Check if respawn boundaries are specified and if not set them to world boundaries
        if (boundary1 == null && boundary2 == null)
        {
            Debug.LogWarning("Respawn boundaries not specified for " + this + " world boundaries are taken by default");
            GameObject worldBoundaries = GameObject.Find("Map Boundaries");

            boundary1 = worldBoundaries.transform.Find("Boundary1").gameObject;
            boundary2 = worldBoundaries.transform.Find("Boundary2").gameObject;
        }

        // Set minimum and maximum positions
        minPos = Vector3.Min(boundary1.transform.position, boundary2.transform.position);
        maxPos = Vector3.Max(boundary1.transform.position, boundary2.transform.position);
    }
    void FixedUpdate()
    {
        // Check if position is within boundaries
        if (!InBoundaries() && !respawning)
        {
            respawning = true;

            // Delay respawn for x seconds
            StartCoroutine(DelayedRespawn());
        }
    }
    private IEnumerator DelayedRespawn()
    {
        yield return new WaitForSeconds(this.respawnDelay);

        this.respawning = false;

        if (!InBoundaries())
            this.Respawn();
    }
    public bool InBoundaries()
    {
        Vector3 currentPos = transform.position;
        return !(currentPos.x < minPos.x || currentPos.y < minPos.y || currentPos.z < minPos.z
            || currentPos.x > maxPos.x || currentPos.y > maxPos.y || currentPos.z > maxPos.z);
    }
    public void Respawn()
    {
        this.transform.position = spawnPosition;

        // Reset velocity
        ResetVelocity();

        // Play respawn sound
        if (respawnSound != null)
            AudioSource.PlayClipAtPoint(respawnSound, transform.position);
    }
    public void ResetVelocity()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            PlayerMovement pm = GetComponent<PlayerMovement>();
            pm.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollision : MonoBehaviour
{

    public GameObject player;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != player.name)
        {
            playerMovement.isGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name != player.name)
        {
            playerMovement.isGrounded = false;
        }
    }
}

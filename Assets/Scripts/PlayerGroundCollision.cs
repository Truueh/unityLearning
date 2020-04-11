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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name != player.name)
        {
            print("Enter ");
            playerMovement.isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.name != player.name)
        {
            print("Exit");
            playerMovement.isGrounded = false;
        }
    }
}

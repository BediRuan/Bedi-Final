using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination;
    public float cooldown = 0.5f; // Half a second cooldown
    private float nextTeleportTime = 0f;

    public void SetDestination(Transform newDestination)
    {
        destination = newDestination;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && destination != null && Time.time >= nextTeleportTime)
        {
            // Teleport the player to the destination portal
            other.transform.position = destination.position;
            // Set the cooldown
            nextTeleportTime = Time.time + cooldown;
            // Also set the cooldown on the destination portal to prevent immediate return teleport
            destination.GetComponent<Portal>().nextTeleportTime = Time.time + cooldown;
        }
    }
}

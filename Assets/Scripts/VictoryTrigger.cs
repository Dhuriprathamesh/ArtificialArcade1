
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    private bool victoryTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (victoryTriggered)
            return;

        PlayerMovement movement =
            other.GetComponentInParent<PlayerMovement>();

        if (movement == null)
            return;

        victoryTriggered = true;

        // Stop normal player movement.
        movement.enabled = false;

        Rigidbody playerRb =
            movement.GetComponent<Rigidbody>();

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            playerRb.isKinematic = true;
        }

        // Prevent the restart script from restarting during victory.
        PlayerCollision collision =
            movement.GetComponent<PlayerCollision>();

        if (collision != null)
            collision.enabled = false;

        // Find the character's Animator.
        Animator animator =
            movement.GetComponentInChildren<Animator>();

        if (animator != null)
        {
            animator.applyRootMotion = false;
            animator.Play("Victory", 0, 0f);
            animator.Update(0f);
        }
        else
        {
            Debug.LogWarning("Victory: No Animator found on player.");
        }

        Debug.Log("LEVEL COMPLETE!");
    }
}

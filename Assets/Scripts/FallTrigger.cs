
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FallTrigger : MonoBehaviour
{
    private FallingBlock fallingBlock;
    private bool triggered;

    private void Awake()
    {
        // Find the block this trigger belongs to.
        fallingBlock = GetComponentInParent<FallingBlock>();

        // Ensure this collider is a trigger.
        Collider triggerCollider = GetComponent<Collider>();
        triggerCollider.isTrigger = true;

        if (fallingBlock == null)
        {
            Debug.LogError(
                "FallTrigger: No FallingBlock found on this object or its parents.",
                gameObject
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || fallingBlock == null)
            return;

        // Detect the player by tag or movement component.
        bool isPlayer =
            other.CompareTag("Player") ||
            other.GetComponentInParent<PlayerMovement>() != null;

        if (!isPlayer)
            return;

        triggered = true;

        Debug.Log(
            "Trigger activated for: " + fallingBlock.gameObject.name,
            gameObject
        );

        fallingBlock.StartFalling();
    }
}

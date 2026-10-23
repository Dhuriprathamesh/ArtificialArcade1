
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallingBlock : MonoBehaviour
{
    [Header("Fall Settings")]
    public float fallSpeed = 5f;

    private Rigidbody rb;
    private bool hasFallen;
    private bool isFrozen;

    private bool savedKinematic;
    private bool savedGravity;
    private Vector3 savedVelocity;
    private Vector3 savedAngularVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Block stays suspended until its trigger activates.
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void StartFalling()
    {
        if (hasFallen)
            return;

        hasFallen = true;

        // If time is frozen, wait until it resumes.
        if (isFrozen)
            return;

        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void SetFrozen(bool freeze)
    {
        if (isFrozen == freeze)
            return;

        isFrozen = freeze;

        if (freeze)
        {
            savedKinematic = rb.isKinematic;
            savedGravity = rb.useGravity;
            savedVelocity = rb.linearVelocity;
            savedAngularVelocity = rb.angularVelocity;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        else
        {
            // Resume falling only if this block was activated.
            if (hasFallen)
            {
                rb.isKinematic = savedKinematic && !hasFallen;
                rb.useGravity = savedGravity || hasFallen;

                if (!rb.isKinematic)
                {
                    rb.linearVelocity = savedVelocity;
                    rb.angularVelocity = savedAngularVelocity;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (hasFallen && !isFrozen && rb.linearVelocity.y < -fallSpeed)
        {
            Vector3 velocity = rb.linearVelocity;
            velocity.y = -fallSpeed;
            rb.linearVelocity = velocity;
        }
    }
}

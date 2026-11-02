
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class GravityShift : MonoBehaviour
{
    [Header("Gravity Settings")]
    public float gravityStrength = 9.81f;
    public float rotationSpeed = 360f;

    private Rigidbody rb;

    // -2 = upside down from the left
    // -1 = gravity left
    //  0 = normal gravity
    //  1 = gravity right
    //  2 = upside down from the right
    private int gravityStep = 0;

    private Quaternion targetRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // We apply gravity manually to this player.
        rb.useGravity = false;

        targetRotation = rb.rotation;
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            // Move one 90-degree step right.
            if (gravityStep < 2)
                gravityStep++;

            UpdateTargetRotation();
        }
        else if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            // Move one 90-degree step left.
            if (gravityStep > -2)
                gravityStep--;

            UpdateTargetRotation();
        }
    }

    private void UpdateTargetRotation()
    {
        targetRotation = Quaternion.Euler(
            0f, 0f, -gravityStep * 90f
        );
    }

    private void FixedUpdate()
    {
        // Rotate the player toward the target orientation.
        Quaternion nextRotation = Quaternion.RotateTowards(
            rb.rotation,
            targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        );

        rb.MoveRotation(nextRotation);

        // Apply gravity in the selected direction.
        Vector3 gravityDirection;

        switch (gravityStep)
        {
            case 1:
                gravityDirection = Vector3.right;
                break;

            case 2:
            case -2:
                gravityDirection = Vector3.up;
                break;

            case -1:
                gravityDirection = Vector3.left;
                break;

            default:
                gravityDirection = Vector3.down;
                break;
        }

        rb.AddForce(
            gravityDirection * gravityStrength,
            ForceMode.Acceleration
        );
    }
}

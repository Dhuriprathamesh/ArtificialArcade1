
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 1.5f;
    public float jumpForce = 4.5f;

    [Header("Ground Detection")]
    public float groundNormalThreshold = 0.5f;

    private Rigidbody rb;
    private CapsuleCollider capsule;
    private Animator animator;

    private float moveInput;
    private bool jumpRequested;
    private bool isGrounded;

    private readonly HashSet<Collider> groundSurfaces =
        new HashSet<Collider>();

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();

        animator = GetComponentInChildren<Animator>();

        rb.constraints |= RigidbodyConstraints.FreezePositionZ;
        rb.constraints |= RigidbodyConstraints.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        moveInput = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed)
                moveInput -= 1f;

            if (Keyboard.current.dKey.isPressed ||
                Keyboard.current.rightArrowKey.isPressed)
                moveInput += 1f;

            // Jump is accepted only while grounded.
            if (Keyboard.current.spaceKey.wasPressedThisFrame &&
                isGrounded)
            {
                jumpRequested = true;
            }
        }

        UpdateAnimations();
    }

    void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveInput * moveSpeed;
        rb.linearVelocity = velocity;

        if (jumpRequested && isGrounded)
        {
            // Clear ground contact before applying the jump.
            isGrounded = false;
            groundSurfaces.Clear();
            jumpRequested = false;

            velocity = rb.linearVelocity;
            velocity.y = 0f;
            rb.linearVelocity = velocity;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            jumpRequested = false;
        }

        UpdateAnimations();
    }

    void UpdateAnimations()
    {
        if (animator == null)
            return;

        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetBool("IsGrounded", isGrounded);

        bool falling =
            !isGrounded && rb.linearVelocity.y < -0.1f;

        animator.SetBool("IsFalling", falling);
    }

    void OnCollisionStay(Collision collision)
    {
        bool touchingGround = false;

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > groundNormalThreshold)
            {
                touchingGround = true;
                break;
            }
        }

        if (touchingGround)
            groundSurfaces.Add(collision.collider);
        else
            groundSurfaces.Remove(collision.collider);

        isGrounded = groundSurfaces.Count > 0;
    }

    void OnCollisionExit(Collision collision)
    {
        groundSurfaces.Remove(collision.collider);
        isGrounded = groundSurfaces.Count > 0;
    }
}

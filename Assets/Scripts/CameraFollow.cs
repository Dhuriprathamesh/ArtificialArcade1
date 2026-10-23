
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0f, 3f, -10f);
    public float smoothSpeed = 5f;

    [Header("Follow Options")]
    public bool followX = true;
    public bool followY = true;

    private float fixedZ;

    void Start()
    {
        fixedZ = transform.position.z;

        if (target != null)
        {
            // Keep the initial camera offset from the player.
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = transform.position;

        if (followX)
            desiredPosition.x = target.position.x + offset.x;

        if (followY)
            desiredPosition.y = target.position.y + offset.y;

        desiredPosition.z = fixedZ;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}

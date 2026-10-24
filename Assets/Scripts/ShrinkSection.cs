
using UnityEngine;
using UnityEngine.InputSystem;

public class ShrinkSection : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;
    public float interactionRange = 3f;

    [Header("Shrink Settings")]
    public float animationSpeed = 5f;

    private Transform[] blocks;
    private Vector3[] originalScales;
    private Collider[][] blockColliders;
    private bool[] isShrunk;

    private void Start()
    {
        int count = transform.childCount;

        blocks = new Transform[count];
        originalScales = new Vector3[count];
        blockColliders = new Collider[count][];
        isShrunk = new bool[count];

        for (int i = 0; i < count; i++)
        {
            blocks[i] = transform.GetChild(i);
            originalScales[i] = blocks[i].localScale;
            blockColliders[i] =
                blocks[i].GetComponentsInChildren<Collider>(true);
        }
    }

    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            int nearestIndex = FindNearestBlock();

            if (nearestIndex != -1)
            {
                isShrunk[nearestIndex] = !isShrunk[nearestIndex];

                foreach (Collider col in blockColliders[nearestIndex])
                {
                    if (col != null)
                        col.enabled = !isShrunk[nearestIndex];
                }
            }
        }

        for (int i = 0; i < blocks.Length; i++)
        {
            Vector3 targetScale = isShrunk[i]
                ? Vector3.zero
                : originalScales[i];

            blocks[i].localScale = Vector3.MoveTowards(
                blocks[i].localScale,
                targetScale,
                animationSpeed * Time.deltaTime
            );
        }
    }

    private int FindNearestBlock()
    {
        if (player == null)
            return -1;

        int nearestIndex = -1;
        float nearestDistance = interactionRange;

        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i] == null)
                continue;

            float distance = Vector3.Distance(
                player.position,
                blocks[i].position
            );

            if (distance <= nearestDistance)
            {
                nearestDistance = distance;
                nearestIndex = i;
            }
        }

        return nearestIndex;
    }
}


using UnityEngine;
using UnityEngine.InputSystem;

public class TimeFreeze : MonoBehaviour
{
    [Header("Time Freeze Settings")]
    public Key freezeKey = Key.F;

    public bool IsTimeFrozen { get; private set; }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current[freezeKey].wasPressedThisFrame)
        {
            ToggleTimeFreeze();
        }
    }

    void ToggleTimeFreeze()
    {
        IsTimeFrozen = !IsTimeFrozen;

        // Tell all falling blocks to freeze or resume.
        FallingBlock[] blocks = FindObjectsByType<FallingBlock>(
            FindObjectsSortMode.None);

        foreach (FallingBlock block in blocks)
        {
            block.SetFrozen(IsTimeFrozen);
        }

        Debug.Log(IsTimeFrozen ? "TIME FROZEN" : "TIME RESUMED");
    }
}

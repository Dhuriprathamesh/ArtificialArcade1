
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLineTrigger : MonoBehaviour
{
    [Header("Finish UI")]
    [SerializeField] private GameObject finishPanel;

    [Header("Exit Settings")]
    [SerializeField] private string menuSceneName = "MainMenu";

    private bool hasFinished;

    private void Start()
    {
        Time.timeScale = 1f;

        if (finishPanel != null)
            finishPanel.SetActive(false);
        else
            Debug.LogError("Finish Panel is not assigned!", this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasFinished || !other.CompareTag("Player"))
            return;

        hasFinished = true;

        if (finishPanel != null)
            finishPanel.SetActive(true);

        // Stop gameplay while leaving the UI visible.
        Time.timeScale = 0f;
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

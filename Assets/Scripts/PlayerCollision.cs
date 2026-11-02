
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCollision : MonoBehaviour
{
    [Header("Fall Detection")]
    [SerializeField] private float voidY = -10f;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Menu Settings")]
    [SerializeField] private string menuSceneName = "MainMenu";

    private bool isGameOver;

    private void Awake()
    {
        // Make sure gameplay is not paused when a level loads.
        Time.timeScale = 1f;
        isGameOver = false;
    }

    private void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        else
        {
            Debug.LogError(
                "Game Over Panel is not assigned in PlayerCollision!",
                this
            );
        }
    }

    private void Update()
    {
        if (isGameOver)
            return;

        if (transform.position.y < voidY)
        {
            ShowGameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGameOver)
            return;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ShowGameOver();
        }
    }

    private void ShowGameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Freeze gameplay while keeping the UI visible.
        Time.timeScale = 0f;
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}

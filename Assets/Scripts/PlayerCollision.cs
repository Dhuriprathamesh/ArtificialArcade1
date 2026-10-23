
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public float voidY = -3.5f;

    private bool restarting;

    private void Update()
    {
        // Restart if the player falls below the level.
        if (transform.position.y < voidY)
        {
            RestartLevel();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            RestartLevel();
        }
    }

    private void RestartLevel()
    {
        if (restarting)
            return;

        restarting = true;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}

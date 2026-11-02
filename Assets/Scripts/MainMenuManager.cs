
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

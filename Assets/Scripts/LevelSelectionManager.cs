
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionManager : MonoBehaviour
{
    public void OpenLevel1()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void OpenLevel2()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void OpenLevel3()
    {
        SceneManager.LoadScene("Level_3");
    }
}


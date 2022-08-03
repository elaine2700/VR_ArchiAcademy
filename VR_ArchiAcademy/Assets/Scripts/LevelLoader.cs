using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void GoToScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}

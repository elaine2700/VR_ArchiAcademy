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
        // todo ask if it is okay to quit.
        Application.Quit();
    }
}

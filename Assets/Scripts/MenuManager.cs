using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // -------------------------------
    // Load scene by name
    // -------------------------------
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // -------------------------------
    // Reload current scene
    // -------------------------------
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // -------------------------------
    // Quit game (works in build)
    // -------------------------------
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit"); // Only visible in editor
    }
}

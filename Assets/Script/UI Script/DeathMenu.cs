using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject DeathScreen;

    public void Retry()
    {
        DeathScreen.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Lobby");
        Debug.Log("Going to room scene");
    }
    public void QuitApp()
    {
        //Application.Quit();
        Debug.Log("Quit App");
    }
}

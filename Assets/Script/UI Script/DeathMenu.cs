using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void MainMenu()
    {
        //SceneManager.LoadScene("RoomScene");
        Debug.Log("Going to room scene");
    }
    public void QuitApp()
    {
        //Application.Quit();
        Debug.Log("Quit App");
    }
}

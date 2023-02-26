using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class DeathMenu : MonoBehaviour
{
    public GameObject DeathScreen;

    public void Retry()
    {
        DeathScreen.SetActive(false);
    }
    public void MainMenu()
    {
        //PhotonNetwork.LeaveRoom();
        //PhotonNetwork.LeaveLobby();
        PhotonNetwork.LoadLevel(0);
        Debug.Log("Going to room scene");
    }
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Quit App");
    }
}

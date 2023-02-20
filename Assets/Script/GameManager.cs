using UnityEngine;

using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{    
    public GameObject playerPrefab;

    void SpawnPlayer()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0.0f, 0.5f, 0.0f), Quaternion.identity);
    }

    void Start()
    {
        if (playerPrefab == null)
            return;

        SpawnPlayer();
    }
}

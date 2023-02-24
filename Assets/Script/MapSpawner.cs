using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MapSpawner : MonoBehaviour
{
    public GameObject startingRoom;
    GameObject Map;
    private bool mapSpawned = false;
    private void Update()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if (mapSpawned == false)
            {
                Map = PhotonNetwork.Instantiate("startingRoom", this.transform.position, Quaternion.identity);
                Map.transform.SetParent(this.transform);
                mapSpawned = true;
            }
        }
    }
    public void RegenerateMap()
    {
        Destroy(Map);
        mapSpawned = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
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
                Map = Instantiate(startingRoom, this.transform.position, Quaternion.identity, this.transform);
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

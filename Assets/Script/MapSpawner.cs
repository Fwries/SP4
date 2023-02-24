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
            //if (mapSpawned == false)
            //{
            //    Map = PhotonNetwork.Instantiate("startingRoom", transform.position, Quaternion.identity);
            //    Map.transform.SetParent(transform);
            //    mapSpawned = true;
            //}
            if (mapSpawned == false)
            {
                Map = PhotonNetwork.Instantiate("startingRoom", transform.position, Quaternion.identity);
                Map.transform.SetParent(transform);
                GetComponent<PhotonView>().RPC("SetRoomChild", RpcTarget.Others, Map.GetComponent<PhotonView>().ViewID, this.GetComponent<PhotonView>().ViewID);
                mapSpawned = true;
            }
        }
    }
    public void RegenerateMap()
    {
        PhotonNetwork.Destroy(Map);
        mapSpawned = false;
    }

    [PunRPC]
    public void SetRoomChild(int childID, int parentID)
    {
        GameObject Parent = PhotonView.Find(parentID).gameObject;
        GameObject Child = PhotonView.Find(childID).gameObject;
        Child.transform.SetParent(Parent.transform);
    }
}

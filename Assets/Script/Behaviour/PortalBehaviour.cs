using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PortalBehaviour : MonoBehaviour
{
    public RoomTemplates Map;
    public GameObject reference;
    private void Awake()
    {
        reference = GameObject.Find("RoomTemplates");
        Map = reference.GetComponent<RoomTemplates>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (other.tag == "Player")
            {
                Debug.Log("Enter Portal");
                Map.NewMap();
                List<GameObject> playerList = new List<GameObject>();
                int i = 1;
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject player in players)
                {
                    player.transform.position = new Vector3(0, 0, 0);
                    i++;
                }
            }
        }
    }
}

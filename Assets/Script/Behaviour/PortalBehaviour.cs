using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (other.tag == "Player")
        {
            Debug.Log("Enter Portal");
            Map.NewMap();
            List<GameObject> playerList = new List<GameObject>();
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            int i = 1;
            foreach (GameObject player in players)
            {
                player.transform.position = new Vector3(i*10, 0, 0);
                i++;
            }
        }
    }
}

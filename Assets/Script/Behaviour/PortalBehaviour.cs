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
        if (other.name == "Player")
        {
            Map.NewMap();
        }
    }
}

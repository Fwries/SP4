using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public GameObject startingRoom;
    private void Awake()
    {
        Instantiate(startingRoom, this.transform.position, Quaternion.identity);
    }
}

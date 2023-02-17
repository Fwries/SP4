using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public GameObject startingRoom;
    GameObject Map;
    private bool mapSpawned = false;
    private void Update()
    {
        if (mapSpawned == false)
        {
            Map = Instantiate(startingRoom, this.transform.position, Quaternion.identity, this.transform);
            mapSpawned = true;
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Destroy(Map);
            mapSpawned = false;
        }
    }
}

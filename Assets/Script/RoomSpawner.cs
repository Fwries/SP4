using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int DoorDirection;
    /*=====================
     1-Need bottom door
     2-Need left door
     3-Need top door
     4-Need right door
    =======================*/

    private RoomTemplates Templates;
    private int rand;
    private bool spawned = false;

    private void Start()
    {
        Templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn",0.1f);
    }

    void Spawn()
    {
        if (spawned == false)
        {
            if (DoorDirection == 1)
            {
                rand = Random.Range(0, Templates.BottomRooms.Length);
                Instantiate(Templates.BottomRooms[rand], transform.position, Templates.BottomRooms[rand].transform.rotation);
            }
            else if (DoorDirection == 2)
            {
                rand = Random.Range(0, Templates.LeftRooms.Length);
                Instantiate(Templates.LeftRooms[rand], transform.position, Templates.LeftRooms[rand].transform.rotation);
            }
            else if (DoorDirection == 3)
            {
                rand = Random.Range(0, Templates.TopRooms.Length);
                Instantiate(Templates.TopRooms[rand], transform.position, Templates.TopRooms[rand].transform.rotation);
            }
            else if (DoorDirection == 4)
            {
                rand = Random.Range(0, Templates.RightRooms.Length);
                Instantiate(Templates.RightRooms[rand], transform.position, Templates.RightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SpawnPoint")&&other.GetComponent<RoomSpawner>().spawned==true)
        {
            Destroy(gameObject);
        }
    }
}

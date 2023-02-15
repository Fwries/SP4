using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] TopRooms;
    public GameObject[] BottomRooms;
    public GameObject[] LeftRooms;
    public GameObject[] RightRooms;
    public GameObject[] RoomDecor;

    public List<GameObject> EnemySetsEasy;

    public GameObject SecretRoom;

    public List<GameObject> Rooms;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;

    private void Update()
    {
        if (waitTime <= 0 && spawnedBoss == false)
        {
            //Instantiate(boss, Rooms[Rooms.Count - 1].transform.position, Quaternion.identity);
            spawnedBoss = true;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}

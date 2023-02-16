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
    public GameObject[] boss;

    public GameObject[] EnemySetsEasy;

    public GameObject SecretRoom;

    public List<GameObject> Rooms;

    public float waitTime;
    private bool spawnedBoss;

    int rand;
    private void Update()
    {
        if (waitTime <= 0 && spawnedBoss == false)
        {
            rand = Random.Range(0, boss.Length);
            Instantiate(boss[rand], Rooms[Rooms.Count - 1].transform.position, Quaternion.identity);
            spawnedBoss = true;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}

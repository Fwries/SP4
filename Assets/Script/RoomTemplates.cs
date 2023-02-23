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
    public GameObject Map;
    public GameObject LoadScreen;

    private const float m_ROOM_SIZE = 17.0f;
    private float m_MapScaleZ = 0.0f;
    private float m_MapScaleX = 0.0f;

    private bool spawnedBoss;
    private bool spawned;
    private bool MapSpawned;

    private int rand;
    private int enemyRand;

    public void AddMapScaleZ()
    {
        m_MapScaleZ += m_ROOM_SIZE;
    }

    public void AddMapScaleX()
    {
        m_MapScaleX += m_ROOM_SIZE;
    }

    private void Update()
    {
        if (waitTime <= 0 && spawned == false)
        {
            for (int i = 0; i < Rooms.Count; i++)
            {
                enemyRand = Random.Range(0, EnemySetsEasy.Length);
                rand = Random.Range(0, 10);
                if (rand >= 8 && Rooms[i] != Rooms[Rooms.Count-1])
                {
                    Instantiate(RoomDecor[0], Rooms[i].transform.position, Rooms[i].transform.rotation, Rooms[i].transform);
                }
                else
                {
                    if (Rooms[i] != Rooms[Rooms.Count - 1])
                    {
                        rand = Random.Range(1, RoomDecor.Length);
                        Instantiate(RoomDecor[rand], Rooms[i].transform.position, Rooms[i].transform.rotation, Rooms[i].transform);
                        Instantiate(EnemySetsEasy[enemyRand], Rooms[i].transform.position, Quaternion.identity, Rooms[i].transform);
                    }
                }
            }
            spawned = true;
        }
        if (waitTime <= 0 && spawnedBoss == false)
        {
            rand = Random.Range(0, boss.Length);
            Instantiate(boss[rand], Rooms[Rooms.Count - 1].transform.position, Quaternion.identity, Rooms[Rooms.Count - 1].transform);
            spawnedBoss = true;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }

        if (waitTime <= 0 && Rooms.Count <= 10)
        {
            NewMap();
        }
        if (Rooms.Count >= 10)
        {
            MapSpawned = true;
        }
        if (MapSpawned == true && waitTime <= 0)
        {
            LoadScreen.SetActive(false);
        }
        else
        {
            LoadScreen.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            Map.GetComponent<MapSpawner>().RegenerateMap();
        }
    }

    public void NewMap()
    {
        spawnedBoss = false;
        spawned = false;
        waitTime = 4;
        MapSpawned = false;
        Rooms.Clear();
        Map.GetComponent<MapSpawner>().RegenerateMap();
    }
}

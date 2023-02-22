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
    private bool spawned;
    public GameObject Map;

    int rand;
    int enemyRand;
    private void Update()
    {
        if(waitTime<=0&&spawned==false)
        {
            for(int i=0;i<Rooms.Count;i++)
            {
                enemyRand = Random.Range(0, EnemySetsEasy.Length);
                rand = Random.Range(0, 10);
                if (rand >= 8)
                {
                    GameObject NewDecor = Instantiate(RoomDecor[0], Rooms[i].transform.position, Rooms[i].transform.rotation);
                    NewDecor.transform.SetParent(this.transform);
                }
                else
                {
                    rand = Random.Range(1, RoomDecor.Length);
                    GameObject NewDecor = Instantiate(RoomDecor[rand], Rooms[i].transform.position, Rooms[i].transform.rotation);
                    NewDecor.transform.SetParent(this.transform);
                    GameObject enemies = Instantiate(EnemySetsEasy[enemyRand], Rooms[i].transform.position, Quaternion.identity);
                    enemies.transform.SetParent(this.transform);
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

        if (spawnedBoss == true && Rooms.Count < 10)
        {
            spawnedBoss = false;
            spawned = false;
            waitTime = 4;
            Rooms.Clear();
            Map.GetComponent<MapSpawner>().RegenerateMap();
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            Map.GetComponent<MapSpawner>().RegenerateMap();
        }
    }
}

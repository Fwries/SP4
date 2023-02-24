using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    public Sprite bossSprite;
    
    private const float m_ROOM_SIZE = 17.0f;
    private Vector2 m_MinBound;
    private Vector2 m_MaxBound;

    private bool spawnedBoss;
    private bool spawned;
    private bool MapSpawned;

    private int rand;
    private int enemyRand;
    private PhotonView photonView;

    private void Awake()
    {
        photonView = transform.GetComponent<PhotonView>();
    }

    public Vector2 GetMinBound()
    {
        return m_MinBound;
    }

    public void SetMinBound(float x = float.NaN, float y = float.NaN)
    {
        if (!float.IsNaN(x))
            m_MinBound.x = x;
        if (!float.IsNaN(y))
            m_MinBound.y = y;
    }

    public Vector2 GetMaxBound()
    {
        return m_MaxBound;
    }

    public void SetMaxBound(float x = float.NaN, float y = float.NaN)
    {
        if (!float.IsNaN(x))
            m_MaxBound.x = x;
        if (!float.IsNaN(y))
            m_MaxBound.y = y;
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

                if (i == Rooms.Count - 1)
                    GameObject.FindGameObjectWithTag("MinimapCamera").GetComponent<MinimapCameraAdjuster>()
                                                                     .AdjustMinimapCamera(m_MinBound, m_MaxBound, m_ROOM_SIZE / 2.0f);
            }
            spawned = true;
        }
        if (waitTime <= 0 && spawnedBoss == false)
        {
            rand = Random.Range(0, boss.Length);
            Transform lastRoomTransform = Rooms[Rooms.Count - 1].transform;
            Instantiate(boss[rand], lastRoomTransform.position, Quaternion.identity, lastRoomTransform);
            lastRoomTransform.Find("MinimapObjects").Find("RoomType").GetComponent<SpriteRenderer>().sprite = bossSprite;
            spawnedBoss = true;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }

        if (waitTime <= 0 && Rooms.Count < 10)
        {
            NewMap();
        }
        if (Rooms.Count >= 10)
        {
            MapSpawned = true;
        }
        if (MapSpawned == true && waitTime <= 0)
        {
            photonView.RPC("LoadScreenCheck", RpcTarget.AllViaServer, MapSpawned);
        }
        else
        {
            photonView.RPC("LoadScreenCheck", RpcTarget.AllViaServer, MapSpawned);
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

    [PunRPC]

    void LoadScreenCheck(bool mapSpawned)
    {
        LoadScreen.SetActive(!mapSpawned);
    }
}

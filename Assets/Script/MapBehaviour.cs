using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    public GameObject TilePrefab;
    public GameObject WallPrefab;
    public ScTile[] Tileset;

    public int SpawnX, SpawnY;

    [HideInInspector] public int[,] BaseLayer;
    [HideInInspector] public int[,] SecondLayer;

    [HideInInspector] public bool[,] SolidTileMap;
    [HideInInspector] public bool[,] InteractableTileMap;
    [HideInInspector] public bool[,] EventTileMap;

    // Start is called before the first frame update
    void Start()
    {
        ChangeMap();
        SpawnMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeMap()
    {
        int[,] NewMap = new int[70,70];
        int[,] TLRB = {
            { 0, 2, 2, 1, 2, 2, 0},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 1, 1, 1, 1, 1, 1, 1},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 0, 2, 2, 1, 2, 2, 0}
        };
        int[,] B = {
            { 0, 2, 2, 2, 2, 2, 0},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 0, 2, 2, 1, 2, 2, 0}
        };
        int[,] T = {
            { 0, 2, 2, 1, 2, 2, 0},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 0, 2, 2, 2, 2, 2, 0}
        };
        int[,] L = {
            { 0, 2, 2, 2, 2, 2, 0},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 1, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 0, 2, 2, 2, 2, 2, 0}
        };
        int[,] R = {
            { 0, 2, 2, 2, 2, 2, 0},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 1},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 0, 2, 2, 2, 2, 2, 0}
        };
        int[,] TB = {
            { 0, 2, 2, 1, 2, 2, 0},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 0, 2, 2, 1, 2, 2, 0}
        };
        int[,] LR = {
            { 0, 2, 2, 2, 2, 2, 0},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 1, 1, 1, 1, 1, 1, 1},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 0, 2, 2, 2, 2, 2, 0}
        };
        int[,] TL = {
            { 0, 2, 2, 1, 2, 2, 0},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 1, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 0, 2, 2, 2, 2, 2, 0}
        };
        int[,] TR= {
            { 0, 2, 2, 1, 2, 2, 0},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 1},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 0, 2, 2, 2, 2, 2, 0}
        };
        int[,] BL = {
            { 0, 2, 2, 2, 2, 2, 0},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 1, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 0, 2, 2, 1, 2, 2, 0}
        };
        int[,] BR = {
            { 0, 2, 2, 2, 2, 2, 0},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 1},
            { 2, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 2},
            { 0, 2, 2, 1, 2, 2, 0}
        };
        //NewMap = StartingRoom;
        //=========================================
        // Define the positions of each room on the map
        List<int[,]> roomT = new List<int[,]> { TLRB, T, TB, TR, TL};
        List<int[,]> roomB = new List<int[,]> { TLRB, B, TB, BR, BL};
        List<int[,]> roomL = new List<int[,]> { TLRB, L, LR, TL, TB};
        List<int[,]> roomR = new List<int[,]> { TLRB, R, LR, TR, BR};
        List<int[]> roomPositions = new List<int[]> { };

        for (int i = 0; i < (NewMap.GetLength(0) / TLRB.GetLength(0)); i++)
        {
            for (int j = 0; j < (NewMap.GetLength(0) / TLRB.GetLength(0)); j++)
            {
                roomPositions.Add(new int[] { i * TLRB.GetLength(0), j * TLRB.GetLength(0) });
            }
        }
        int rand = UnityEngine.Random.Range(0, NewMap.GetLength(0));
        int[] StartingPos = roomPositions[rand];
        int[,] StartingRoom = new int [TLRB.GetLength(0), TLRB.GetLength(0)];
        rand = UnityEngine.Random.Range(1, roomB.Count);
        if (StartingPos[0] != 0 || StartingPos[0] != NewMap.GetLength(0)-TLRB.GetLength(0))
        {
            StartingRoom = roomB[rand];
        }
        else if (StartingPos[0] == 0)
        {
            while(rand==3)
            {
                rand = UnityEngine.Random.Range(1, roomB.Count);
            }
            StartingRoom = roomB[rand];
        }
        else if (StartingPos[0] == NewMap.GetLength(0) - TLRB.GetLength(0))
        {
            while (rand == 4)
            {
                rand = UnityEngine.Random.Range(1, roomB.Count);
            }
            StartingRoom = roomB[rand];
        }
        for (int x = 0; x < StartingRoom.GetLength(0); x++)
        {
            for (int y = 0; y < StartingRoom.GetLength(1); y++)
            {
                int mapX = StartingPos[0] + x;
                int mapY = StartingPos[1] + y;
                NewMap[mapX, mapY] = StartingRoom[x, y];
            }
        }

        // Iterate over the rooms and place them on the map
        /*for (int i = 0; i < roomPositions.Count; i++)
        {
            int rand = UnityEngine.Random.Range(0, roomConfigs.Count);
            int[,] roomConfig = roomConfigs[rand];
            int[] roomPosition = roomPositions[i];

            // Check if the room overlaps with any other room
            bool overlaps = false;
            for (int x = 0; x < roomConfig.GetLength(0); x++)
            {
                for (int y = 0; y < roomConfig.GetLength(1); y++)
                {
                    int mapX = roomPosition[0] + x;
                    int mapY = roomPosition[1] + y;
                    if (mapX >= NewMap.GetLength(0) || mapY >= NewMap.GetLength(1) || NewMap[mapX, mapY] != 0)
                    {
                        overlaps = true;
                        break;
                    }
                }
                if (overlaps)
                {
                    break;
                }
            }

            // If the room does not overlap, place it on the map
            if (!overlaps)
            {
                for (int x = 0; x < roomConfig.GetLength(0); x++)
                {
                    for (int y = 0; y < roomConfig.GetLength(1); y++)
                    {
                        int mapX = roomPosition[0] + x;
                        int mapY = roomPosition[1] + y;
                        NewMap[mapX, mapY] = roomConfig[x, y];
                    }
                }
            }
        }*/
        //=========================================
        BaseLayer = new int[NewMap.GetLength(0), NewMap.GetLength(1)];
        SecondLayer = new int[NewMap.GetLength(0), NewMap.GetLength(1)];

        for (int y = 0; y < NewMap.GetLength(0); y++)
        {
            for (int x = 0; x < NewMap.GetLength(1); x++)
            {
                BaseLayer[NewMap.GetLength(0) - 1 - y, x] = NewMap[y, x];
            }
        }

        SolidTileMap = new bool[BaseLayer.GetLength(0), BaseLayer.GetLength(1)];
        InteractableTileMap = new bool[BaseLayer.GetLength(0), BaseLayer.GetLength(1)];
        EventTileMap = new bool[BaseLayer.GetLength(0), BaseLayer.GetLength(1)];

        for (int y = 0; y < BaseLayer.GetLength(0); y++)
        {
            for (int x = 0; x < BaseLayer.GetLength(1); x++)
            {
                if (BaseLayer[y, x] != 0)
                {
                    SolidTileMap[y, x] = Tileset[BaseLayer[y, x]].Solid;
                    InteractableTileMap[y, x] = Tileset[BaseLayer[y, x]].Interactable;
                    EventTileMap[y, x] = Tileset[BaseLayer[y, x]].Event;
                }
                else
                {
                    SolidTileMap[y, x] = true;
                }
            }
        }
    }

    string CheckSide(int y, int x, int Type)
    {
        char[] ReturnValue = { ' ', ' ', ' ', ' ' };

        bool[][,] CheckType = new bool[3][,];
        CheckType[0] = SolidTileMap;
        CheckType[1] = InteractableTileMap;
        CheckType[2] = EventTileMap;

        if (y + 1 < CheckType[Type].GetLength(0)) // UP
        {
            if (CheckType[Type][y + 1, x])
            {
                ReturnValue[0] = 'U';
            }
            else
            {
                ReturnValue[0] = 'F';
            }
        }
        if (y > 0) // DOWN
        {
            if (CheckType[Type][y - 1, x])
            {
                ReturnValue[1] = 'D';
            }
            else
            {
                ReturnValue[1] = 'F';
            }
        }
        if (x > 0) // LEFT
        {
            if (CheckType[Type][y, x - 1])
            {
                ReturnValue[2] = 'L';
            }
            else
            {
                ReturnValue[2] = 'F';
            }
        }
        if (x + 1 < CheckType[Type].GetLength(1)) // RIGHT
        {
            if (CheckType[Type][y, x + 1])
            {
                ReturnValue[3] = 'R';
            }
            else
            {
                ReturnValue[3] = 'F';
            }
        }
        return new string(ReturnValue);
    }

    void SpawnMap()
    {
        for (int y = 0; y < BaseLayer.GetLength(0); y++)
        {
            for (int x = 0; x < BaseLayer.GetLength(1); x++)
            {
                if (BaseLayer[y, x] != 0)
                {
                    Vector3 Pos = new Vector3(x, 0, y);
                    Vector3 Rotate = new Vector3(0, 0, 0);

                    if (SolidTileMap[y, x])
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            GameObject TempWall;
                            TempWall = Instantiate(WallPrefab, new Vector3(x, 0.5f + i, y), Quaternion.identity);
                            TempWall.GetComponent<MeshRenderer>().material = Tileset[BaseLayer[y, x]].TileTexture;
                            TempWall.transform.SetParent(this.transform);
                        }
                    }
                    else
                    {
                        GameObject TempTile = Instantiate(TilePrefab, Pos, Quaternion.identity);
                        TempTile.GetComponent<MeshRenderer>().material = Tileset[BaseLayer[y, x]].TileTexture;
                        TempTile.transform.SetParent(this.transform);
                    }
                }
            }
        }
    }
}

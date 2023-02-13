using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    public GameObject TilePrefab;
    public GameObject WallPrefab;
    public Tile[] Tileset;

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
        int[,] NewMap = {
            { 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
            { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
            { 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2}
        };

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
                        for (int i = 0; i < 3; i++)
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
                        Debug.Log(Tileset[BaseLayer[y, x]]);
                        TempTile.transform.SetParent(this.transform);
                    }
                }
            }
        }
    }
}

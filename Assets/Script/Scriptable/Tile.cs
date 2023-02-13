using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Dungeon Hurler/Tile")]
public class Tile : ScriptableObject
{
    public Material TileTexture;
    public bool Solid;
    public bool Interactable;
    //public TileInteractable InteractScript;
    public bool Event;
    //public TileEvent EventScript;
}

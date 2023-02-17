using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rariety List", menuName = "Dungeon Hurler/Rariety List")]
public class ScRarietyList : ScriptableObject
{
    public GameObject[] Common;
    public GameObject[] Rare;
    public GameObject[] Epic;
    public GameObject[] Legendary;
}

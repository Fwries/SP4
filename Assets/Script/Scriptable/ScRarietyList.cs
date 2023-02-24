using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Rariety List", menuName = "Dungeon Hurler/Rariety List")]
[Serializable]
public class ScRarietyList : ScriptableObject
{
    public GameObject[] Common;
    public GameObject[] Rare;
    public GameObject[] Epic;
    public GameObject[] Legendary;
}

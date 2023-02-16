using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Dungeon Hurler/Weapon")]
public class ScWeapon : ScriptableObject
{
    public GameObject Prefab;
    public string Name;
    public string Description;
    public float AtkSpeed;
    public int AtkReach;
    public int Area1;
    public int Area2;
    public int Weight;

    public float OffsetX;
    public float OffsetY;
}

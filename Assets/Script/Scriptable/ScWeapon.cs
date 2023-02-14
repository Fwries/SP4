using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Dungeon Hurler/Weapon")]
public class ScWeapon : ScriptableObject
{
    public Sprite WeaponIcon;
    public string Description;
    public int Damage;
    public float AtkSpeed;
    public int AtkReach;
    public int Weight;
}

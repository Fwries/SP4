using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Dungeon Hurler/Weapon")]
public class ScWeapon : ScriptableObject
{
    public Sprite WeaponIcon;
    public string Weapon;
    public string Description;
    public int RedDmg;
    public int OrangeDmg;
    public int YellowDmg;
    public float AtkSpeed;
    public int AtkReach;
    public int Area1;
    public int Area2;
    public int Weight;
}

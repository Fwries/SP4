using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Dungeon Hurler/Weapon")]
public class ScWeapon : ScriptableObject
{
    public enum TypeEnum
    {
        None,
        Swing = 1,
        Stab = 2,
        Crush = 3,
    };

    public GameObject Prefab;
    public Sprite WeaponIcon;
    public string Name;
    public string Description;

    public TypeEnum AtkType = TypeEnum.Swing;

    public float AtkSpeed;
    public float AtkCooldown;
    public int AtkReach;
    public float AtkRange;
    public int Mass;

    public float OffsetX;
    public float OffsetY;

    public int shopPrice;
}

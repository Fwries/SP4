using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Dungeon Hurler/Weapon")]
public class ScWeapon : ScriptableObject
{
    public enum TypeEnum
    {
        Swing,
        Stab,
        Crush
    };

    public GameObject Prefab;
    public string Name;
    public string Description;

    public TypeEnum AtkType = TypeEnum.Swing;

    public float AtkSpeed;
    public float AtkCooldown;
    public int AtkReach;
    public float AtkRange;
    public int Weight;

    public float OffsetX;
    public float OffsetY;
}

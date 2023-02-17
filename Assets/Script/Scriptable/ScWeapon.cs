using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Dungeon Hurler/Weapon")]
public class ScWeapon : ScriptableObject
{
<<<<<<< Updated upstream
    public Sprite WeaponIcon;
    public string Weapon;
    public string Description;
    public int RedDmg;
    public int OrangeDmg;
    public int YellowDmg;
=======
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

>>>>>>> Stashed changes
    public float AtkSpeed;
    public int AtkReach;
<<<<<<< HEAD
    public int Area1;
    public int Area2;
    public int Mass;
=======
    public float AtkRange;
    public int Weight;
<<<<<<< Updated upstream
=======
>>>>>>> a7d85e4b55e5906279b6cda23daacd45c55b470c

    public float OffsetX;
    public float OffsetY;
>>>>>>> Stashed changes
}

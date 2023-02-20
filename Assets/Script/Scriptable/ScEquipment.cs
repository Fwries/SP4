using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Dungeon Hurler/Equipment")]
public class ScEquipment : ScriptableObject
{
    public Sprite EquipmentIcon;
    public string Description;
    public int shopPrice;
    public float AtkSpeedAdd;
    public int HealthAdd;
    public int ReachAdd;
    public int SpeedAdd;
    public float RegenAdd;
    public int DodgeAdd;
    public int CritChanceAdd;
    public int LifeStealAdd;
    public int ResistDamageAdd;
}
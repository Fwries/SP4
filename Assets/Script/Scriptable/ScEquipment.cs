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
    public float SpeedAdd;
    public int RegenAdd;
    public int DodgeAdd;
    public int CritChanceAdd;
    public int LifeStealAdd;
    public int ResistDamageAdd;
    [SerializeField]private int BaseStack;
    public int stack;
    private void OnEnable()
    {
        stack = BaseStack;
    }
}
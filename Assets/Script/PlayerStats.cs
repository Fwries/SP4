using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public string Username;
    public int MaxHealth = 17;
    public int currentHealth;
    public int Coin;

    public float AtkSpeed;
    public int Reach;
    public int Speed;
    public float Regen;
    public int Dodge;
    public int CritChance;
    public int LifeSteal;
    public int ResistDamage;

    public HealthBarBehaviour HealthBar;
    public coinCounter coinCounter;


    private void Start()
    {
        currentHealth = MaxHealth;
        HealthBar.SetMaxHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        damage -= ResistDamage;
        if (damage < 0) { damage = 0; }

        currentHealth -= damage;
        HealthBar.SetHealth(currentHealth);
    }
    public void IncreaseCoins(int n)
    {
        Coin += n;
        coinCounter.SetCoins(Coin);
    }
    public void RecoverHealth(int amount)
    {
        currentHealth += amount;
        HealthBar.SetHealth(currentHealth);
    }

    public void EquipmentEquip(ScEquipment scEquipment)
    {
        AtkSpeed += scEquipment.AtkSpeedAdd;
        currentHealth += scEquipment.HealthAdd;
        MaxHealth += scEquipment.HealthAdd;
        Reach += scEquipment.ReachAdd;
        Speed += scEquipment.SpeedAdd;
        Regen += scEquipment.RegenAdd;
        Dodge += scEquipment.DodgeAdd;
        CritChance += scEquipment.CritChanceAdd;
        LifeSteal += scEquipment.LifeStealAdd;
        ResistDamage += scEquipment.ResistDamageAdd;

        HealthBar.SetHealth(currentHealth);
    }
}

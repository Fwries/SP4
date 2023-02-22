using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public string Username;
    public int MaxHealth = 20;
    public int currentHealth;
    public int Coin;

    public float AtkSpeed;
    public int Reach;
    public int Speed;
    public int Regen;
    public int Dodge;
    public int CritChance;
    public int LifeSteal;
    public int ResistDamage;

    public HealthBarBehaviour HealthBar;
    public coinCounter coinCounter;

    private float Cooldown;

    private void Start()
    {
        currentHealth = MaxHealth;
        HealthBar.SetMaxHealth(currentHealth);
    }

    void Update()
    {
        Cooldown += Time.deltaTime;
        if (Cooldown >= 1)
        {
            if (Regen > 0)
            {
                RecoverHealth(Regen);
            }

            Cooldown = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        if (Random.Range(0, 100) >= Dodge) { return; }

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

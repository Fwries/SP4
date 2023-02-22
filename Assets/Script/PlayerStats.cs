using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public string Username;
    public int MaxHealth = 20;
    public int currentHealth;
    public int Coin;

    public float AtkSpeed;
    public int Reach;
    public float Speed;
    public int Regen;
    public int Dodge;
    public int CritChance;
    public int LifeSteal;
    public int ResistDamage;

    public HealthBarBehaviour HealthBar;
    public coinCounter coinCounter;
    public GameObject dodgeText;

    private float Cooldown;

    private void Start()
    {
        currentHealth = MaxHealth;
        HealthBar.SetMaxHealth(currentHealth);
    }

    void Update()
    {
        Cooldown += Time.deltaTime;
        if (Cooldown >= 20)
        {
            if (Regen > 0)
            {
                RecoverHealth(Regen);
            }

            Cooldown = 0;
        }
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("DeathScene");
        }
    }

    public void TakeDamage(int damage)
    {
        if (Random.Range(0, 100) <= Dodge) 
        {
            var go = Instantiate(dodgeText, transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().color = Color.white;
            go.GetComponent<TextMesh>().text = "DODGED";
            return; 
        }

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
        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
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

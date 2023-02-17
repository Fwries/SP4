using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int MaxHealth = 17;
    private int currentHealth;
    public HealthBarBehaviour HealthBar;
    private void Start()
    {
        currentHealth = MaxHealth;
        HealthBar.SetMaxHealth(currentHealth);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealthBar.SetHealth(currentHealth);
    }
}

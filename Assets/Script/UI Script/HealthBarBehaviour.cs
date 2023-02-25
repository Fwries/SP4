using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    public Slider slider;
    public GameObject deathScreen;
    private int currentHealth;
    public void SetMaxHealth(int health)
    {
        currentHealth = health;
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        currentHealth = health;
    }
    public void CheckDeath()
    {
        if(currentHealth<=0)
        {
            deathScreen.SetActive(true);
        }
    }
}

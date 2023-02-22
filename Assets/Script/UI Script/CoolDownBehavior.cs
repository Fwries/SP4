using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownBehavior : MonoBehaviour
{
    public Slider slider;
    public void SetMaxCooldown(float cooldown)
    {
        slider.maxValue = cooldown;
    }
    public void SetCooldown(float cooldown)
    {
        slider.value = cooldown;
    }
}

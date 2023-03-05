using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Health healthReference;

    void Start()
    {
        SetMaxHealth(healthReference.initialHealth);
        healthReference.TriggerUpdateHealth += SetHealth;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        Debug.Log("HEALTH: " + health);
        Debug.Log(health > healthReference.initialHealth);

        if (health > healthReference.initialHealth) {
            slider.value = 100f;
        } else {
            slider.value = health;
        }

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}

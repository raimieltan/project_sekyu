using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Health healthReference;

    void Start()
    {
        slider.maxValue = 100f;
        slider.value = 0;
        healthReference.TriggerUpdateHealth += SetArmor;
    }


    public void SetArmor(float health)
    {
        float armorValue = health - 100;

        if (armorValue > 100)
        {
            slider.value = 100;
        }
        else if (armorValue < 0)
        {
            slider.value = 0;
        }
        else
        {
            slider.value = armorValue;
        }

        Debug.Log("slider value: " + slider.value);
    }
}

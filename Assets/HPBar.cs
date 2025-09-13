using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider healthSlider;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        // Calculate the fill amount based on current health relative to max health
        healthSlider.value = currentHealth / maxHealth;
        Debug.Log(healthSlider.value);
        Debug.Log(currentHealth / maxHealth);
    }
}

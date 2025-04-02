using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private Slider healthSlider; // La jauge de vie (Slider UI)
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float healthDrainRate = 1f; // Vitesse de diminution de la vie
    [SerializeField] private bool enemyAlerted = false; // Booléen indiquant si un ennemi est en alerte
    
    // Start is called before the first frame update
    void Start()
    {
        healthSlider = FindObjectOfType<Slider>();
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAlerted)
        {
            DecreaseHealthOverTime();
        }
    }
    void DecreaseHealthOverTime()
    {
        if (currentHealth > 0)
        {
            currentHealth -= healthDrainRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UpdateHealthUI();
        }
    }
    
    void UpdateHealthUI()
    {
        healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth / maxHealth, Time.deltaTime * 5);
    }
}

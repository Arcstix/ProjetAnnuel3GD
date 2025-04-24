using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private Slider healthSlider; // La jauge de vie (Slider UI)
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float decreaseTimer = 1f; // Vitesse de diminution de la vie
    [SerializeField] private float increaseTimer = 1f; // Vitesse d'augmentation de la vie
    [SerializeField] private float timerBeforeIncrease = 0f;
    private float timer;
    public bool enemyAlerted = false; // Booléen indiquant si un ennemi est en alerte
    [SerializeField] private GameObject spawner;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        timer = 0f;
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        playerTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAlerted)
        {
            healthSlider.gameObject.SetActive(true);
            DecreaseHealthOverTime();
        }
        if (!enemyAlerted)
        {
            timer += Time.deltaTime;
            if (timer >= timerBeforeIncrease)
            {
                 IncreaseHealthOverTime();
            }
            if (currentHealth == maxHealth)
            {
                healthSlider.gameObject.SetActive(false);
            }
        }

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }
    void DecreaseHealthOverTime()
    {
        if (currentHealth > 0)
        {
            currentHealth -= decreaseTimer * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UpdateHealthUI();
        }
    }
    void IncreaseHealthOverTime()
    {
        if (currentHealth < maxHealth) // Vérifie si la vie n'est pas déjà au maximum
        {
            currentHealth += increaseTimer * Time.deltaTime; // Augmente la vie progressivement
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // S'assure que la vie reste dans les limites
            UpdateHealthUI(); // Met à jour l'affichage de la barre de vie
        }
    }
    
    void UpdateHealthUI()
    {
        healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth / maxHealth, Time.deltaTime * 5);
    }

    public void Respawn()
    {
        if (spawner)
        {
            playerTransform.position = spawner.transform.position; // Déplace le joueur
        }
        ResetHealth(); // Remet la vie à 100%
    }

    private void ResetHealth()
    {
        currentHealth = maxHealth; // Reset la vie
        UpdateHealthUI();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathZone")) // Vérifie si on touche la DeathZone
        {
            Respawn();
        }
    }
}

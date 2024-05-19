using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public  TextMeshProUGUI healthText;

    void Start()
    {
        // Initialize current health to max health at the start
        currentHealth = maxHealth;
        healthText.text = "health:"+currentHealth.ToString();
    }

    // Method to increase health
    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Cap the health to the max value
        }
        healthText.text = "health:" + currentHealth.ToString();

        Debug.Log("Health increased by " + amount + ". Current health: " + currentHealth);
    }

    // Method to handle taking damage (optional)
    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0; // Ensure health does not go below zero
            Debug.Log("Player health is zero. Player will be destroyed.");
            Destroy(gameObject); // Destroy the player GameObject
        }
        healthText.text = "health:" + currentHealth.ToString();

        Debug.Log("Health decreased by " + amount + ". Current health: " + currentHealth);
    }

    // Method to handle taking damage (optional)
    public void TakeDamage(int amount)
    {
        DecreaseHealth(amount);
    }

}

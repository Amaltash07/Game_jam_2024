using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        // Initialize current health to max health at the start
        currentHealth = maxHealth;
    }

    // Method to increase health
    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Cap the health to the max value
        }

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

        Debug.Log("Health decreased by " + amount + ". Current health: " + currentHealth);
    }

    // Method to handle taking damage (optional)
    public void TakeDamage(int amount)
    {
        DecreaseHealth(amount);
    }

}

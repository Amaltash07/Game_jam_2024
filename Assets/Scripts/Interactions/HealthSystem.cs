using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }
    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log(this.gameObject + "HP= " +  currentHealth);
    }
    public void Damage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Ded");
            DeathLogic();
        }
        Debug.Log(this.gameObject + "HP= " + currentHealth);
    }
    void DeathLogic()
    {
        if(this.gameObject.CompareTag("Player"))
        {
            //player Death Logic,reload game(death logic)
            Destroy(this.gameObject);
        }

        else if(this.gameObject.CompareTag("Enemy"))
        {
            //Enemy destroy and scoring logic(maybe some paticale effects)
            PlayerScore.Instance.AddScore(100);
            Destroy(this.gameObject);
        }
        else
        {
            //wall Destroy logic(maybe some paticale effects)
            Destroy(this.gameObject);
        }
    }
}

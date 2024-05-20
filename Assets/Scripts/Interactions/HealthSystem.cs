using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public TextMeshProUGUI HPCount;

    void Start()
    {
        currentHealth = maxHealth;
        updateHP();
    }
    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        updateHP();
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
        updateHP();
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
    void updateHP()
    {
        if (HPCount != null)
        {
            HPCount.text = "HP = " + currentHealth.ToString();
        }
        
    }
}

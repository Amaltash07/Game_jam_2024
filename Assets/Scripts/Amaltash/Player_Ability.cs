using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Player_Ability : MonoBehaviour
{
    private InventorySystem inventory;
    private HealthSystem playerHealth;
    private WeaponManager weapon;
    public TextMeshProUGUI AbilityName;
    private int abilityID = 0;

    void Start()
    {
        inventory = FindObjectOfType<InventorySystem>();
        playerHealth = GetComponent<HealthSystem>();
        weapon = GetComponent<WeaponManager>();
        updateAbility();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Item"))
        {
            Debug.Log("colliding with the item");
            int itemID = other.gameObject.GetComponent<itemHandler>().itemID;


            if (inventory.PickupItem(itemID))
            {
                updateAbility();
                Destroy(other.gameObject); // Destroy item object if picked up successfully
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switchAbility(true);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            switchAbility(false);
        }

        useAbility(abilityID);
    }
    void switchAbility(bool dir)
    {
        if (dir)
        {
            abilityID = (abilityID + 1) % inventory.abilityCount;
        }

        else if (!dir)
        {
            abilityID = (abilityID - 1) % inventory.abilityCount;
            if (abilityID == -1)
            {
                abilityID = inventory.abilityCount + abilityID;
            }
        }
        updateAbility();

    }
    void useAbility(int abilityID)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (abilityID == 0 && playerHealth.currentHealth < playerHealth.maxHealth)
            {
                bool used = inventory.UseItem(abilityID);
                if (used)
                {
                    playerHealth.Heal(20);
                    Debug.Log("ability used.");
                    updateAbility();
                }
                else
                {
                    Debug.Log("Ability unavailable");
                }
            }
            else if (abilityID == 1 && !weapon.isWallBreaker)
            {
                bool used = inventory.UseItem(abilityID);
                if (used)
                {
                    weapon.activateWallBreaker();
                    updateAbility();
                    Debug.Log("ability used.");
                }
                else
                {
                    Debug.Log("Ability unavailable");
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (playerHealth != null)
            {
                playerHealth.Damage(40); // Decrease health by 40
            }
        }
    }
    void updateAbility()
    {
        inventory.changeIcon(abilityID);
        editNumber();
    }
    public void editNumber()
    {
        AbilityName.text = inventory.items[abilityID].currentPickups.ToString();
    }
}

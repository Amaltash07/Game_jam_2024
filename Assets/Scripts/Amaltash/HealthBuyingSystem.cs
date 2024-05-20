using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuyingSystem : MonoBehaviour
{
    public int Cost = 10;
    public int ammoCount = 1;
    private InventorySystem inventorySystem;
    private bool isInHealthZone = false; 

    private void Start()
    {
        inventorySystem = FindObjectOfType<InventorySystem>();
    }
    void Update()
    {
        if (isInHealthZone && Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("TAB key pressed.");
            BuyBullet();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter detected with object: " + other.gameObject.name);
        if (other.CompareTag("HealthZone"))
        {
            isInHealthZone = true;
            Debug.Log("Entered Health Zone");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit detected with object: " + other.gameObject.name);
        if (other.CompareTag("HealthZone"))
        {
            isInHealthZone = false;
            Debug.Log("Exited Buying Zone");
        }
    }

    public void BuyBullet()
    {
        if (inventorySystem.items[0].currentPickups < inventorySystem.items[0].maxPickups) 
        {
            if (PlayerScore.Instance.SpendScore(Cost))
            {
                inventorySystem.PickupItem(0);
                Player_Ability ability = GetComponent<Player_Ability>();
                ability.editNumber();
            }
            else
            {
                Debug.Log("Not enough score to buy a health.");
            }
        }
    }
}

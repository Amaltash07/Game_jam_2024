using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreakBuyingSystem : MonoBehaviour
{
    public int Cost = 10;
    public int ammoCount = 1;
    private InventorySystem inventorySystem;
    private bool isInWallZone = false;

    private void Start()
    {
        inventorySystem = FindObjectOfType<InventorySystem>();
    }
    void Update()
    {
        if (isInWallZone && Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("TAB key pressed.");
            BuyBullet();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter detected with object: " + other.gameObject.name);
        if (other.CompareTag("WallZone"))
        {
            isInWallZone = true;
            Debug.Log("Entered Health Zone");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit detected with object: " + other.gameObject.name);
        if (other.CompareTag("WallZone"))
        {
            isInWallZone = false;
            Debug.Log("Exited Buying Zone");
        }
    }

    public void BuyBullet()
    {
        if (inventorySystem.items[1].currentPickups < inventorySystem.items[1].maxPickups)
        {
            if (PlayerScore.Instance.SpendScore(Cost))
            {
                inventorySystem.PickupItem(1);
                Player_Ability ability=GetComponent<Player_Ability>();
                ability.editNumber();
            }
            else
            {
                Debug.Log("Not enough score to buy a health.");
            }
        }
    }
}
  


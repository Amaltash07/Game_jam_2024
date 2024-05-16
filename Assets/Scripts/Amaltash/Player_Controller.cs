using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Controller  : MonoBehaviour
{


    public InventorySystem inventory;

    void Start()
    {
        if (inventory == null)
        {
            inventory = FindObjectOfType<InventorySystem>();
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Item"))
        {
            Debug.Log("colliding with the item");
            int itemID = other.gameObject.GetComponent<itemHandler>().itemID;

            if (inventory.PickupItem(itemID))
            {
                Destroy(other.gameObject); // Destroy item object if picked up successfully
            }
        }
    }
}

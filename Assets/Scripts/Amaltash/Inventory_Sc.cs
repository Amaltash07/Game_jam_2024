using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public int itemID;
        public int maxPickups;
        public int currentPickups;

        public Item(int ID, int max)
        {
            itemID = ID;
            maxPickups = max;
            currentPickups = 0;
        }
    }

    // List to store the items in the inventory
    public List<Item> items;

    void Start()
    {
        // Initialize the items list
        items = new List<Item>
        {
            new Item(0, 3),
            new Item(1, 2),
            new Item(2, 1)
        };
    }

    // Method to pick up an item
    public bool PickupItem(int itemID)
    {
        foreach (Item item in items)
        {
            if (item.itemID == itemID)
            {
                if (item.currentPickups < item.maxPickups)
                {
                    item.currentPickups++;
                    Debug.Log("Picked up: " + itemID);
                    return true;
                }
                else
                {
                    Debug.Log("Cannot pick up " + itemID + " anymore.");
                    return false;
                }
            }
        }

        Debug.Log("Item " + itemID + " not found in inventory.");
        return false;
    }
}

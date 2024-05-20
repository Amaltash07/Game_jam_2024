using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public string name;
        public int itemID;
        public GameObject icon;
        public int maxPickups;
        public int currentPickups;

        public Item(int ID, int max)
        {
            itemID = ID;
            maxPickups = max;
            currentPickups = 0;
        }
    }

    public List<Item> items;
    public int abilityCount;

    private void Start()
    {
        abilityCount=items.Count;
    }
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

    public bool UseItem(int itemID)
    {
        foreach (Item item in items)
        {
            if (item.itemID == itemID)
            {
                if (item.currentPickups > 0)
                {
                    item.currentPickups--;
                    Debug.Log("Used one: " + itemID);
                    return true;
                }
                else
                {
                    Debug.Log("No more " + itemID + " to use.");
                    return false;
                }
            }
        }
        Debug.Log("Item " + itemID + " not found in inventory.");
        return false;
    }
    public void changeIcon(int itemID)
    {
        foreach(Item item in items)
        {
            if(item.itemID == itemID)
            {
                item.icon.SetActive(true);
            }
            else
            {
                item.icon.SetActive(false);
            }
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilityBuyZone : MonoBehaviour
{
    private int cost;
    private bool isInBuyingZone = false;
    private AbilitySpawner buyShit;


    void Update()
    {
        if (isInBuyingZone && Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("TAB key pressed.");
            BuyAbility();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter detected with object: " + other.gameObject.name);
        if (other.CompareTag("abilityZone"))
        {
            isInBuyingZone = true;
            cost = buyShit.Cost;
            buyShit=other.gameObject.GetComponent<AbilitySpawner>();
            Debug.Log("Entered Buying Zone");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit detected with object: " + other.gameObject.name);
        if (other.CompareTag("abilityZone"))
        {
            isInBuyingZone = false;
            cost = 0;
            buyShit=null;
            Debug.Log("Exited Buying Zone");
        }
    }

    public void BuyAbility()
    {
        if (PlayerScore.Instance.SpendScore(cost))
        {
            if(buyShit != null)
            {
                buyShit.spawn();
            }
           
        }
        else
        {
            Debug.Log("Not enough score to buy the ability.");
        }
    }
}

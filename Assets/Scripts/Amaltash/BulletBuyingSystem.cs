using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBuyingSystem : MonoBehaviour
{
    public int bulletCost = 10;
    public PlayerScore playerScore;
    public int bullets = 0;
    private bool isInBuyingZone = false; // To track if player is in the buying zone

    void Start()
    {
        if (playerScore == null)
        {
            playerScore = FindObjectOfType<PlayerScore>();
            if (playerScore == null)
            {
                Debug.LogError("PlayerScore component not found in the scene.");
            }
        }
    }

    void Update()
    {
        if (isInBuyingZone && Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("TAB key pressed.");
            BuyBullet();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter detected with object: " + other.gameObject.name);
        if (other.CompareTag("BuyingZone"))
        {
            isInBuyingZone = true;
            Debug.Log("Entered Buying Zone");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit detected with object: " + other.gameObject.name);
        if (other.CompareTag("BuyingZone"))
        {
            isInBuyingZone = false;
            Debug.Log("Exited Buying Zone");
        }
    }

    public void BuyBullet()
    {
        if (playerScore.SpendScore(bulletCost))
        {
            bullets++;
            Debug.Log("Bullet purchased! Total bullets: " + bullets);
        }
        else
        {
            Debug.Log("Not enough score to buy a bullet.");
        }
    }
}

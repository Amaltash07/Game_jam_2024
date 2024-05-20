using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBuyingSystem : MonoBehaviour
{
    public int bulletCost = 10;
    public int ammoCount = 0;
    private WeaponManager playerGun;
    private bool isInBuyingZone = false; // To track if player is in the buying zone

    private void Start()
    {
        playerGun = GetComponent<WeaponManager>();
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
        if (playerGun.currentWeapon.CurrentAmmo < playerGun.currentWeapon.MaxAmmo)
        {
            if (PlayerScore.Instance.SpendScore(bulletCost))
            {
                playerGun.buyAmmo(ammoCount);
                Debug.Log("Bullet purchased! Total bullets: " + ammoCount);
            }
            else
            {
                Debug.Log("Not enough score to buy a bullet.");
            }
        }
        else
        {
            Debug.Log("ammo full");
        }

    }
}

using UnityEngine;
using DG.Tweening;
using static Weapon;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weapons;

    [HideInInspector]
    public Weapon currentWeapon;
    public float switchTime;
    public LayerMask targetMask;


    private int currentID = 0;
    private bool canSwitch;
    private float nextFireTime;
    private Camera playerCamera;

    private void Start()
    {
        playerCamera = Camera.main;
        canSwitch = false;
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].isActive)
            {
                currentID = i;
                weaponEquip(weapons[i]);
                break;
            }
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Change Weapon"))
        {
            nextWeapon();
        }

        weaponAttackLogic();

    }



    void weaponEquip(Weapon weapon)
    {
        weapon.weaponRectTransform.localPosition = Vector3.up * -Screen.height;
        weapon.weaponObject.SetActive(true);
        weapon.weaponRectTransform.DOLocalMoveY(0, switchTime).OnComplete(() =>
        {
            currentWeapon = weapon;
            canSwitch = true;
        });
    }
    void nextWeapon()
    {
        if (canSwitch)
        {
            canSwitch = false;
            do
            {
                currentID = (currentID + 1) % weapons.Count;
            }
            while (!weapons[currentID].isActive);
            switchWeapon(weapons[currentID]);


        }

    }
    void switchWeapon(Weapon newWeapon)
    {

        currentWeapon.weaponRectTransform.DOLocalMoveY(-Screen.height, switchTime).OnComplete(() =>
        {
            currentWeapon.weaponObject.SetActive(false);
            weaponEquip(newWeapon);
        });
    }

    void weaponAttackLogic()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentWeapon.ShootType == shootingType.SemiAuto)
            {
                attackLogic();
            }
            else if (currentWeapon.ShootType == shootingType.Burst)
            {
                StartCoroutine(FireBurst());
            }
        }
        else if (Input.GetButton("Fire1"))
        {
            if (currentWeapon.ShootType == shootingType.Auto && Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 1 / currentWeapon.rateOfFire;
                attackLogic();
            }
        }
    }

    IEnumerator FireBurst()
    {
        if (Time.time < nextFireTime)
        {
            yield break;
        }

        int bulletsFired = 0;

        while (bulletsFired < currentWeapon.burstCount)
        {
            if (Time.time >= nextFireTime)
            {
                attackLogic();
                bulletsFired++;
                yield return new WaitForSeconds(1f / currentWeapon.rateOfFire);
            }
            else
            {
                yield return null;
            }
        }

        nextFireTime = Time.time + currentWeapon.burstInterval;
    }

    void attackLogic()
    {
        if (currentWeapon.WeaponType == weaponType.melee)
        {
            Debug.Log("Orraaa");
        }
        else if (currentWeapon.CurrentAmmo > 0)
        {
            if (currentWeapon.WeaponType == weaponType.hitscan)
            {
                ShootHitScan();
            }
            else if (currentWeapon.WeaponType == weaponType.projectile)
            {
                Debug.Log("Boom");
            }
            else if (currentWeapon.WeaponType == weaponType.hybrid)
            {
                Debug.Log("PewBoom");
            }
            currentWeapon.CurrentAmmo--;
        }
        else
        {
            Debug.Log("Out of Ammo");
        }

    }

    void ShootHitScan()
    {
        ApplyRecoil();

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, currentWeapon.hitscanRange, targetMask))
        {
            Debug.Log("Hit " + hit.collider.gameObject.name);
            Destroy(hit.collider.gameObject);
        }
        else
        {
            // If no hit, you may want to do something here
        }
    }
    void ApplyRecoil()
    {
        Vector2 recoilDirection =new Vector2(Random.Range(-currentWeapon.aimOffset.x, currentWeapon.aimOffset.x), Random.Range(-currentWeapon.aimOffset.y, currentWeapon.aimOffset.y));

        playerCamera.transform.Rotate(recoilDirection.y, 0, 0);
        transform.Rotate(0, recoilDirection.x, 0);
    }
}


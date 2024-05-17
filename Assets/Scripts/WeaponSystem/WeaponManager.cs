using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Weapon;
using System.Collections;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weapons;

    [HideInInspector]
    public Weapon currentWeapon;
    public float switchTime;


    private int currentID = 0;
    private bool canSwitch;
    private float nextFireTime;

    public RectTransform crosshairUI;
    public ParticleSystem muzzleFlash;
    public TrailRenderer bulletTrail;




    private void Start()
    {
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
                Debug.Log("Pew");
                //hitscan  system
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
    
}


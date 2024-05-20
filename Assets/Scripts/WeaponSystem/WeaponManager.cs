using UnityEngine;
using DG.Tweening;
using static Weapon;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weapons;

    [HideInInspector]
    public Weapon currentWeapon;
    public float switchTime;
    public LayerMask targetMask;
    public Transform firepoint;
    public float hitDelay;
    public bool isWallBreaker;
    public float wallBreakerDuration;
    public TextMeshProUGUI ammoText;

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
        /*
        if (Input.GetButtonDown("Change Weapon"))
        {
            nextWeapon();
        }
        */

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchTo(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchTo(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            switchTo(2);
        }


        weaponAttackLogic();

    }

    void switchTo(int ID)
    {
        if(canSwitch)
        {
            if (currentID != ID)
            {
                canSwitch = false;
                currentID = ID;
                switchWeapon(weapons[ID]);
            }
        }
       
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
    void weaponEquip(Weapon weapon)
    {
        weapon.weaponRectTransform.localPosition = Vector3.up * -Screen.height;
        weapon.weaponObject.SetActive(true);
        weapon.weaponRectTransform.DOLocalMoveY(0, switchTime).OnComplete(() =>
        {
            currentWeapon = weapon;
            canSwitch = true;
            if (currentWeapon.CurrentAmmo > currentWeapon.MaxAmmo)
            {
                currentWeapon.CurrentAmmo = currentWeapon.MaxAmmo;
            }
            displayAmmo();
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
            displayAmmo();
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
        GameObject bulletTrail = Instantiate(currentWeapon.projectilePrefab, firepoint.position, Quaternion.identity);

        if (Physics.Raycast(ray, out hit, currentWeapon.hitscanRange, targetMask))
        {
            float distanceRatio = Vector3.Distance(firepoint.position, hit.collider.gameObject.transform.position) / Vector3.Distance(firepoint.position, ray.GetPoint(currentWeapon.hitscanRange));
            bulletTrail.transform.DOMove(hit.point, hitDelay * distanceRatio).OnComplete(() =>
            {
                if(isWallBreaker)
                {
                    Debug.Log(Vector3.Distance(firepoint.position, hit.collider.gameObject.transform.position));
                    damageEnemy(hit.collider.gameObject);
                    Destroy(bulletTrail);
                }
                else if(!isWallBreaker && hit.collider.CompareTag("Enemy"))
                {
                    Debug.Log(Vector3.Distance(firepoint.position, hit.collider.gameObject.transform.position));
                    damageEnemy(hit.collider.gameObject);
                    Destroy(bulletTrail);
                }
            });
        }
        else
        {

            bulletTrail.transform.DOMove(ray.GetPoint(currentWeapon.hitscanRange), hitDelay).OnComplete(() =>
            {
                Destroy(bulletTrail);
            });
        }
    }
    private void ApplyRecoil()
    {
        Vector3 customShakePattern = currentWeapon.recoilOffset;
         playerCamera.transform.DOShakePosition(0.1f, customShakePattern);
    }

    void damageEnemy(GameObject hitObject)
    {
        hitObject.GetComponent<HealthSystem>().Damage(currentWeapon.bulletDamage);
    }

    public void buyAmmo(int ammoCount)
    {
        currentWeapon.loadAmmo(ammoCount);
        displayAmmo();
    }
    public void activateWallBreaker()
    {
        isWallBreaker = true;
        Invoke("resetWallBreaker", wallBreakerDuration);
    }
    void resetWallBreaker()
    {
        isWallBreaker=false;
    }
    void displayAmmo()
    {
        ammoText.text=currentWeapon.CurrentAmmo.ToString();
    }


}

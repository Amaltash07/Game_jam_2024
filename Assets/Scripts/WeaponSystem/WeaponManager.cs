using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weapons;

    public Weapon currentWeapon;
    public float switchTime;


    private int currentID = 0;
    private bool canSwitch;



    private void Start()
    {
        canSwitch = false;
        for(int i=0;i<weapons.Count;i++)
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
        if(canSwitch)
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
}

using UnityEngine;
using DG.Tweening;
using Unity.PlasticSCM.Editor.WebApi;

[System.Serializable]
public class Weapon
{
    public string weaponName;
    public GameObject weaponObject;
    public RectTransform weaponRectTransform;

    public float rateOfFire;
    public float bulletDamage;
    public int MaxAmmo;
    public int burstCount;
    public float burstInterval;
    [HideInInspector]
    public enum shootingType
    {
        Auto,
        Burst,
        SemiAuto
    }
    [HideInInspector]
    public enum weaponType
    {
        hitscan,
        projectile,
        hybrid,
        melee
    }
    public shootingType ShootType;
    public weaponType WeaponType;
    public GameObject projectilePrefab;
    public float hitscanRange;
    public Vector2 recoilOffset;
    public bool isActive;

    //[HideInInspector]
    public int CurrentAmmo;

    public Weapon(GameObject obj)
    {
        weaponObject = obj;
        weaponRectTransform = obj.GetComponent<RectTransform>();
    }

    public void loadAmmo(int ammoCount)
    {
        if(CurrentAmmo<MaxAmmo)
        {
            CurrentAmmo += ammoCount;
            if(CurrentAmmo > MaxAmmo)
            {
                CurrentAmmo = MaxAmmo;
            }
        }
    }
}

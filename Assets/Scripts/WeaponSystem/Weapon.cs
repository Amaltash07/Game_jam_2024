using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class Weapon
{
    public string weaponName;
    public GameObject weaponObject;
    public RectTransform weaponRectTransform;

    public float rateOfFire;
    public float bulletDamage;
    public int MaxAmmo;
    public bool isAutoMatic;
    public bool isMelee;
    public bool isActive;

    public Weapon(GameObject obj)
    {
        weaponObject = obj;
        weaponRectTransform = obj.GetComponent<RectTransform>();
    }
}

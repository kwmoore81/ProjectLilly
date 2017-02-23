using UnityEngine;
using System.Collections;

public class BaseWeapon : BaseStatItem
{
    public enum WeaponTypes
    {
        SWORD,
        STAFF,
        SPEAR,
        DAGGER,
        CLUB,
        BOW,
        SHIELD
    }

    private WeaponTypes weaponType;

    public WeaponTypes WeaponType
    {
        get { return weaponType; }
        set { weaponType = value; }
    }
}

using UnityEngine;
using System.Collections;

public class AttackData : MonoBehaviour
{
    public GameObject projectile;

    public enum AttackType
    {
        MELEE, RANGED, SPELL, CLEANSE, RESTORE
    }

    public enum DamageType
    {
        NORMAL, FIRE, WATER, EARTH, WOOD, METAL
    }

    public string attackName;
    public string attackDescription;

    public bool partyTargeting = false;
    public bool selfTarget = false;

    public int attackDamage;
    public int energyCost;
    public int chargeCost;

    public int fireChargeRestore;
    public int waterChargeRestore;
    public int earthChargeRestore;

    public float attackWaitTime;
    public float damageWaitTime;
    public string attackAnimation;

    public float targetOffset;
    public float attackOffset;
    public float finishOffset;
    public bool moveDuringAttack = false;

    public AttackType attackType;
    public DamageType damageType;
}

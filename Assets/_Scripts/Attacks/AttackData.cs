using UnityEngine;
using System.Collections;

public class AttackData : MonoBehaviour
{
    public GameObject projectile;

    public enum AttackType
    {
        MELEE, RANGED, SPELL
    }

    public enum DamageType
    {
        NORMAL, FIRE, WATER, EARTH, WOOD, METAL
    }

    public string attackName;
    public string attackDescription;

    public float attackDamage;
    public float resouceCost;
    public float chargeCost;

    public float attackWaitTime;
    public float damageWaitTime;
    public string attackAnimation;

    public AttackType attackType;
    public DamageType damageType;
}

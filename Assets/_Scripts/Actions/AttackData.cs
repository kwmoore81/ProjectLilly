using UnityEngine;
using System.Collections;

public class AttackData : MonoBehaviour
{
    public GameObject projectile;

    public enum AttackType
    {
        MELEE, RANGED, SPELL, CLEANSE
    }

    public enum DamageType
    {
        NORMAL, FIRE, WATER, EARTH, WOOD, METAL
    }

    public string attackName;
    public string attackDescription;

    public int attackDamage;
    public int energyCost;
    public int chargeCost;

    public float attackWaitTime;
    public float damageWaitTime;
    public string attackAnimation;

    public Vector3 attackDistOffset;

    public AttackType attackType;
    public DamageType damageType;
}

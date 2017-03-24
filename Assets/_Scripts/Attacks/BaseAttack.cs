using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseAttack : MonoBehaviour
{
    public GameObject projectile;

    public enum AttackType
    {
        MELEE, RANGED, SPELL
    }

    public enum DamageType
    {
        NORMAL, FIRE, ICE, EARTH, AIR
    }

    public string attackName;
    public string attackDescription;

    public float attackDamage;
    public float attackCost;

    public float attackWaitTime;
    public float damageWaitTime;
    public string attackAnimation;

    public AttackType attackType;
    public DamageType damageType;
}

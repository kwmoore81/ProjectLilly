using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseAttack : MonoBehaviour
{
    public string attackName;
    public string attackDescription;

    public float attackDamage;
    public float attackCost;

    public enum damageType
    {
        NORMAL, FIRE, ICE, EARTH, AIR
    }
}

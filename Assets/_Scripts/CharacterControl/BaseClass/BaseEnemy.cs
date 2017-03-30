using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BaseEnemy : BaseClass
{
    public BaseEnemy()
    {
        baseStrength = 7;
        baseAgility = 8;
        baseResilience = 8;
        baseMind = 6;
        baseSpirit = 5;
        baseHealth = 100;

        BaseSpeed = CurrentAgility;
        BaseAccuracy = CurrentAgility + CurrentMind;
        BaseEvasion = CurrentAgility + CurrentSpirit;

        BaseAttackPower = CurrentStrength; //+ EquippedWeapon.PhysicalPower
        BaseMagicPower = CurrentMind; //+ EquippedWeapon.MagicPower
        BasePhysicalDefense = CurrentResilience; //+ CombinedEquipment.PhysicalDefense
        BaseMagicDefense = CurrentSpirit; //+ CombinedEquipment.MagicDefense
    }

    public List<BaseAttack> attacks = new List<BaseAttack>();

    public enum EnemyType
    {
        FIRE, ICE, EARTH, AIR
    }

    public enum SpawnType
    {
        COMMON, UNCOMMON, RARE, LEGEND
    }

    public EnemyType enemyType;
    public SpawnType spawnType;
}

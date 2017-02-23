using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseEnemy : BaseClass
{
    public BaseEnemy()
    {
        BaseStrength = 8;
        BaseAgility = 6;
        BaseEndurance = 7;
        BaseIntellect = 5;

        BaseHP = 100;
        BaseMP = 50;
    }

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

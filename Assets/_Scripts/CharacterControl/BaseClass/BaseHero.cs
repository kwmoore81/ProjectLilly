using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BaseHero: BaseClass
{
    public BaseHero()
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
    public List<BaseAttack> magicAttacks = new List<BaseAttack>();
    public List<BaseAttack> fireSpells = new List<BaseAttack>();
    public List<BaseAttack> waterSpells = new List<BaseAttack>();
    public List<BaseAttack> earthSpells = new List<BaseAttack>();
}

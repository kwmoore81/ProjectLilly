using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BaseHero: BaseClass
{
    public BaseHero()
    {
        BaseStrength = 7;
        BaseAgility = 8;
        BaseResilience = 8;
        BaseMind = 6;
        BaseSpirit = 5;
        BaseHealth = 100;

        BaseMP = 50;  // temporary until other energy sources are added to code

        BaseSpeed = CurrentAgility;
        BaseAccuracy = CurrentAgility + CurrentMind;
        BaseEvasion = CurrentAgility + CurrentSpirit;

        BaseAttackPower = CurrentStrength; //+ EquippedWeapon.PhysicalPower
        BaseMagicPower = CurrentMind; //+ EquippedWeapon.MagicPower
        BasePhysicalDefense = CurrentResilience; //+ CombinedEquipment.PhysicalDefense
        BaseMagicDefense = CurrentSpirit; //+ CombinedEquipment.MagicDefense
    }

    public List<BaseAttack> magicAttacks = new List<BaseAttack>();
}

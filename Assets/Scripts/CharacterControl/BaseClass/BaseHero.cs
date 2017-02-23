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
        BaseEndurance = 8;
        BaseIntellect = 6;

        BaseHP = 100;
        BaseMP = 50;
    }

    public List<BaseAttack> magicAttacks = new List<BaseAttack>();
}

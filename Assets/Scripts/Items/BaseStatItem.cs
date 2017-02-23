using UnityEngine;
using System.Collections;

public class BaseStatItem : BaseItem
{
    private int strength;
    private int agility;
    private int endurance;
    private int intellect;

    private int attackBonus;
    private int defenseBonus;
    private int magicAttackBonus;
    private int magicDefenseBonus;

    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    public int Agility
    {
        get { return agility; }
        set { agility = value; }
    }

    public int Endurance
    {
        get { return endurance; }
        set { endurance = value; }
    }

    public int Intellect
    {
        get { return intellect; }
        set { intellect = value; }
    }

    public int AttackBonus
    {
        get { return attackBonus; }
        set { attackBonus = value; }
    }

    public int DefenseBonus
    {
        get { return defenseBonus; }
        set { defenseBonus = value; }
    }

    public int MagicAttackBonus
    {
        get { return magicAttackBonus; }
        set { magicAttackBonus = value; }
    }

    public int MagicDefenseBonus
    {
        get { return magicDefenseBonus; }
        set { magicDefenseBonus = value; }
    }
}

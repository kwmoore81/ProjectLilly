using UnityEngine;
using System.Collections;

public class PoisonSpell_01 : BaseAttack
{
    public PoisonSpell_01()
    {
        attackName = "Poison - Lvl 1";
        attackDescription = "Poisons target for damage over time.";
        attackDamage = 5f;
        attackCost = 10f;
    }
}

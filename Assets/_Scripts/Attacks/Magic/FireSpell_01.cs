using UnityEngine;
using System.Collections;

public class FireSpell_01 : BaseAttack
{
    public FireSpell_01()
    {
        attackName = "Fireball";
        attackDescription = "Shoots a ball of fire at the enemy.";
        attackDamage = 25f;
        attackCost = 15f;
    }
}

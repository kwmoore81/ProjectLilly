using UnityEngine;
using System.Collections;

public class BasicCast2 : BaseAttack
{
    public BasicCast2()
    {
        attackName = "Fireball";
        attackDescription = "Shoots a ball of fire at the enemy.";
        attackDamage = 25f;
        attackCost = 15f;
    }
}

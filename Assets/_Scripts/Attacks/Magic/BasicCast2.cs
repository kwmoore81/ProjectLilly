using UnityEngine;
using System.Collections;

public class BasicCast3 : BaseAttack
{
    public BasicCast3()
    {
        attackName = "Fireball";
        attackDescription = "Shoots a ball of fire at the enemy.";
        attackDamage = 25f;
        attackCost = 15f;
    }
}

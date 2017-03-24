using UnityEngine;
using System.Collections;

public class BasicCast : BaseAttack
{
    public BasicCast()
    {
        attackName = "Fireball";
        attackDescription = "Shoots a ball of fire at the enemy.";
        attackDamage = 25f;
        attackCost = 15f;
    }   
}

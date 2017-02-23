using UnityEngine;
using System.Collections;

public class Slash : BaseAttack
{
    public Slash()
    {
        attackName = "Slash";
        attackDescription = "A basic attack with a bladed weapon.";
        attackDamage = 10f;
        attackCost = 0;
    }
}

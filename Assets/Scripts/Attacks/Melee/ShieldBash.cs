using UnityEngine;
using System.Collections;

public class ShieldBash : BaseAttack
{
    public ShieldBash()
    {
        attackName = "Shield Bash";
        attackDescription = "A hard strike with a shield that can cause a stun.";
        attackDamage = 8f;
        attackCost = 0;
    }
}

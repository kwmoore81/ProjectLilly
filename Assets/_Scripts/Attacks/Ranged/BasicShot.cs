using UnityEngine;
using System.Collections;

public class BasicShot : BaseAttack
{
    public BasicShot()
    {
        attackName = "Basic Shot";
        attackDescription = "Normal arrow shot";
        attackDamage = 10f;
        attackCost = 10f;
    }
}

using UnityEngine;
using System.Collections;

public class MeleeAttackButton : MonoBehaviour
{
    public BaseAttack meleeAttack;

    public void doMeleeAttack()
    {
        GameObject.Find("BattleManager").GetComponent<BattleController>().MeleeInput(meleeAttack);
    }
}

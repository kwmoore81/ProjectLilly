using UnityEngine;
using System.Collections;

public class MeleeAttackButton : MonoBehaviour
{
    private BattleController battleControl;
    public AttackData meleeAttack;

    void Start()
    {
        battleControl = GameObject.Find("BattleManager").GetComponent<BattleController>();
    }

    public void doMeleeAttack()
    {
        battleControl.MeleeInput(meleeAttack);
    }
}

using UnityEngine;
using System.Collections;

public class DefendButton : MonoBehaviour
{
    public BaseAttack defense;

    public void doDefense()
    {
        GameObject.Find("BattleManager").GetComponent<BattleController>().DefendInput();
    }
}

using UnityEngine;
using System.Collections;

public class UtilityButton : MonoBehaviour
{
    public BaseAttack utility;

    public void doUtility()
    {
        GameObject.Find("BattleManager").GetComponent<BattleController>().SetUtilityInput(utility);
    }
}

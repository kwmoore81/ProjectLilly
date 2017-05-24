using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public GameObject battleManager;

    public void CloseResultWindow()
    {
        battleManager.GetComponent<BattleController>().battleResultWait = false;
    }
}

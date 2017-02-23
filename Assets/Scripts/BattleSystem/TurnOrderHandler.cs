using UnityEngine;
using System.Collections;

[System.Serializable]
public class TurnOrderHandler
{
    public string activeAgent;  // Name of the active agent
    public GameObject agentGO;  // Game object of active agent
    public GameObject targetGO; // Game obejct of target being attacked

    public BaseAttack chosenAttack;
}

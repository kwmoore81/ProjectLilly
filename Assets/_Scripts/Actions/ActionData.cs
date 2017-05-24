using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionData : MonoBehaviour
{
    public GameObject projectile;

    public enum ActionType
    {
        BUFF, DEBUFF, HEAL
    }

    public enum ActionElementType
    {
        NONE, FIRE, WATER, EARTH, WOOD, METAL
    }

    public string actionName;
    public string actionDescription;

    public bool partyTargeting = false;
    public bool selfTarget = false;

    // Energy manipulation
    public int energyCost;
    public int energyRestore;
    public int chargeRestore;

    // Action Affects
    public int strengthChange;
    public int speedChange;
    public int defenseChange;
    public bool confusion;
    public bool blind;
    public bool removeDebuff;

    // Animation control
    public float actionWaitTime;
    public float reactionWaitTime;
    public string actionAnimation;

    public ActionType actionType;
    public ActionElementType damageType;
}

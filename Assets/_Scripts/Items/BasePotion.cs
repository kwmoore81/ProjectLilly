using UnityEngine;
using System.Collections;

public class BasePotion : BaseStatItem
{
    public enum PotionTypes
    {
            HEALTH,
            MAGIC,
            CURE,
            STRENGTH,
            AGILITY,
            ENDURANCE,
            INTELLECT
    }

    private PotionTypes potionType;
    
    public PotionTypes PotionType
    {
        get { return potionType; }
        set { potionType = value; }
    }
}

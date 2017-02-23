using UnityEngine;
using System.Collections;

public class BaseArrow : BaseStatItem
{
    public enum ArrowTypes
    {
        NORMAL,
        BLUNT,
        FIRE,
        WATER,
        OIL,
        POISON
    }

    private ArrowTypes arrowType;

    public ArrowTypes ArrowType
    {
        get { return arrowType; }
        set { arrowType = value; }
    }
}

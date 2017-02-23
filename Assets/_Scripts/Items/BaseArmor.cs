using UnityEngine;
using System.Collections;

public class BaseArmor : BaseStatItem
{
    public enum ArmorTypes
    {
        HELM,
        CHEST,
        GLOVES,
        LEGS,
        BOOTS,
        NECKLACE,
        RING
    }

    public enum ArmorMaterials
    {
        CLOTH,
        LEATHER,
        CHAIN,
        PLATE,
        JEWELRY
    }

    private ArmorTypes armorType;
    private ArmorMaterials armorMaterial;

    public ArmorTypes ArmorType
    {
        get { return armorType; }
        set { armorType = value; }
    }

    public ArmorMaterials ArmorMaterial
    {
        get { return armorMaterial; }
        set { armorMaterial = value; }
    }
}

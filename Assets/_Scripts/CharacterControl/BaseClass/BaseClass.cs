using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseClass
{
    // Character info (public)
    public string characterName;
    public string characterDescription;

    // Basic Attributes (public)
    public int baseStrength;
    public int baseAgility;
    public int baseResilience;
    public int baseMind;
    public int baseSpirit;

    // Basic Attributes (private)
    private int currentStrength;
    private int currentAgility;
    private int currentResilience;
    private int currentMind;
    private int currentSpirit;

    // Resources (public)
    public float baseHealth;
    public float baseEnergy;
    public float maxFireCharges;
    public float maxWaterCharges;
    public float maxEarthCharges;

    // Resources (private)
    private float currentHealth;
    private float currentEnergy;
    private float currentFireCharges;
    private float currentWaterCharges;
    private float currentEarthCharges;

    // Secondary Attributes (private)
    // Speed = Agility
    private float baseSpeed;
    private float currentSpeed;
    // Accuracy = Agility + Mind
    private float baseAccuracy;
    private float currentAccuracy;
    // Evasion = Agility + (Resilience or Spirit?)
    private float baseEvasion;
    private float currentEvasion;
    // Attack Power = Strength + Weapon Base Physical Power
    private float baseAttackPower;
    private float currentAttackPower;
    // Magic Power = Mind + Weapon Base Magic Power
    private float baseMagicPower;
    private float currentMagicPower;
    // Physical Defense = Resilience + Combined Armor Base Physical Defense
    private float basePhysicalDefense;
    private float currentPhysicalDefense;
    // Magic Defense = Spirit + Combined Armor Base Magic Defense
    private float baseMagicDefense;
    private float currentMagicDefense;

    //// Name and description
    //public string CharacterName
    //{
    //    get { return characterName; }
    //    set { characterName = value; }
    //}

    //public string CharacterDescription
    //{
    //    get { return characterDescription; }
    //    set { characterDescription = value; }
    //}

    // Basic attributes (private)
    //public int BaseStrength
    //{
    //    get { return baseStrength; }
    //    set { baseStrength = value; }
    //}

    public int CurrentStrength
    {
        get { return currentStrength; }
        set { currentStrength = value; }
    }

    //public int BaseAgility
    //{
    //    get { return baseAgility; }
    //    set { baseAgility = value; }
    //}

    public int CurrentAgility
    {
        get { return currentAgility; }
        set { currentAgility = value; }
    }

    //public int BaseResilience
    //{
    //    get { return baseResilience; }
    //    set { baseResilience = value; }
    //}

    public int CurrentResilience
    {
        get { return currentResilience; }
        set { currentResilience = value; }
    }
    
    //public int BaseMind
    //{
    //    get { return baseMind; }
    //    set { baseMind = value; }
    //}

    public int CurrentMind
    {
        get { return currentMind; }
        set { currentMind = value; }
    }

    //public int BaseSpirit
    //{
    //    get { return baseSpirit; }
    //    set { baseSpirit = value; }
    //}

    public int CurrentSpirit
    {
        get { return currentSpirit; }
        set { currentSpirit = value; }
    }

    // Resources (private)
    //public float BaseHealth
    //{
    //    get { return baseHealth; }
    //    set { baseHealth = value; }
    //}

    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    //public float BaseEnergy
    //{
    //    get { return baseEnergy; }
    //    set { baseEnergy = value; }
    //}

    public float CurrentEnergy
    {
        get { return currentEnergy; }
        set { currentEnergy = value; }
    }

    //public float MaxFireCharges
    //{
    //    get { return maxFireCharges; }
    //    set { maxFireCharges = value; }
    //}

    public float CurrentFireCharges
    {
        get { return currentFireCharges; }
        set { currentFireCharges = value; }
    }

    //public float MaxWaterCharges
    //{
    //    get { return maxWaterCharges; }
    //    set { maxWaterCharges = value; }
    //}

    public float CurrentWaterCharges
    {
        get { return currentWaterCharges; }
        set { currentWaterCharges = value; }
    }

    //public float MaxEarthCharges
    //{
    //    get { return maxEarthCharges; }
    //    set { maxEarthCharges = value; }
    //}

    public float CurrentEarthCharges
    {
        get { return currentEarthCharges; }
        set { currentEarthCharges = value; }
    }

    // Speed, accuracy, and evasion
    public float BaseSpeed
    {
        get { return baseSpeed; }
        set { baseSpeed = value; }
    }

    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }

    public float BaseAccuracy
    {
        get { return baseAccuracy; }
        set { baseAccuracy = value; }
    }

    public float CurrentAccuracy
    {
        get { return currentAccuracy; }
        set { currentAccuracy = value; }
    }

    public float BaseEvasion
    {
        get { return baseEvasion; }
        set { baseEvasion = value; }
    }

    public float CurrentEvasion
    {
        get { return currentEvasion; }
        set { currentEvasion = value; }
    }

    // Attack and Defense
    public float BaseMagicPower
    {
        get { return baseMagicPower; }
        set { baseMagicPower = value; }
    }

    public float CurrentMagicPower
    {
        get { return currentMagicPower; }
        set { currentMagicPower = value; }
    }

    public float BaseAttackPower
    {
        get { return baseAttackPower; }
        set { baseAttackPower = value; }
    }

    public float CurrentAttackPower
    {
        get { return currentAttackPower; }
        set { currentAttackPower = value; }
    }

    public float BasePhysicalDefense
    {
        get { return basePhysicalDefense; }
        set { basePhysicalDefense = value; }
    }

    public float CurrentPhysicalDefense
    {
        get { return currentPhysicalDefense; }
        set { currentPhysicalDefense = value; }
    }

    public float BaseMagicDefense
    {
        get { return baseMagicDefense; }
        set { baseMagicDefense = value; }
    }

    public float CurrentMagicDefense
    {
        get { return currentMagicDefense; }
        set { currentMagicDefense = value; }
    }
}
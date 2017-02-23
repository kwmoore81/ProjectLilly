using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseClass
{
    private string characterName;
    private string characterDescription;

    private int baseStrength;
    private int currentStrength;
    private int baseAgility;
    private int currentAgility;
    private int baseResilience;
    private int currentResilience;
    private int baseMind;
    private int currentMind;
    private int baseSpirit;
    private int currentSpirit;

    private float baseHealth;
    private float currentHealth;
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

    public List<BaseAttack> attacks = new List<BaseAttack>();

    // ******************************************************
    // temporary until other energy sources are added to code 
    private float baseMP;
    private float currentMP;

    public float BaseMP
    {
        get { return baseMP; }
        set { baseMP = value; }
    }

    public float CurrentMP
    {
        get { return currentMP; }
        set { currentMP = value; }
    }
    // End of temporary section
    // ******************************************************

    // Name and description
    public string CharacterName
    {
        get { return characterName; }
        set { characterName = value; }
    }

    public string CharacterDescription
    {
        get { return characterDescription; }
        set { characterDescription = value; }
    }

    // Basic attributes
    public int BaseStrength
    {
        get { return baseStrength; }
        set { baseStrength = value; }
    }

    public int CurrentStrength
    {
        get { return currentStrength; }
        set { currentStrength = value; }
    }

    public int BaseAgility
    {
        get { return baseAgility; }
        set { baseAgility = value; }
    }

    public int CurrentAgility
    {
        get { return currentAgility; }
        set { currentAgility = value; }
    }

    public int BaseResilience
    {
        get { return baseResilience; }
        set { baseResilience = value; }
    }

    public int CurrentResilience
    {
        get { return currentResilience; }
        set { currentResilience = value; }
    }
    
    public int BaseMind
    {
        get { return baseMind; }
        set { baseMind = value; }
    }

    public int CurrentMind
    {
        get { return currentMind; }
        set { currentMind = value; }
    }

    public int BaseSpirit
    {
        get { return baseSpirit; }
        set { baseSpirit = value; }
    }

    public int CurrentSpirit
    {
        get { return currentSpirit; }
        set { currentSpirit = value; }
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

    // Health, attack and defense
    public float BaseHealth
    {
        get { return baseHealth; }
        set { baseHealth = value; }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

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
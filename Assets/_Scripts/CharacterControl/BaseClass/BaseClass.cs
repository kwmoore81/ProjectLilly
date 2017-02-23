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
    private int baseEndurance;
    private int currentEndurance;
    private int baseIntellect;
    private int currentIntellect;

    private float baseHP;
    private float currentHP;
    private float baseMP;       // Remove this later once fleshed out in classes
    private float currentMP;    // Remove this later once fleshed out in classes

    private float baseAttack;
    private float currentAttack;
    private float baseDefense;
    private float currentDefense;

    public List<BaseAttack> attacks = new List<BaseAttack>();

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

    public int BaseEndurance
    {
        get { return baseEndurance; }
        set { baseEndurance = value; }
    }

    public int CurrentEndurance
    {
        get { return currentEndurance; }
        set { currentEndurance = value; }
    }
    
    public int BaseIntellect
    {
        get { return baseIntellect; }
        set { baseIntellect = value; }
    }

    public int CurrentIntellect
    {
        get { return currentIntellect; }
        set { currentIntellect = value; }
    }

    // Health, attack and defense
    public float BaseHP
    {
        get { return baseHP; }
        set { baseHP = value; }
    }

    public float CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }

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

    public float BaseAttack
    {
        get { return baseAttack; }
        set { baseAttack = value; }
    }

    public float CurrentAttack
    {
        get { return currentAttack; }
        set { currentAttack = value; }
    }

    public float BaseDefense
    {
        get { return baseDefense; }
        set { baseDefense = value; }
    }

    public float CurrentDefense
    {
        get { return currentDefense; }
        set { currentDefense = value; }
    }
}
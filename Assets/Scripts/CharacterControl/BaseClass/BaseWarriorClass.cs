using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseWarriorClass : BaseClass
{
    public BaseWarriorClass()
    {
        CharacterName = "Warrior";
        CharacterDescription = "A fighter skilled in the use of sword and shield.";

        BaseStrength = 10;
        BaseAgility = 10;
        BaseEndurance = 10;
        BaseIntellect = 10;

        BaseHP = 100;
    }
}

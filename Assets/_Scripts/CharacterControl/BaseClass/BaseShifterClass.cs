using UnityEngine;
using System.Collections;

public class BaseShifterClass : BaseClass
{
    public BaseShifterClass()
    {
        CharacterName = "Shifter";
        CharacterDescription = "Mutates into various forms, depending on the nature of the terrain.";

        BaseStrength = 10;
        BaseAgility = 10;
        BaseEndurance = 10;
        BaseIntellect = 10;

        BaseHP = 100;
    }
}

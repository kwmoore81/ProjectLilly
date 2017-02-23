using UnityEngine;
using System.Collections;

public class BaseArcherClass : BaseClass
{
    public BaseArcherClass()
    {
        CharacterName = "Archer";
        CharacterDescription = "A fighter skilled in the use of bow and arrow.";

        BaseStrength = 10;
        BaseAgility = 10;
        BaseEndurance = 10;
        BaseIntellect = 10;

        BaseHP = 100;
    }
}

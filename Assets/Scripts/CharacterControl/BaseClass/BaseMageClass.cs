using UnityEngine;
using System.Collections;

public class BaseMageClass : BaseClass
{
    public BaseMageClass()
    {
        CharacterName = "Mage";
        CharacterDescription = "A spellcaster who harnesses the powers of nature.";

        BaseStrength = 10;
        BaseAgility = 10;
        BaseEndurance = 10;
        BaseIntellect = 10;

        BaseHP = 100;
    }
}

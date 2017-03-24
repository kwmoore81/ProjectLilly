using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldSceneChanger1 : MonoBehaviour {

    public GameObject overworldScene;
    public GameObject battleScene;

    public GameObject battleMaster;
    private OverWorldSceneChanger2 overWorldSceneChanger2;

    public GameObject DataBase;
    private CharacterStatsDB characterStatsDB;

    public GameObject activeCharacter;
    RPGCharacterControllerFREE characterController;
    Animator animator;

    public int gabiCurrentHealth;
    public int gabiCurrentResolve;

    public int arvandusCurrentHealth;
    public int arvanusCurrentStamina;

    public int quinnCurrentHealth;
    public int quinnCurrentFire;
    public int quinnCurrentEarth;
    public int quinnCurrentWater;

    public int currentAreaCorruption;
    public float characterMovementCounter;
    float randValue;
    public float encounterChance = 0.60f;
    public float encounterBuffer;
    public float maxTimeBeforeEncounter;

    void Start()
    {
        characterStatsDB = DataBase.GetComponent<CharacterStatsDB>();
        characterController = activeCharacter.GetComponent<RPGCharacterControllerFREE>();
        animator = activeCharacter.GetComponentInChildren<Animator>();
        overWorldSceneChanger2 = battleMaster.GetComponent<OverWorldSceneChanger2>();
    }

    public void SceneChange()
    {
            battleScene.gameObject.SetActive(true);
            overWorldSceneChanger2.UpdateFromBank();
            overworldScene.gameObject.SetActive(false);   
    }

    public void UpdateFromBank()
    {
        gabiCurrentHealth = characterStatsDB.gabiCurrentHealth;
        gabiCurrentResolve = characterStatsDB.gabiCurrentResolve;

        arvandusCurrentHealth = characterStatsDB.arvandusCurrentHealth;
        arvanusCurrentStamina = characterStatsDB.arvandusCurrentStamina;

        quinnCurrentHealth = characterStatsDB.quinnCurrentHealth;
        quinnCurrentFire = characterStatsDB.quinnCurrentFire;
        quinnCurrentEarth = characterStatsDB.quinnCurrentEarth;
        quinnCurrentWater = characterStatsDB.quinnCurrentWater;

        //currentAreaCorruption = databank.currentAreaCorruption;             
    }

    void Update()
    {
        if (animator.GetBool("Moving"))
        { 

        characterController.movementCounter += Time.deltaTime;
        characterController.maxMovmentCounter += Time.deltaTime;

            if (characterController.movementCounter >= encounterBuffer)
            {
                characterController.movementCounter = 0;
                randValue = Random.value;
                if (randValue < encounterChance)
                {
                    if (battleScene.gameObject.activeInHierarchy == false && overworldScene.gameObject.activeInHierarchy == true)
                    {
                        characterStatsDB.SendData1();
                        SceneChange();
                    }
                }
                else if (characterController.maxMovmentCounter >= maxTimeBeforeEncounter)
                {
                    if (battleScene.gameObject.activeInHierarchy == false && overworldScene.gameObject.activeInHierarchy == true)
                    {
                        characterStatsDB.SendData1();
                        SceneChange();
                    }
                }
            }           
        }
    }

}

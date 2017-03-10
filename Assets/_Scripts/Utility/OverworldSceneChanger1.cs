using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldSceneChanger1 : MonoBehaviour {

    public GameObject overworld1;
    public GameObject overworld2;
   
    public GameObject DataBankObj;
    private DataBank databank;

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
        databank = DataBankObj.GetComponent<DataBank>();
        characterController = activeCharacter.GetComponent<RPGCharacterControllerFREE>();
        animator = activeCharacter.GetComponentInChildren<Animator>();
    }

    public void SceneChange()
    {             
            overworld2.gameObject.SetActive(true);
            overworld1.gameObject.SetActive(false);   
    }

    public void UpdateFromBank()
    {
        gabiCurrentHealth = databank.gabiCurrentHealth;
        gabiCurrentResolve = databank.gabiCurrentResolve;

        arvandusCurrentHealth = databank.arvandusCurrentHealth;
        arvanusCurrentStamina = databank.arvanusCurrentStamina;

        quinnCurrentHealth = databank.quinnCurrentHealth;
        quinnCurrentFire = databank.quinnCurrentFire;
        quinnCurrentEarth = databank.quinnCurrentEarth;
        quinnCurrentWater = databank.quinnCurrentWater;

        currentAreaCorruption = databank.currentAreaCorruption;             
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
                    if (overworld2.gameObject.activeInHierarchy == false && overworld1.gameObject.activeInHierarchy == true)
                    {
                        databank.UpdateBank(gabiCurrentHealth, gabiCurrentResolve, arvandusCurrentHealth, arvanusCurrentStamina, quinnCurrentHealth, quinnCurrentFire, quinnCurrentEarth, quinnCurrentWater, currentAreaCorruption);
                        SceneChange();
                    }
                }
                else if (characterController.maxMovmentCounter >= maxTimeBeforeEncounter)
                {
                    if (overworld2.gameObject.activeInHierarchy == false && overworld1.gameObject.activeInHierarchy == true)
                    {
                        databank.UpdateBank(gabiCurrentHealth, gabiCurrentResolve, arvandusCurrentHealth, arvanusCurrentStamina, quinnCurrentHealth, quinnCurrentFire, quinnCurrentEarth, quinnCurrentWater, currentAreaCorruption);
                        SceneChange();
                    }
                }
            }           
        }
    }

}

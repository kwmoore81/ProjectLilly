using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldSceneChanger2 : MonoBehaviour {

    public GameObject overworld1;
    public GameObject overworld2;

    public GameObject DataBase;
    private CharacterStatsDB characterStatsDB;

    public int gabiCurrentHealth;
    public int gabiCurrentResolve;

    public int arvandusCurrentHealth;
    public int arvanusCurrentStamina;

    public int quinnCurrentHealth;
    public int quinnCurrentFire;
    public int quinnCurrentEarth;
    public int quinnCurrentWater;

    public int currentAreaCorruption;

    void Start()
    {
        characterStatsDB = DataBase.GetComponent<CharacterStatsDB>();
    }

    public void SceneChange()
    {     
            overworld1.gameObject.SetActive(true);
            overworld2.gameObject.SetActive(false);              
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (overworld2.gameObject.activeInHierarchy == true && overworld1.gameObject.activeInHierarchy == false)
            {
                //databank.UpdateBank(gabiCurrentHealth, gabiCurrentResolve, arvandusCurrentHealth, arvanusCurrentStamina, quinnCurrentHealth, quinnCurrentFire, quinnCurrentEarth, quinnCurrentWater, currentAreaCorruption);              
                SceneChange();
            }
        }
    }

}


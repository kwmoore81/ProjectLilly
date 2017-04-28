using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldSceneChanger2 : MonoBehaviour {

    public GameObject overworldScene;
    public GameObject battleScene;

    public GameObject overworldMaster;
    private OverworldSceneChanger1 overWorldSceneChanger1;

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

    public float currentAreaCorruption;

    void Start()
    {
        characterStatsDB = DataBase.GetComponent<CharacterStatsDB>();
        overWorldSceneChanger1 = overworldMaster.GetComponent<OverworldSceneChanger1>();
        Cursor.visible = true;
    }

    public void SceneChange()
    {
            overworldScene.gameObject.SetActive(true);
            overWorldSceneChanger1.UpdateFromBank();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Destroy(overWorldSceneChanger1.battleSceneTemp);                      
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

        currentAreaCorruption = characterStatsDB.currentAreaCorruption;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (battleScene.gameObject.activeInHierarchy == true && overworldScene.gameObject.activeInHierarchy == false)
            {
                characterStatsDB.SendData2();
                SceneChange();
            }
        }
    }

}


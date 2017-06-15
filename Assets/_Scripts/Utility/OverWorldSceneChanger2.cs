using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class OverWorldSceneChanger2 : MonoBehaviour
{

    public GameObject overworldScene;
    public GameObject battleScene;

    public GameObject overworldMaster;
    private OverworldSceneChanger1 overWorldSceneChanger1;

    public GameObject DataBase;
    private CharacterStatsDB characterStatsDB;

    public GameObject ThirdPersonCamera;
    private VolumetricFog volumetricFog;
    public PostProcessingBehaviour postProcessingBehavior;

    public GameObject fogTrigger;
    private FogTriggerScript fogTriggerScript;

    public float gabiCurrentHealth;
    public float gabiCurrentResolve;

    public float arvandusCurrentHealth;
    public float arvanusCurrentStamina;

    public float quinnCurrentHealth;
    public int quinnCurrentFire;
    public int quinnCurrentEarth;
    public int quinnCurrentWater;

    public float currentAreaCorruption;

    void Start()
    {
        characterStatsDB = DataBase.GetComponent<CharacterStatsDB>();
        overWorldSceneChanger1 = overworldMaster.GetComponent<OverworldSceneChanger1>();
        volumetricFog = ThirdPersonCamera.GetComponent<VolumetricFog>();
        Cursor.visible = true;
        fogTriggerScript = fogTrigger.GetComponent<FogTriggerScript>();
    }

    public void SceneChange()
    {
        overworldScene.gameObject.SetActive(true);
        characterStatsDB.SendData2();
        overWorldSceneChanger1.UpdateFromBank();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(overWorldSceneChanger1.battleSceneTemp);

        postProcessingBehavior.enabled = true;

        if (fogTriggerScript.fogActive == true)
        {
            volumetricFog.enabled = true;
        }
        else
        {
            volumetricFog.enabled = false;
        }

        
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
        //    if (Input.GetKeyDown(KeyCode.Tab))
        //    {
        //        if (battleScene.gameObject.activeInHierarchy == true && overworldScene.gameObject.activeInHierarchy == false)
        //        {
        //            characterStatsDB.SendData2();
        //            SceneChange();
        //        }
        //    }
        //}

    }
}


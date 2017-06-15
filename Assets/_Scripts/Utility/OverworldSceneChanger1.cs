using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class OverworldSceneChanger1 : MonoBehaviour {
    
    public GameObject overworldScene;
    public GameObject battleScene;
    public GameObject battleSceneTemp;
    public GameObject ForestBattlePrefab;
    public GameObject BossBattlePrefab;    

    public GameObject battleMaster;
    private OverWorldSceneChanger2 overWorldSceneChanger2;

    public GameObject DataBase;
    private CharacterStatsDB characterStatsDB;

    public GameObject thirdPersonControllerOBJ;
    private vThirdPersonController thirdPersonController;

    public GameObject transition_Canvas;
    private CameraBlurTest cameraBlur;

    public GameObject ThirdPersonCamera;
    private VolumetricFog volumetricFog;
    public PostProcessingBehaviour postProcessingBehavior;
    

    Animator animator;  
    
    public float gabiCurrentHealth;
    public float gabiCurrentResolve;
    private float gabiHealthMax = 960.0f;
    private float gabiResolveMax = 100;

    public float arvandusCurrentHealth;
    public float arvanusCurrentStamina;
    private float arvandusHealthMax = 520.0f;
    private float arvandusStaminaMax = 100;

    public float quinnCurrentHealth;
    public int quinnCurrentFire;
    public int quinnCurrentEarth;
    public int quinnCurrentWater;
    private float quinnHealthMax = 410.0f;
    private int quinnFireMax = 5;
    private int quinnEarthMax = 5;
    private int quinnWaterMax = 5;

    public float currentAreaCorruption;
    public float characterMovementCounter;
    float randValue;
    public float encounterChance = 0.60f;
    public float encounterBuffer;
    public float maxTimeBeforeEncounter;

    public bool battleToggle = true;
    public Vector3 playerLastPos;
    public Vector3 playerCurrentPos;
    float _time = 0;
    public float movementCounter = 0;
    public float maxMovmentCounter = 0;
    bool takeScreenShot = false;

    void Start()
    {
        characterStatsDB = DataBase.GetComponent<CharacterStatsDB>();
        thirdPersonController = thirdPersonControllerOBJ.GetComponent<vThirdPersonController>();
        animator = thirdPersonControllerOBJ.GetComponent<Animator>();
        overWorldSceneChanger2 = battleMaster.GetComponent<OverWorldSceneChanger2>();
        cameraBlur = transition_Canvas.GetComponent<CameraBlurTest>();
        volumetricFog = ThirdPersonCamera.GetComponent<VolumetricFog>();
    }

    public void SceneChange()
    {       
        volumetricFog.enabled = false;
        postProcessingBehavior.enabled = false;
        StartCoroutine(cameraBlur.FadeIn(cameraBlur.targetAlpha, cameraBlur.lerpSpeed));                      
    }

    public void DelayedSceenChange()
    {       
        overworldScene.gameObject.SetActive(false);
        battleSceneTemp = Object.Instantiate(ForestBattlePrefab, battleScene.transform);
        overWorldSceneChanger2.UpdateFromBank();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    
    public void BossSceneChange()
    {
        battleSceneTemp = Object.Instantiate(BossBattlePrefab, battleScene.transform);
        overWorldSceneChanger2.UpdateFromBank();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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

        currentAreaCorruption = characterStatsDB.currentAreaCorruption;             
    }

    void Update()
    {
        if (battleToggle)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {

                movementCounter += Time.deltaTime;
                maxMovmentCounter += Time.deltaTime;

                if (movementCounter >= encounterBuffer)
                {
                    movementCounter = 0;
                    randValue = Random.value;
                    if (randValue < encounterChance)
                    {
                        maxMovmentCounter = 0;
                        characterStatsDB.SendData1();
                        SceneChange();
                    }
                    else if (maxMovmentCounter >= maxTimeBeforeEncounter)
                    {
                        maxMovmentCounter = 0;
                        characterStatsDB.SendData1();
                        SceneChange();
                    }
                }
            }
            else if (_time < 2f)
            {

                _time = 0;
            }
            else if (_time < 2f)
            {
                _time += Time.deltaTime;
            }

            playerLastPos = playerCurrentPos;

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            characterStatsDB.SendData1();
            SceneChange();
        }
    }

    public void HealAll()
    {
        gabiCurrentHealth = gabiHealthMax;
        quinnCurrentHealth = quinnHealthMax;
        arvandusCurrentHealth = arvandusHealthMax;
    }

    public void ResourceRestore()
    {
        gabiCurrentResolve = gabiResolveMax;
        arvanusCurrentStamina = arvandusStaminaMax;
        quinnCurrentWater = quinnWaterMax;
        quinnCurrentFire = quinnFireMax;
        quinnCurrentEarth = quinnEarthMax;

    }
}

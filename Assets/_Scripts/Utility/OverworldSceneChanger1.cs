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

    public GameObject thirdPersonControllerOBJ;
    private vThirdPersonController thirdPersonController;
    Animator animator;
    //private vThirdPersonAnimator thirdPersonAnimator;
    
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

    
    public Vector3 playerLastPos;
    public Vector3 playerCurrentPos;
    float _time = 0;
    public float movementCounter = 0;
    public float maxMovmentCounter = 0;

    void Awake()
    {
        playerLastPos = thirdPersonControllerOBJ.transform.position;
    }
    void Start()
    {
        characterStatsDB = DataBase.GetComponent<CharacterStatsDB>();
        thirdPersonController = thirdPersonControllerOBJ.GetComponent<vThirdPersonController>();
        //thirdPersonAnimator = GetComponent<vThirdPersonAnimator>(); 
        animator = thirdPersonControllerOBJ.GetComponent<Animator>();
        overWorldSceneChanger2 = battleMaster.GetComponent<OverWorldSceneChanger2>();
        //playerPos = thirdPersonControllerOBJ.transform.position;
        //playerTempPos = playerPos;
    }

    public void SceneChange()
    {
            battleScene.gameObject.SetActive(true);
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

        //currentAreaCorruption = databank.currentAreaCorruption;             
    }

    void Update()
    {
        
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") !=0)
        {

            movementCounter += Time.deltaTime;
            maxMovmentCounter += Time.deltaTime;

            if (movementCounter >= encounterBuffer)
            {
                movementCounter = 0;
                randValue = Random.value;
                if (randValue < encounterChance)
                {
                    if (battleScene.gameObject.activeInHierarchy == false && overworldScene.gameObject.activeInHierarchy == true)
                    {
                        maxMovmentCounter = 0;
                        characterStatsDB.SendData1();
                        SceneChange();
                    }
                }
                else if (maxMovmentCounter >= maxTimeBeforeEncounter)
                {
                    if (battleScene.gameObject.activeInHierarchy == false && overworldScene.gameObject.activeInHierarchy == true)
                    {
                        maxMovmentCounter = 0;
                        characterStatsDB.SendData1();
                        SceneChange();
                    }
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

}

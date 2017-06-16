﻿
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour
{
    // Enums for tracking primary and secondary evironment elements
    public enum TerrainElementPrimary
    {
        FIRE, WATER, EARTH, WOOD, METAL
    }
    [HideInInspector]
    public TerrainElementPrimary terrainElementPrimary;

    public enum TerrainElementSecondary
    {
        FIRE, WATER, EARTH, WOOD, METAL
    }
    [HideInInspector]
    public TerrainElementSecondary terrainElementSecondary;

    // State engine for perfoming actions
    public enum ActionState
    {
        WAITING,        // Waiting for input
        RECEIVEACTION,  // Receive input
        PERFORMACTION,  // Perfrom action based on input
        CHECKFORDEAD,   // Check if any agents are dead
        WIN,            // Heroes won the battle
        LOSE            // Heroes lost the battle
    }
    public ActionState actionState;

    // State engine for handling hero input
    public enum HeroUI
    {
        ACTIVATE,   // Turn on hero UI
        IDLE,       // Waiting between actions
        INPUT1,     // Button 1 selected
        INPUT2,     // Button 2 selected
        DONE        // Input has been completed
    }
    public HeroUI heroInput;

    private TurnOrderHandler heroChoice;

    [Header("Animators")]
    Animator animatorCamera;
    public Animator animPanelGabi;
    public Animator animPanelQuinn;
    public Animator animPanelArvandus;
    //public Animator animButtonGabi;
    //public Animator animButtonQuinn;
    //public Animator animButtonArvandus;

    [Header("Battle Control Lists")]
    public List<TurnOrderHandler> activeAgentList = new List<TurnOrderHandler>();
    public List<GameObject> heroesInBattle = new List<GameObject>();
    public List<GameObject> enemiesInBattle = new List<GameObject>();
    public List<GameObject> deadHeroes = new List<GameObject>();
    public List<GameObject> deadEnemies = new List<GameObject>();
    public List<GameObject> heroesToManage = new List<GameObject>();
    public List<GameObject> upperRowEnemies = new List<GameObject>();
    public List<GameObject> lowerRowEnemies = new List<GameObject>();

    [Header("UI Panels")]
    public GameObject fadeInPanel;
    public GameObject actionPanel;
    public GameObject earthPanel;
    public GameObject firePanel;
    public GameObject waterPanel;
    public GameObject attackPanel;
    public GameObject utilityPanel;
    public GameObject failedActionPanel;
    public GameObject enemySelectPanel;
    public GameObject heroSelectPanel;
    public GameObject corruptionMeter;
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public GameObject endGamePanel;

    [Header("UI Panel Spacers")]
    public Transform actionSpacer;
    public Transform earthSpacer;
    public Transform fireSpacer;
    public Transform waterSpacer;
    public Transform attackSpacer;
    public Transform utilitySpacer;

    [Header("UI Buttons")]
    public GameObject actionButton;
    public GameObject defendButton;
    public GameObject meleeButton;
    public GameObject spellButton;
    public GameObject utilityButton;
    public GameObject enemyButton;

    [Header("Other Stuff")]
    //Button Lists
    private List<GameObject> attackButtons = new List<GameObject>();
    private List<GameObject> enemyButtonList = new List<GameObject>();

    // Battle start delay
    public bool startBattle = false;
    private float startDelay = 5f;
    private float startDelayTimer;
    public bool pauseBattle = false;

    // Battle end delay
    private float endDelay = 5f;
    private float endDelayTimer;

    // Fade In Properties
    public float fadeInTimer = 0.0f, fadeInLength = 10f;
    public float endGameLength = 10f;
    Color fadeInColorStart, fadeInColorEnd;

    //Scene Changer
    private OverWorldSceneChanger2 overWorldSceneChanger2;
    [HideInInspector]
    public bool battleResultWait = false;

    // Animation control variables
    public bool gabiTurn = false;
    public bool quinnTurn = false;
    public bool arvandusTurn = false;
    public bool heroPanelActive = false;
    public bool heroPanelOn = false;
    private bool expandTriggered = false;
    private bool contractTriggered = false;

    void Start ()
	{
        SpawnEnemies();

        animatorCamera = GameObject.Find("MainCamera").GetComponentInChildren<Animator>();

        terrainElementPrimary = TerrainElementPrimary.EARTH;
        terrainElementSecondary = TerrainElementSecondary.WOOD;

        actionState = ActionState.WAITING;
        heroInput = HeroUI.ACTIVATE;
        enemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        heroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        actionPanel.SetActive(false);
        attackPanel.SetActive(false);
        earthPanel.SetActive(false);
        firePanel.SetActive(false);
        earthPanel.SetActive(false);
        waterPanel.SetActive(false);
        utilityPanel.SetActive(false);
        failedActionPanel.SetActive(false);
        enemySelectPanel.SetActive(false);
        heroSelectPanel.SetActive(false);
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
        endGamePanel.SetActive(false);

        startDelayTimer = startDelay;
        endDelayTimer = endDelay;
        
        overWorldSceneChanger2 = GameObject.Find("BattleMaster").GetComponent<OverWorldSceneChanger2>();

        corruptionMeter.GetComponent<CorruptionMeter>().currentCorruption = overWorldSceneChanger2.currentAreaCorruption;
    }
	
	void Update ()
	{
        if (startBattle)
        {
            if (!pauseBattle)
            {
                CheckActionState();
                CheckHeroInputState();
            }
            else
            {
                if (fadeInTimer < endGameLength)
                {
                    fadeInTimer += Time.deltaTime;
                }
                else
                {
                    endGamePanel.SetActive(true);
                }
            }
        }
        else
        {
            if (startDelayTimer <= 0)
            {
                startBattle = true;
            }
            else
            {
                if (fadeInTimer > fadeInLength)
                {
                    fadeInTimer += Time.deltaTime;
                }
                else
                {
                    endGamePanel.SetActive(true);
                }

                startDelayTimer -= Time.deltaTime;
            }
        }
    }

    void SpawnEnemies()
    {
        // Make sure all upper and lower row enemies are inactive
        for (int i = 0; i < upperRowEnemies.Count; i++)
        {
            upperRowEnemies[i].SetActive(false);
        }

        for (int i = 0; i < lowerRowEnemies.Count; i++)
        {
            lowerRowEnemies[i].SetActive(false);
        }

        // Spawn random upper row enemy
        int randomSpawn1;
        randomSpawn1 = Random.Range(0, upperRowEnemies.Count);
        upperRowEnemies[randomSpawn1].SetActive(true);

        // Spawn random lower row enemy
        int randomSpawn2;
        randomSpawn2 = Random.Range(0, lowerRowEnemies.Count);
        lowerRowEnemies[randomSpawn2].SetActive(true);
    }

    void CheckActionState()
    {
        switch (actionState)
        {
            case (ActionState.WAITING):
                if (activeAgentList.Count > 0) actionState = ActionState.RECEIVEACTION;
                break;
            case (ActionState.RECEIVEACTION):
                ReceiveAction();
                break;
            case (ActionState.PERFORMACTION):
                // Idle state
                break;
            case (ActionState.CHECKFORDEAD):
                CheckForDead();
                break;
            case (ActionState.WIN):
                WinBattle();
                break;
            case (ActionState.LOSE):
                LoseBattle();
                break;
        }
    }

    void CheckHeroInputState()
    {
        switch (heroInput)
        {
            case (HeroUI.ACTIVATE):
                ActivateHero();
                break;
            case (HeroUI.IDLE):
                // Idle state
                break;
            case (HeroUI.INPUT1):

                break;
            case (HeroUI.INPUT2):

                break;
            case (HeroUI.DONE):
                HeroInputDone();
                break;
        }
    }

    void ActivateHero()
    {
        if (heroesToManage.Count > 0)
        {
            heroesToManage[0].transform.FindChild("Selector").gameObject.SetActive(true);
            heroChoice = new TurnOrderHandler();

            ActivateHeroPanels();

            actionPanel.SetActive(true);
            CreateActionButtons();

            heroInput = HeroUI.IDLE;
        }
    }

    void ReceiveAction()
    {
        GameObject agent = GameObject.Find(activeAgentList[0].activeAgent);

        if (agent.transform.tag == "Enemy")
        {
            EnemyController enemyControl = agent.GetComponent<EnemyController>();

            for (int i = 0; i < heroesInBattle.Count; i++)
            {
                if (activeAgentList[0].targetGO == heroesInBattle[i])
                {
                    enemyControl.currentState = EnemyController.EnemyState.ACTION;
                    break;
                }
                else
                {
                    activeAgentList[0].targetGO = heroesInBattle[Random.Range(0, heroesInBattle.Count)];
                    enemyControl.currentState = EnemyController.EnemyState.ACTION;
                }
            }
        }
        else if (agent.transform.tag == "Hero")
        {
            HeroController heroControl = agent.GetComponent<HeroController>();
            heroControl.enemyToAttack = activeAgentList[0].targetGO;
            heroControl.currentState = HeroController.HeroState.ACTION;
        }

        actionState = ActionState.PERFORMACTION;
    }

    // Send hero and enemy choices to the active agent list
    public void ActionCollector(TurnOrderHandler _agentInfo)
    {
        activeAgentList.Add(_agentInfo);
    }

    // Add dead characters to the appropriate list and check if there are any left alive
    void CheckForDead()
    {
        if (heroesInBattle.Count <= 0)
        {
            actionState = ActionState.LOSE;
            battleResultWait = true;
        }
        else if (enemiesInBattle.Count <= 0)
        {

            actionState = ActionState.WIN;
            battleResultWait = true;
        }
        else
        {
            ClearAttackPanel();
            heroInput = HeroUI.ACTIVATE;
            actionState = ActionState.WAITING;
            deadHeroes.AddRange(GameObject.FindGameObjectsWithTag("DeadHero"));
            deadEnemies.AddRange(GameObject.FindGameObjectsWithTag("DeadEnemy"));
        }
    }

    public void ActionInput()
    {
        heroChoice.activeAgent = heroesToManage[0].name;
        heroChoice.agentGO = heroesToManage[0];
        heroChoice.chosenAttack = heroesToManage[0].GetComponent<HeroController>().hero.attacks[0];

        if (heroChoice.chosenAttack.partyTargeting)
        {
            ContractHeroPanels();
            ForceWaitTime(1);
            heroSelectPanel.SetActive(true);
        }
        else
        {
            enemySelectPanel.SetActive(true);
        }
    }

    public void EnemySelectInput(GameObject chosenEnemy)
    {
        heroChoice.targetGO = chosenEnemy;
        heroInput = HeroUI.DONE;
    }

    public void HeroSelectInput(GameObject chosenHero)
    {
        heroChoice.targetGO = chosenHero;
        heroInput = HeroUI.DONE;
    }

    public void HeroInputDone()
    {
        activeAgentList.Add(heroChoice);
        ClearAttackPanel();

        heroesToManage[0].transform.FindChild("Selector").gameObject.SetActive(false);

        ContractHeroPanels();
        ForceWaitTime(1);

        heroesToManage.RemoveAt(0);
        heroInput = HeroUI.ACTIVATE;
    }

    void ResetAttackPanels()
    {
        enemySelectPanel.SetActive(false);
        heroSelectPanel.SetActive(false);
        earthPanel.SetActive(false);
        firePanel.SetActive(false);
        waterPanel.SetActive(false);
        attackPanel.SetActive(false);
        utilityPanel.SetActive(false);
        failedActionPanel.SetActive(false);
    }

    void ClearAttackPanel()
    {
        enemySelectPanel.SetActive(false);
        heroSelectPanel.SetActive(false);
        actionPanel.SetActive(false);
        earthPanel.SetActive(false);
        firePanel.SetActive(false);
        waterPanel.SetActive(false);
        attackPanel.SetActive(false);
        utilityPanel.SetActive(false);
        failedActionPanel.SetActive(false);

        foreach (GameObject attackButton in attackButtons)
        {
            Destroy(attackButton);
        }
        attackButtons.Clear();
    }

    void ActivateHeroPanels()
    {
        if (heroesToManage[0].gameObject.GetComponent<HeroController>().name == "Gabi")
        {
            gabiTurn = true;
        }
        else if (heroesToManage[0].gameObject.GetComponent<HeroController>().name == "Quinn")
        {
            quinnTurn = true;
        }
        else if (heroesToManage[0].gameObject.GetComponent<HeroController>().name == "Arvandus")
        {
            arvandusTurn = true;
        }

        expandTriggered = false;
        contractTriggered = false;
        ExpandHeroPanels();
    }

    void ExpandHeroPanels()
    {
        if (!expandTriggered)
        {
            if (gabiTurn)
            {
                animPanelGabi.SetTrigger("expand");
                //animButtonGabi.SetTrigger("expand");
                expandTriggered = true;
            }
            else if (quinnTurn)
            {
                animPanelGabi.SetTrigger("gabiUp");
                animPanelQuinn.SetTrigger("expand");
                //animButtonGabi.SetTrigger("gabiUp");
                //animButtonQuinn.SetTrigger("expand");
                expandTriggered = true;
            }
            else if (arvandusTurn)
            {
                animPanelGabi.SetTrigger("gabiUp");
                animPanelQuinn.SetTrigger("quinnUp");
                animPanelArvandus.SetTrigger("expand");
                //animButtonGabi.SetTrigger("gabiUp");
                //animButtonQuinn.SetTrigger("quinnUp");
                //animButtonArvandus.SetTrigger("expand");
                expandTriggered = true;
            }
        }
    }

    void ContractHeroPanels()
    {
        if (!contractTriggered)
        {
            if (gabiTurn)
            {
                animPanelGabi.SetTrigger("minimize");
                //animButtonGabi.SetTrigger("minimize");
                contractTriggered = true;
                gabiTurn = false;
            }
            else if (quinnTurn)
            {
                animPanelGabi.SetTrigger("gabiDown");
                animPanelQuinn.SetTrigger("minimize");
                //animButtonGabi.SetTrigger("gabiDown");
                //animButtonQuinn.SetTrigger("minimize");
                contractTriggered = true;
                quinnTurn = false;
            }
            else if (arvandusTurn)
            {
                animPanelGabi.SetTrigger("gabiDown");
                animPanelQuinn.SetTrigger("quinnDown");
                animPanelArvandus.SetTrigger("minimize");
                //animButtonGabi.SetTrigger("gabiDown");
                //animButtonQuinn.SetTrigger("quinnDown");
                //animButtonArvandus.SetTrigger("minimize");
                contractTriggered = true;
                arvandusTurn = false;
            }
        }
    }
    
    public IEnumerator FailedActionNotification(string _resourceType)
    {
        Text failedActionText = failedActionPanel.transform.FindChild("text").gameObject.GetComponent<Text>();
        failedActionText.text = _resourceType;
        failedActionPanel.SetActive(true);
        yield return new WaitForSeconds(3);

        failedActionPanel.SetActive(false);
        yield return null;
    }

    // Create action buttons
    void CreateActionButtons()
    {
        // Create attack buttons
        if (heroesToManage[0].GetComponent<HeroController>().hero.attacks.Count > 0)
        {
            // Attack Button
            GameObject attackButton = Instantiate(actionButton) as GameObject;
            Text attackButtonText = attackButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
            attackButtonText.text = "Attack";
            attackButton.GetComponent<Button>().onClick.AddListener(() => AttackInput());
            attackButton.transform.SetParent(actionSpacer, false);
            attackButtons.Add(attackButton);

            foreach (AttackData attack in heroesToManage[0].GetComponent<HeroController>().hero.attacks)
            {
                GameObject meleeBtn = Instantiate(meleeButton) as GameObject;
                Text attackTypeButtonText = meleeBtn.transform.FindChild("Text").gameObject.GetComponent<Text>();
                attackTypeButtonText.text = attack.attackName;
                MeleeAttackButton meleeAttackButton = meleeBtn.GetComponent<MeleeAttackButton>();
                meleeAttackButton.meleeAttack = attack;
                meleeBtn.transform.SetParent(attackSpacer, false);
                attackButtons.Add(meleeBtn);
            }
        }

        // Create fire spell buttons
        if (heroesToManage[0].GetComponent<HeroController>().hero.fireSpells.Count > 0)
        {
            // Magic Button
            GameObject magicButton = Instantiate(actionButton) as GameObject;
            Text magicButtonText = magicButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
            magicButtonText.text = "Fire Spells";
            magicButton.GetComponent<Button>().onClick.AddListener(() => MagicInput("Fire"));
            magicButton.transform.SetParent(actionSpacer, false);
            attackButtons.Add(magicButton);

            // Spell Buttons
            foreach (AttackData magicAttack in heroesToManage[0].GetComponent<HeroController>().hero.fireSpells)
            {
                GameObject spellBtn = Instantiate(spellButton) as GameObject;
                Text spellButtonText = spellBtn.transform.FindChild("Text").gameObject.GetComponent<Text>();
                spellButtonText.text = magicAttack.attackName;
                SpellCastButton spellCastButton = spellBtn.GetComponent<SpellCastButton>();
                spellCastButton.spellToCast = magicAttack;
                spellBtn.transform.SetParent(fireSpacer, false);
                attackButtons.Add(spellBtn);
            }
        }

        // Create water spell buttons
        if (heroesToManage[0].GetComponent<HeroController>().hero.waterSpells.Count > 0)
        {
            // Magic Button
            GameObject magicButton = Instantiate(actionButton) as GameObject;
            Text magicButtonText = magicButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
            magicButtonText.text = "Water Spells";
            magicButton.GetComponent<Button>().onClick.AddListener(() => MagicInput("Water"));
            magicButton.transform.SetParent(actionSpacer, false);
            attackButtons.Add(magicButton);

            // Spell Buttons
            foreach (AttackData magicAttack in heroesToManage[0].GetComponent<HeroController>().hero.waterSpells)
            {
                GameObject spellBtn = Instantiate(spellButton) as GameObject;
                Text spellButtonText = spellBtn.transform.FindChild("Text").gameObject.GetComponent<Text>();
                spellButtonText.text = magicAttack.attackName;
                SpellCastButton spellCastButton = spellBtn.GetComponent<SpellCastButton>();
                spellCastButton.spellToCast = magicAttack;
                spellBtn.transform.SetParent(waterSpacer, false);
                attackButtons.Add(spellBtn);
            }
        }

        // Create earth spell buttons
        if (heroesToManage[0].GetComponent<HeroController>().hero.earthSpells.Count > 0)
        {
            // Magic Button
            GameObject magicButton = Instantiate(actionButton) as GameObject;
            Text magicButtonText = magicButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
            magicButtonText.text = "Earth Spells";
            magicButton.GetComponent<Button>().onClick.AddListener(() => MagicInput("Earth"));
            magicButton.transform.SetParent(actionSpacer, false);
            attackButtons.Add(magicButton);

            // Spell Buttons
            foreach (AttackData magicAttack in heroesToManage[0].GetComponent<HeroController>().hero.earthSpells)
            {
                GameObject spellBtn = Instantiate(spellButton) as GameObject;
                Text spellButtonText = spellBtn.transform.FindChild("Text").gameObject.GetComponent<Text>();
                spellButtonText.text = magicAttack.attackName;
                SpellCastButton spellCastButton = spellBtn.GetComponent<SpellCastButton>();
                spellCastButton.spellToCast = magicAttack;
                spellBtn.transform.SetParent(earthSpacer, false);
                attackButtons.Add(spellBtn);
            }
        }

        // Create utility buttons
        if (heroesToManage[0].GetComponent<HeroController>().hero.utility.Count > 0)
        {
            // Magic Button
            GameObject utilButton = Instantiate(actionButton) as GameObject;
            Text utilityButtonText = utilButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
            utilityButtonText.text = "Utility";
            utilButton.GetComponent<Button>().onClick.AddListener(() => UtilityInput());
            utilButton.transform.SetParent(actionSpacer, false);
            attackButtons.Add(utilButton);

            // Utility Buttons
            foreach (AttackData utilityAction in heroesToManage[0].GetComponent<HeroController>().hero.utility)
            {
                GameObject utilityBtn = Instantiate(utilityButton) as GameObject;
                Text utilityActionButtonText = utilityBtn.transform.FindChild("Text").gameObject.GetComponent<Text>();
                utilityActionButtonText.text = utilityAction.attackName;
                UtilityButton utilityActionButton = utilityBtn.GetComponent<UtilityButton>();
                utilityActionButton.utilityToUse = utilityAction;
                utilityBtn.transform.SetParent(utilitySpacer, false);
                attackButtons.Add(utilityBtn);
            }
        }

        // Create item buttons


        // Create defend button
        if (heroesToManage[0].GetComponent<HeroController>().hero.defend.Count > 0)
        {
            GameObject defendButton = Instantiate(actionButton) as GameObject;
            Text defendButtonText = defendButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
            defendButtonText.text = "Defend";
            defendButton.GetComponent<Button>().onClick.AddListener(() => DefendInput());
            defendButton.transform.SetParent(actionSpacer, false);
            attackButtons.Add(defendButton);
        }
    }

    public void AttackInput()
    {
        ResetAttackPanels();
        attackPanel.SetActive(true);
    }

    public void MeleeInput(AttackData chosenAttack)
    {
        heroChoice.activeAgent = heroesToManage[0].name;
        heroChoice.agentGO = heroesToManage[0];
        heroChoice.chosenAttack = chosenAttack;

        enemySelectPanel.SetActive(true);
        enemyButtonsControl();
    }

    public void MagicInput(string _magicType)
    {
        ResetAttackPanels();

        if (_magicType == "Fire")
        {
            firePanel.SetActive(true);
        }
        else if (_magicType == "Water")
        {
            waterPanel.SetActive(true);
        }
        else if (_magicType == "Earth")
        {
            earthPanel.SetActive(true);
        }
    }

    public void SpellInput(AttackData chosenSpell)
    {
        heroChoice.activeAgent = heroesToManage[0].name;
        heroChoice.agentGO = heroesToManage[0];
        heroChoice.chosenAttack = chosenSpell;

        if (heroChoice.chosenAttack.attackType == AttackData.AttackType.RESTORE)
        {
            heroChoice.targetGO = heroesToManage[0];
            heroInput = HeroUI.DONE;
        }
        else if (heroChoice.chosenAttack.partyTargeting)
        {
            ContractHeroPanels();
            ForceWaitTime(.25f);
            heroSelectPanel.SetActive(true);
        }
        else
        {
            enemySelectPanel.SetActive(true);
            enemyButtonsControl();
        }
    }

    public void UtilityInput()
    {
        ResetAttackPanels();
        utilityPanel.SetActive(true);
    }

    public void SetUtilityInput(AttackData _chosenUtility)
    {
        heroChoice.activeAgent = heroesToManage[0].name;
        heroChoice.agentGO = heroesToManage[0];
        heroChoice.chosenAttack = _chosenUtility;

        //utilityPanel.SetActive(false);
        if (heroChoice.chosenAttack.partyTargeting)
        {
            ContractHeroPanels();
            ForceWaitTime(1);
            heroSelectPanel.SetActive(true);
        }
        else
        {
            enemySelectPanel.SetActive(true);
            enemyButtonsControl();
        }
    }

    public void DefendInput()
    {
        AttackData _chosenDefend = heroesToManage[0].GetComponent<HeroController>().hero.defend[0];

        heroChoice.activeAgent = heroesToManage[0].name;
        heroChoice.agentGO = heroesToManage[0];
        heroChoice.chosenAttack = _chosenDefend;
        heroChoice.targetGO = heroesToManage[0];
        heroInput = HeroUI.DONE;
    }

    public void enemyButtonsControl()
    {
        for (int i = 0; i < enemiesInBattle.Count; i++)
        {
            enemiesInBattle[i].GetComponent<EnemyController>().enemyActionControl.EnemyPanelButtonOn();
        }

        for (int i = 0; i < deadEnemies.Count; i++)
        {
            deadEnemies[i].GetComponent<EnemyController>().enemyActionControl.EnemyPanelButtonOff();
        }
    }

    public void enemyButtonsOff()
    {

    }

    void WinBattle()
    {
        for (int i = 0; i < heroesInBattle.Count; i++)
        {
            heroesInBattle[i].GetComponent<HeroController>().currentState = HeroController.HeroState.IDLE;
        }   

        victoryPanel.SetActive(true);

        if (!battleResultWait)
        {
            victoryPanel.SetActive(false);

            // Write current stats to database
            overWorldSceneChanger2.currentAreaCorruption = corruptionMeter.GetComponent<CorruptionMeter>().currentCorruption;

            for (int i = 0; i < heroesInBattle.Count; i++)
            {
                heroesInBattle[i].GetComponent<HeroController>().heroActionControl.WriteStats();
            }

            overWorldSceneChanger2.SceneChange();
        }
    }

    void LoseBattle()
    {
        for (int i = 0; i < enemiesInBattle.Count; i++)
        {
            enemiesInBattle[i].GetComponent<EnemyController>().currentState = EnemyController.EnemyState.IDLE;
        }

        defeatPanel.SetActive(true);

        if (!battleResultWait)
        {
            defeatPanel.SetActive(false);

            // Write current stats to database
            overWorldSceneChanger2.currentAreaCorruption = corruptionMeter.GetComponent<CorruptionMeter>().currentCorruption;

            for (int i = 0; 0 < heroesInBattle.Count; i++)
            {
                heroesInBattle[i].GetComponent<HeroController>().heroActionControl.WriteStats();
            }

            overWorldSceneChanger2.SceneChange();
        }
    }

    void ForceWaitTime(float _timer)
    {
        while (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour
{
    Animator animatorCamera;
    
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

    [Header("Battle Control Lists")]
    public List<TurnOrderHandler> activeAgentList = new List<TurnOrderHandler>();
    public List<GameObject> heroesInBattle = new List<GameObject>();
    public List<GameObject> enemiesInBattle = new List<GameObject>();
    public List<GameObject> deadHeroes = new List<GameObject>();
    public List<GameObject> deadEnemies = new List<GameObject>();
    public List<GameObject> heroesToManage = new List<GameObject>();

    [Header("UI Panels")]
    public GameObject fadeInPanel;
    public GameObject actionPanel;
    public GameObject earthPanel;
    public GameObject firePanel;
    public GameObject waterPanel;
    public GameObject attackPanel;
    public GameObject utilityPanel;
    public GameObject enemySelectPanel;
    public GameObject corruptionMeter;

    [Header("UI Panel Spacers")]
    public Transform spacer;
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
    private float startDelay = 3.25f;
    private float startDelayTimer;
    public bool pauseBattle = false;

    // Battle end delay
    private float endDelay = 5f;
    private float endDelayTimer;

    // Fade In Properties
    public float fadeInTimer = 0.0f, fadeInLength = 2f;
    Color fadeInColorStart, fadeInColorEnd;

    void Start ()
	{
        animatorCamera = GameObject.Find("MainCamera").GetComponentInChildren<Animator>();

        fadeInPanel.SetActive(true);
        fadeInColorStart = new Color(0, 0, 0, 1);
        fadeInColorEnd = new Color(fadeInColorStart.r, fadeInColorStart.g, fadeInColorStart.b, 0);


        actionState = ActionState.WAITING;
        heroInput = HeroUI.ACTIVATE;
        enemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        heroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        actionPanel.SetActive(false);
        earthPanel.SetActive(false);
        firePanel.SetActive(false);
        earthPanel.SetActive(false);
        waterPanel.SetActive(false);
        utilityPanel.SetActive(false);
        enemySelectPanel.SetActive(false);

        EnemySelectionButtons();

        startDelayTimer = startDelay;
        endDelayTimer = endDelay;
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
        }
        else
        {
            if (startDelayTimer <= 0)
            {
                startBattle = true;
            }
            else
            {
                if (fadeInTimer < fadeInLength)
                {
                    fadeInPanel.GetComponent<Image>().color = Color.Lerp(fadeInColorStart, fadeInColorEnd, fadeInTimer / fadeInLength);
                    fadeInTimer += Time.deltaTime;
                }
                else
                {
                    fadeInPanel.SetActive(false);
                }

                startDelayTimer -= Time.deltaTime;
            }
        }
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
                // Idle
                break;
            case (ActionState.CHECKFORDEAD):
                CheckForDead();
                break;
            case (ActionState.WIN):
                Debug.Log("You win!");
                WinBattle();
                break;
            case (ActionState.LOSE):
                Debug.Log("You lose!");
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

    public void ActionCollector(TurnOrderHandler _agentInfo)
    {
        activeAgentList.Add(_agentInfo);
    }

    void CheckForDead()
    {
        if (heroesInBattle.Count <= 0)
        {
            actionState = ActionState.LOSE;
        }
        else if (enemiesInBattle.Count <= 0)
        {
            actionState = ActionState.WIN;
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

    public void EnemySelectionButtons()
    {
        // Cleanup buttons and button list
        foreach (GameObject enemyBTN in enemyButtonList)
        {
            Destroy(enemyBTN);
        }
        enemyButtonList.Clear();

        // Create enemy buttons
        foreach (GameObject enemy in enemiesInBattle)
        {
            GameObject newButton = Instantiate(enemyButton) as GameObject;
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

            EnemyController currentEnemy = enemy.GetComponent<EnemyController>();

            Text buttonText = newButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
            buttonText.text = currentEnemy.name;

            button.enemyPrefab = enemy;

            newButton.transform.SetParent(spacer);
            enemyButtonList.Add(newButton);
        }
    }

    public void ActionInput()
    {
        heroChoice.activeAgent = heroesToManage[0].name;
        heroChoice.agentGO = heroesToManage[0];
        heroChoice.chosenAttack = heroesToManage[0].GetComponent<HeroController>().hero.attacks[0];
        enemySelectPanel.SetActive(true);
    }

    public void EnemySelectInput(GameObject chosenEnemy)
    {
        heroChoice.targetGO = chosenEnemy;
        heroInput = HeroUI.DONE;
    }

    public void HeroInputDone()
    {
        activeAgentList.Add(heroChoice);
        ClearAttackPanel();

        heroesToManage[0].transform.FindChild("Selector").gameObject.SetActive(false);
        heroesToManage.RemoveAt(0);
        heroInput = HeroUI.ACTIVATE;
    }

    void ClearAttackPanel()
    {
        enemySelectPanel.SetActive(false);
        actionPanel.SetActive(false);
        earthPanel.SetActive(false);
        firePanel.SetActive(false);
        waterPanel.SetActive(false);
        attackPanel.SetActive(false);
        utilityPanel.SetActive(false);

        foreach (GameObject attackButton in attackButtons)
        {
            Destroy(attackButton);
        }
        attackButtons.Clear();
    }

    // TODO: Modify this for specific classes.  Should it be in the individual class controllers?
    void CreateActionButtons()
    {
        // Create attack buttons
        if (heroesToManage[0].GetComponent<HeroController>().hero.attacks.Count > 1)
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
        else if (heroesToManage[0].GetComponent<HeroController>().hero.attacks.Count == 1)
        {
            GameObject attackButton = Instantiate(actionButton) as GameObject;
            Text attackButtonText = attackButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
            attackButtonText.text = "Attack";
            attackButton.GetComponent<Button>().onClick.AddListener(() => ActionInput());
            attackButton.transform.SetParent(actionSpacer, false);
            attackButtons.Add(attackButton);
        }

        // Create fire spell buttons
        if (heroesToManage[0].GetComponent<HeroController>().hero.fireSpells.Count > 1)
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
        if (heroesToManage[0].GetComponent<HeroController>().hero.waterSpells.Count > 1)
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
        if (heroesToManage[0].GetComponent<HeroController>().hero.earthSpells.Count > 1)
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
        if (heroesToManage[0].GetComponent<HeroController>().hero.utility.Count > 1)
        {
            // Magic Button
            GameObject utilityButton = Instantiate(actionButton) as GameObject;
            Text utilityButtonText = utilityButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
            utilityButtonText.text = "Earth Spells";
            utilityButton.GetComponent<Button>().onClick.AddListener(() => MagicInput("Earth"));
            utilityButton.transform.SetParent(actionSpacer, false);
            attackButtons.Add(utilityButton);

            // Spell Buttons
            foreach (ActionData utilityAction in heroesToManage[0].GetComponent<HeroController>().hero.utility)
            {
                GameObject utilityBtn = Instantiate(utilityButton) as GameObject;
                Text utilityActionButtonText = utilityBtn.transform.FindChild("Text").gameObject.GetComponent<Text>();
                utilityActionButtonText.text = utilityAction.actionName;
                UtilityButton utilityActionButton = utilityBtn.GetComponent<UtilityButton>();
                // Fix this line
                //utilityActionButton.blah = utilityAction;
                utilityBtn.transform.SetParent(utilitySpacer, false);
                attackButtons.Add(utilityBtn);
            }
        }

        // Create item buttons


        //Create defend button
        //if (heroesToManage[0].GetComponent<HeroController>().canDefend)
        //{
        //    GameObject defendButton = Instantiate(actionButton) as GameObject;
        //    Text defendButtonText = defendButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
        //    defendButtonText.text = "Defend";
        //    defendButton.GetComponent<Button>().onClick.AddListener(() => DefendInput());
        //    defendButton.transform.SetParent(actionSpacer, false);
        //    attackButtons.Add(defendButton);
        //}
    }

    public void AttackInput()
    {
        attackPanel.SetActive(true);
    }

    public void MeleeInput(AttackData chosenAttack)
    {
        heroChoice.activeAgent = heroesToManage[0].name;
        heroChoice.agentGO = heroesToManage[0];
        heroChoice.chosenAttack = chosenAttack;

        attackPanel.SetActive(false);
        enemySelectPanel.SetActive(true);
    }

    public void MagicInput(string _magicType)
    {
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

        earthPanel.SetActive(false);
        firePanel.SetActive(false);
        waterPanel.SetActive(false);
        enemySelectPanel.SetActive(true);
    }

    public void UtilityInput()
    {
        //utilityPanel.SetActive(true);
    }

    public void SetUtilityInput(AttackData chosenUtility)
    {
        heroChoice.activeAgent = heroesToManage[0].name;
        heroChoice.agentGO = heroesToManage[0];
        heroChoice.chosenAttack = chosenUtility;

        //utilityPanel.SetActive(false);
        //enemySelectPanel.SetActive(true);
    }

    public void DefendInput()
    {
        heroChoice.activeAgent = heroesToManage[0].name;
        heroChoice.agentGO = heroesToManage[0];
        heroChoice.agentGO.GetComponent<HeroController>().isBlocking = true;
    }

    void WinBattle()
    {
        for (int i = 0; i < heroesInBattle.Count; i++)
        {
            heroesInBattle[i].GetComponent<HeroController>().currentState = HeroController.HeroState.IDLE;
        }
    }

    void LoseBattle()
    {
        for (int i = 0; i < enemiesInBattle.Count; i++)
        {
            enemiesInBattle[i].GetComponent<EnemyController>().currentState = EnemyController.EnemyState.IDLE;
        }
    }
}

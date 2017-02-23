﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour
{
    Animator animatorActionPanel;
    Animator animatorTargetPanel;
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

    public List<TurnOrderHandler> activeAgentList = new List<TurnOrderHandler>();
    public List<GameObject> heroesInBattle = new List<GameObject>();
    public List<GameObject> enemiesInBattle = new List<GameObject>();


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

    public List<GameObject> heroesToManage = new List<GameObject>();
    private TurnOrderHandler heroChoice;

    public GameObject enemyButton;
    public Transform spacer;

    public GameObject fadeInPanel;
    public GameObject actionPanel;
    public GameObject magicPanel;
    public GameObject enemySelectPanel;

    // Hero attack variables
    public Transform actionSpacer;
    public Transform magicSpacer;
    public GameObject actionButton;
    public GameObject spellButton;
    private List<GameObject> attackButtons = new List<GameObject>();

    // Enemy button list
    private List<GameObject> enemyButtonList = new List<GameObject>();

    // Battle start delay
    public bool startBattle = false;
    private float startDelay = 3.25f;
    private float startDelayTimer;

    // Fade In Properties
    public float fadeInTimer = 0.0f, fadeInLength = 2f;
    Color fadeInColorStart, fadeInColorEnd;

    void Start ()
	{
        animatorActionPanel = GameObject.Find("ActionPanel").GetComponentInChildren<Animator>();
        animatorTargetPanel = GameObject.Find("TargetSelection").GetComponentInChildren<Animator>();
        animatorCamera = GameObject.Find("MainCamera").GetComponentInChildren<Animator>();

        fadeInPanel.SetActive(true);
        fadeInColorStart = new Color(0, 0, 0, 1);
        fadeInColorEnd = new Color(fadeInColorStart.r, fadeInColorStart.g, fadeInColorStart.b, 0);


        actionState = ActionState.WAITING;
        heroInput = HeroUI.ACTIVATE;
        enemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        heroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        //actionPanel.SetActive(false);
        magicPanel.SetActive(false);
        //enemySelectPanel.SetActive(false);

        EnemySelectionButtons();

        startDelayTimer = startDelay;
	}
	
	void Update ()
	{
        if (startBattle)
        {
            CheckActionState();
            CheckHeroInputState();
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

            //actionPanel.SetActive(true);
            animatorActionPanel.SetBool("UI_ActionClose", false);
            animatorActionPanel.SetBool("UI_ActionOpen", true);
            CreateAttackButtons();

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
        }
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

    public void AttackInput()
    {
        heroChoice.activeAgent = heroesToManage[0].name;
        heroChoice.agentGO = heroesToManage[0];
        heroChoice.chosenAttack = heroesToManage[0].GetComponent<HeroController>().hero.attacks[0];
        //enemySelectPanel.SetActive(true);
        animatorTargetPanel.SetBool("UI_TargetClose", false);
        animatorTargetPanel.SetBool("UI_TargetOpen", true);
    }

    public void EnemySelectInput(GameObject chosenEnemy)
    {
        heroChoice.targetGO = chosenEnemy;
        heroInput = HeroUI.DONE;
        animatorTargetPanel.SetBool("UI_TargetOpen", false);
        animatorTargetPanel.SetBool("UI_TargetClose", true);
    }

    void HeroInputDone()
    {
        activeAgentList.Add(heroChoice);
        ClearAttackPanel();

        heroesToManage[0].transform.FindChild("Selector").gameObject.SetActive(false);
        heroesToManage.RemoveAt(0);
        heroInput = HeroUI.ACTIVATE;
    }

    void ClearAttackPanel()
    {
        //enemySelectPanel.SetActive(false);
        //actionPanel.SetActive(false);
        magicPanel.SetActive(false);

        animatorActionPanel.SetBool("UI_ActionOpen", false);
        animatorActionPanel.SetBool("UI_ActionClose", true);

        foreach (GameObject attackButton in attackButtons)
        {
            Destroy(attackButton);
        }
        attackButtons.Clear();
    }

    void CreateAttackButtons()
    {
        // Attack Button
        GameObject attackButton = Instantiate(actionButton) as GameObject;
        Text attackButtonText = attackButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
        attackButtonText.text = "Attack";
        attackButton.GetComponent<Button>().onClick.AddListener(() => AttackInput());
        attackButton.transform.SetParent(actionSpacer, false);
        attackButtons.Add(attackButton);

        // Magic Button
        GameObject magicButton = Instantiate(actionButton) as GameObject;
        Text magicButtonText = magicButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
        magicButtonText.text = "Magic";
        magicButton.GetComponent<Button>().onClick.AddListener(() => MagicInput());
        magicButton.transform.SetParent(actionSpacer, false);
        attackButtons.Add(magicButton);

        if (heroesToManage[0].GetComponent<HeroController>().hero.magicAttacks.Count > 0)
        {
            foreach(BaseAttack magicAttack in heroesToManage[0].GetComponent<HeroController>().hero.magicAttacks)
            {
                GameObject spellBtn = Instantiate(spellButton) as GameObject;
                Text spellButtonText = spellBtn.transform.FindChild("Text").gameObject.GetComponent<Text>();
                spellButtonText.text = magicAttack.attackName;
                SpellCastButton spellCastButton = spellBtn.GetComponent<SpellCastButton>();
                spellCastButton.spellToCast = magicAttack;
                spellBtn.transform.SetParent(magicSpacer, false);
                attackButtons.Add(spellBtn);
            }
        }
        else
        {
            magicButton.GetComponent<Button>().interactable = false;
        }
    }

    public void MagicInput()
    {
        magicPanel.SetActive(true);
    }

    public void SpellInput(BaseAttack chosenSpell)
    {
        heroChoice.activeAgent = heroesToManage[0].name;
        heroChoice.agentGO = heroesToManage[0];
        heroChoice.chosenAttack = chosenSpell;

        magicPanel.SetActive(false);
        enemySelectPanel.SetActive(true);
    }
}

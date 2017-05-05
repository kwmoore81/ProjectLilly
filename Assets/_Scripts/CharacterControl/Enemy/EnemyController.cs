using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    private BattleController battleControl;
    public BaseEnemy enemy;

    public IEnemyActionControl enemyActionControl;

    // Enemy state machine
    public enum EnemyState
    {
        WAITING,        // Waiting for ATB bar to fill
        CHOOSEACTION,   // Choose enemy action
        IDLE,           // Make enemy idle in between actions
        ACTION,         // Process enemy actions
        DEAD,           // Enemy is dead, waiting for things to happen
        CLEANSED        // Enemy is cleansed and leaves the battle
    }
    public EnemyState currentState;

    // Variables for weapon draw delay
    private float weaponDrawTimer = 0.0f;
    private float weaponDrawDelay = .75f;

    public GameObject selector;

    // Variables for handling ATB
    private float ATB_Timer = 0;
    private float ATB_MaxDelay = 10;

    TurnOrderHandler enemyAttack;

    public bool isAlive = true;

    // Enemy info panel variables
    private EnemyPanelInfo panelInfo;
    public GameObject enemyPanel;
    private Image HP_Bar;
    private Image corruption_Bar;
    private Image element_Icon;

    void Awake()
    {
        InitializeStats();

        ATB_Timer = Random.Range(0, 2.5f);
        currentState = EnemyState.WAITING;
        battleControl = GameObject.Find("BattleManager").GetComponent<BattleController>();

        selector.SetActive(false);

        enemyActionControl = gameObject.GetComponent<IEnemyActionControl>();

        enemyActionControl.EnemyAwake();
    }

    void Update()
    {
        if (battleControl.startBattle) CheckState();

        enemyActionControl.DrawWeapon();
    }

    void CheckState()
    {
        //Debug.Log(currentState);

        switch (currentState)
        {
            case (EnemyState.WAITING):
                UpdateATB();
                break;
            case (EnemyState.CHOOSEACTION):
                ChooseAction();
                currentState = EnemyState.IDLE;
                break;
            case (EnemyState.IDLE):

                break;
            case (EnemyState.ACTION):
                PerformAction();
                break;
            case (EnemyState.DEAD):
                EnemyDeath();
                break;
            case (EnemyState.CLEANSED):
                EnemyCleansed();
                break;
        }
    }

    void InitializeStats()
    {
        enemy.CurrentHealth = enemy.baseHealth;
        enemy.CurrentAttackPower = enemy.BaseAttackPower;
        enemy.CurrentPhysicalDefense = enemy.BasePhysicalDefense;
        enemy.currentCorruption = enemy.startingCorruption;

        StartEnemyPanel();
    }

    void UpdateATB()
    {
        if (ATB_Timer >= ATB_MaxDelay)
        {
            currentState = EnemyState.CHOOSEACTION;
        }
        else
        {
            ATB_Timer += Time.deltaTime;
        }
    }

    void ChooseAction()
    {
        // Create an enemy attack and assign necessary info
        enemyAttack = new TurnOrderHandler();
        enemyAttack.activeAgent = name;
        enemyAttack.agentGO = this.gameObject;
        enemyAttack.targetGO = battleControl.heroesInBattle[Random.Range(0, battleControl.heroesInBattle.Count)];

        // Pass enemy attack to the active agent list
        int randomChoice = Random.Range(0, enemy.attacks.Count);
        enemyAttack.chosenAttack = enemy.attacks[randomChoice];
        //Debug.Log(this.gameObject.name + " has chosen " + enemyAttack.chosenAttack.attackName + " and does " + enemyAttack.chosenAttack.attackDamage + " damage.");

        battleControl.ActionCollector(enemyAttack);
    }

    private void PerformAction()
    {
        //// Set battle camera type
        //if (!battleCameraSet)
        //{
        //    cameraControl.BattleCamInput(transform, enemyAttack.targetGO.transform, 1);
        //    battleCameraSet = true;
        //}

        // Perform attack animation
        Vector3 targetPosition = new Vector3(enemyAttack.targetGO.transform.position.x, transform.position.y, enemyAttack.targetGO.transform.position.z);
        enemyActionControl.AttackInput(battleControl.activeAgentList[0].chosenAttack, targetPosition);
    }

    public void EndAction()
    {
        // Remove from the active agent list
        battleControl.activeAgentList.RemoveAt(0);

        if (battleControl.actionState != BattleController.ActionState.WIN && battleControl.actionState != BattleController.ActionState.LOSE)
        {
            // Reset the battle controller to WAIT
            battleControl.actionState = BattleController.ActionState.WAITING;

            // Reset hero state
            ATB_Timer = 0;
            currentState = EnemyState.WAITING;
        }
        else
        {
            currentState = EnemyState.IDLE;
        }

        //cameraControl.BattleCamReset();
        //battleCameraSet = false;
    }

    public void TakeCleansing(int _damage)
    {
        enemy.currentCorruption -= _damage;

        if (enemy.currentCorruption <= 0)
        {
            enemy.currentCorruption = 0;

            EnemyCleansed();
        }

        UpdateEnemyPanel();
    }

    public void TakeDamage(int _damage)
    {
        enemy.CurrentHealth -= _damage;

        // Play hit animation
        enemyActionControl.HitReaction();

        if (enemy.CurrentHealth <= 0)
        {
            enemy.CurrentHealth = 0;
            currentState = EnemyState.DEAD;
        }

        UpdateEnemyPanel();
    }

    public void DoDamage()
    {
        int calculatedDamage = enemy.CurrentAttackPower + battleControl.activeAgentList[0].chosenAttack.attackDamage;
        enemyAttack.targetGO.GetComponent<HeroController>().TakeDamage(calculatedDamage);
    }

    void EnemyCleansed()
    {
        // Update area corruption
        battleControl.corruptionMeter.GetComponent<CorruptionMeter>().LowerCorruption(enemy.startingCorruption);

        // TODO: Add animations to leave battle area
        // TODO: Cleanup battle manager agent list
        this.gameObject.tag = "DeadEnemy";
        battleControl.enemiesInBattle.Remove(this.gameObject);

        // Disable enemy selector
        selector.SetActive(false);

        // Remove from active agent list
        if (battleControl.enemiesInBattle.Count > 0)
        {
            for (int i = 0; i < battleControl.activeAgentList.Count; i++)
            {
                if (battleControl.activeAgentList[i].agentGO == this.gameObject)
                {
                    battleControl.activeAgentList.Remove(battleControl.activeAgentList[i]);
                }

                if (battleControl.activeAgentList[i].targetGO == this.gameObject)
                {
                    battleControl.activeAgentList[i].targetGO = battleControl.enemiesInBattle[Random.Range(0, battleControl.enemiesInBattle.Count)];
                }
            }
        }

        // Making enemy dead for now, so the battle can continue
        enemyActionControl.DeathReaction();

        // Check if all enemies are dead and set isAlive to false;
        battleControl.actionState = BattleController.ActionState.CHECKFORDEAD;
        isAlive = false;

        // Reset enemy buttons and check if battle has been won or lost
        battleControl.EnemySelectionButtons();
        battleControl.actionState = BattleController.ActionState.CHECKFORDEAD;
    }

    void EnemyDeath()
    {
        if (!isAlive)
        {
            return;
        }
        else
        {
            //// Change tag and remove gameObject enemiesInBattle list
            this.gameObject.tag = "DeadEnemy";
            battleControl.enemiesInBattle.Remove(this.gameObject);

            // Disable enemy selector
            selector.SetActive(false);

            // Remove from active agent list
            if (battleControl.enemiesInBattle.Count > 0)
            {
                for (int i = 0; i < battleControl.activeAgentList.Count; i++)
                {
                    if (battleControl.activeAgentList[i].agentGO == this.gameObject)
                    {
                        battleControl.activeAgentList.Remove(battleControl.activeAgentList[i]);
                    }

                    if (battleControl.activeAgentList[i].targetGO == this.gameObject)
                    {
                        battleControl.activeAgentList[i].targetGO = battleControl.enemiesInBattle[Random.Range(0, battleControl.enemiesInBattle.Count)];
                    }
                }
            }

            // Play death animation
            enemyActionControl.DeathReaction();

            // Update area corruption
            battleControl.corruptionMeter.GetComponent<CorruptionMeter>().RaiseCorruption(enemy.startingCorruption);
           
            // Check if all enemies are dead and set isAlive to false;
            battleControl.actionState = BattleController.ActionState.CHECKFORDEAD;
            isAlive = false;

            // Reset enemy buttons and check if battle has been won or lost
            battleControl.EnemySelectionButtons();
            battleControl.actionState = BattleController.ActionState.CHECKFORDEAD;
        }
    }

    public void EnemyRevive()
    {
        enemyActionControl.Revive();
    }

    void StartEnemyPanel()
    {
        panelInfo = enemyPanel.GetComponent<EnemyPanelInfo>();

        // Add info to hero panel
        panelInfo.enemyName.text = name;
        panelInfo.enemyHP.text = "HP: " + enemy.CurrentHealth + " / " + enemy.baseHealth;

        HP_Bar = panelInfo.HP_Bar;
        corruption_Bar = panelInfo.Corruption_Bar;
        element_Icon = panelInfo.Element_Icon;

        UpdateEnemyPanel();
    }

    public void UpdateEnemyPanel()
    // TODO: Modify to accomodate different class info
    {
        // Update HP bar and text
        float HP_FillPercentage = enemy.CurrentHealth / enemy.baseHealth;
        HP_Bar.transform.localScale = new Vector3(Mathf.Clamp(HP_FillPercentage, 0, 1), HP_Bar.transform.localScale.y, HP_Bar.transform.localScale.z);
        panelInfo.enemyHP.text = "HP: " + enemy.CurrentHealth + " / " + enemy.baseHealth;

        // Update corruption bar and text
        float CorruptionFillPercentage = enemy.currentCorruption / enemy.maxCorruption;
        corruption_Bar.transform.localScale = new Vector3(Mathf.Clamp(CorruptionFillPercentage, 0, 1), corruption_Bar.transform.localScale.y, corruption_Bar.transform.localScale.z);
        panelInfo.corruptionLevel.text = "Corruption: " + Mathf.Round(CorruptionFillPercentage * 100) + "%";

        // Update element icon
    }

    public void EnemyPanelButtonOn()
    {
        panelInfo.GetComponent<Outline>().enabled = true;
    }

    public void EnemyPanelButtonOff()
    {
        panelInfo.GetComponent<Outline>().enabled = false;
    }
}

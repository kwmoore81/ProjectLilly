using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    private BattleController battleControl;
    public BaseEnemy enemy;

    IEnemyActionControl enemyActionControl;

    // Enemy state machine
    public enum EnemyState
    {
        WAITING,        // Waiting for ATB bar to fill
        CHOOSEACTION,   // Choose enemy action
        IDLE,           // Make enemy idle in between actions
        ACTION,         // Process enemy actions
        DEAD            // Enemy is dead, waiting for things to happen
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

    private bool isAlive = true;

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
        }
    }

    void InitializeStats()
    {
        enemy.CurrentHealth = enemy.BaseHealth;
        enemy.CurrentMP = enemy.BaseMP;
        enemy.CurrentAttackPower = enemy.BaseAttackPower;
        enemy.CurrentPhysicalDefense = enemy.BasePhysicalDefense;
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
        Vector3 targetPosition = new Vector3(enemyAttack.targetGO.transform.position.x + 2f, transform.position.y, enemyAttack.targetGO.transform.position.z);
        enemyActionControl.AttackInput(0, targetPosition);
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

    public void TakeDamage(float _damage)
    {
        enemy.CurrentHealth -= _damage;

        // Play hit animation
        enemyActionControl.HitReaction();

        if (enemy.CurrentHealth <= 0)
        {
            enemy.CurrentHealth = 0;
            currentState = EnemyState.DEAD;
        }
    }

    public void DoDamage()
    {
        float calculatedDamage = enemy.CurrentAttackPower + battleControl.activeAgentList[0].chosenAttack.attackDamage;
        enemyAttack.targetGO.GetComponent<HeroController>().TakeDamage(calculatedDamage);
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
            enemyActionControl.DefendInput();
           
            // Check if all enemies are dead and set isAlive to false;
            battleControl.actionState = BattleController.ActionState.CHECKFORDEAD;
            isAlive = false;

            // Reset enemy buttons and check if battle has been won or lost
            battleControl.EnemySelectionButtons();
            battleControl.actionState = BattleController.ActionState.CHECKFORDEAD;
        }
    }
}

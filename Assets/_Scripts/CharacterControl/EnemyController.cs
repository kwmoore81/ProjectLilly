using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    protected Animator animator;
    private BattleController battleControl;
    public BaseEnemy enemy;

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

    public GameObject selector;

    // Variables for handling ATB
    private float ATB_Timer = 0;
    private float ATB_MaxDelay = 10;

    TurnOrderHandler enemyAttack;

    // Variables for performing timed actions
    private Vector3 startPosition;
    private float moveSpeed = 20;
    private bool actionStarted = false;

    private bool isAlive = true;

    void Start ()
    {
        InitializeStats();

        animator = GetComponentInChildren<Animator>();

        startPosition = transform.position;
        ATB_Timer = Random.Range(0, 2.5f);
        currentState = EnemyState.WAITING;
        battleControl = GameObject.Find("BattleManager").GetComponent<BattleController>();

        selector.SetActive(false);
    }

    void Update()
    {
        if (battleControl.startBattle) CheckState();
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
                StartCoroutine(PerformAction());
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
        Debug.Log(this.gameObject.name + " has chosen " + enemyAttack.chosenAttack.attackName + " and does " + enemyAttack.chosenAttack.attackDamage + " damage.");

        battleControl.ActionCollector(enemyAttack);
    }

    private IEnumerator PerformAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        // Move enemy to target
        Vector3 targetPosition = new Vector3(enemyAttack.targetGO.transform.position.x + 1.5f, enemyAttack.targetGO.transform.position.y, enemyAttack.targetGO.transform.position.z);
        while (MoveTowardTarget(targetPosition))
        {
            animator.SetBool("Moving", true);
            animator.SetFloat("Velocity Z", moveSpeed);

            yield return null;
        }

        animator.SetBool("Moving", false);

        // Wait for set time, then do damage
        int punchSide = Random.Range(0, 2);
        if (punchSide == 1)
        {
            animator.SetTrigger("Attack3Trigger");
        }
        else
        {
            animator.SetTrigger("Attack6Trigger");
        }

        yield return new WaitForSeconds(0.5f);
        DoDamage();

        // Move enemy back to starting position
        while (MoveTowardStart(startPosition))
        {
            animator.SetBool("Moving", true);
            animator.SetFloat("Velocity Z", -moveSpeed);

            yield return null;
        }

        animator.SetBool("Moving", false);

        // Remove enemy from the active agent list
        battleControl.activeAgentList.RemoveAt(0);

        // Reset the battle controller to WAIT
        battleControl.actionState = BattleController.ActionState.WAITING;

        // Reset enemy state
        ATB_Timer = 0;
        currentState = EnemyState.WAITING;

        actionStarted = false;
    }

    private bool MoveTowardTarget(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime));
    }

    private bool MoveTowardStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime));
    }

    public void TakeDamage(float _damage)
    {
        enemy.CurrentHealth -= _damage;

        // Play hit animation
        int hits = 5;
        int hitNumber = Random.Range(0, hits);
        animator.SetTrigger("GetHit" + (hitNumber + 1).ToString() + "Trigger");

        if (enemy.CurrentHealth <= 0)
        {
            enemy.CurrentHealth = 0;
            currentState = EnemyState.DEAD;
        }
    }

    void DoDamage()
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
            // Change tag and remove gameObject enemiesInBattle list
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

            // Change model color ... to be replaced by death animation
            animator.SetTrigger("Death1Trigger");
            //this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(105, 105, 105, 255);

            // Check if all enemies are dead and set isAlive to false;
            battleControl.actionState = BattleController.ActionState.CHECKFORDEAD;
            isAlive = false;

            // Reset enemy buttons and check if battle has been won or lost
            battleControl.EnemySelectionButtons();
            battleControl.actionState = BattleController.ActionState.CHECKFORDEAD;
        }
    }
}

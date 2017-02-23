using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroController_Old : MonoBehaviour
{
    protected Animator animator;
    protected CameraController cameraControl;
    private BattleController battleControl;
    public BaseHero hero;

    // Hero state machine
    public enum HeroState
    {
        WAITING,    // Waiting for ATB bar to fill
        ACTIONLIST,  // Add hero to list
        IDLE,       // Make hero idle in between actions
        SELECT,     // Hero is choosing action
        ACTION,     // Process hero actions
        DEAD        // Hero is dead, waiting for things to happen
    }
    public HeroState currentState;

    // Variables for handling ATB bar
    private float ATB_Timer = 0;
    private float ATB_MaxDelay = 5;
    private Image ATB_Bar;
    private Image HP_Bar;
    private Image MP_Bar;

    public GameObject selector;
    public GameObject enemyToAttack;

    // Variables for performing timed actions
    private Vector3 startPosition;
    private float moveSpeed = 20;
    private bool actionStarted = false;

    private bool isAlive = true;

    // Hero panel variables
    private HeroPanelInfo panelInfo;
    public GameObject heroPanel;
    private Transform heroPanelSpacer;

    private bool battleCameraSet = false;

    void Start ()
    {
        InitializeStats();

        animator = GetComponentInChildren<Animator>();

        // Create panel and add info
        heroPanelSpacer = GameObject.Find("BattleCanvas").transform.FindChild("HeroPanel").transform.FindChild("HeroPanelSpacer");
        CreateHeroPanel();

        startPosition = transform.position;
        ATB_Timer = Random.Range(0, 2.5f);
        currentState = HeroState.WAITING;
        battleControl = GameObject.Find("BattleManager").GetComponent<BattleController>();
        cameraControl = GameObject.Find("Main Camera").GetComponent<CameraController>();

        selector.SetActive(false);
	}
	
	void Update ()
    {
        if (battleControl.startBattle)  CheckState();
	}

    void CheckState()
    {
        //Debug.Log(currentState);

        switch (currentState)
        {
            case (HeroState.WAITING):
                UpdateATB();
                break;
            case (HeroState.ACTIONLIST):
                AddToActionList();
                break;
            case (HeroState.IDLE):
                // Idle
                break;
            case (HeroState.SELECT):

                break;
            case (HeroState.ACTION):
                StartCoroutine(PerformAction());
                break;
            case (HeroState.DEAD):
                HeroDeath();
                break;
        }
    }

    void InitializeStats()
    {
        hero.CurrentHP = hero.BaseHP;
        hero.CurrentMP = hero.BaseMP;
        hero.CurrentAttack = hero.BaseAttack;
        hero.CurrentDefense = hero.BaseDefense;
    }

    void UpdateATB()
    {
        if (ATB_Timer >= ATB_MaxDelay)
        {
            currentState = HeroState.ACTIONLIST;
        }
        else
        {
            ATB_Timer += Time.deltaTime;
            float ATB_FillPercentage = ATB_Timer / ATB_MaxDelay;
            ATB_Bar.transform.localScale = new Vector3(Mathf.Clamp(ATB_FillPercentage, 0, 1), ATB_Bar.transform.localScale.y, ATB_Bar.transform.localScale.z);
        }
    }

    void AddToActionList()
    {
        battleControl.heroesToManage.Add(this.gameObject);
        currentState = HeroState.IDLE;
    }

    private IEnumerator PerformAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        // Set battle camera type
        if (!battleCameraSet)
        {
            cameraControl.BattleCamInput(transform, enemyToAttack.transform, 1);
            battleCameraSet = true;
        }

        // Move enemy to target
        Vector3 targetPosition = new Vector3(enemyToAttack.transform.position.x - 2f, transform.position.y, enemyToAttack.transform.position.z);
        while (MoveTowardTarget(targetPosition))
        {
            animator.SetBool("Moving", true);
            animator.SetFloat("Velocity Z", moveSpeed);

            yield return null;
        }

        animator.SetBool("Moving", false);

        // Wait for set time, then do damage
        int kickSide = Random.Range(0, 2);
        if (kickSide == 1)
        {
            animator.SetTrigger("AttackKick1Trigger");
        }
        else
        {
            animator.SetTrigger("AttackKick2Trigger");
        }
        yield return new WaitForSeconds(0.85f);
        DoDamage();

        animator.SetInteger("Jumping", 1);
        animator.SetTrigger("JumpTrigger");

        // Move enemy back to starting position
        while (MoveTowardStart(startPosition))
        {
            //animator.SetBool("Moving", true);
            //animator.SetFloat("Velocity Z", -moveSpeed);

            // Setup jump animation
            animator.SetInteger("Jumping", 2);
            animator.SetTrigger("JumpTrigger");

            yield return null;
        }

        animator.SetInteger("Jumping", 0);

        //animator.SetBool("Moving", false);

        // Remove enemy from the active agent list
        battleControl.activeAgentList.RemoveAt(0);

        if (battleControl.actionState != BattleController.ActionState.WIN && battleControl.actionState != BattleController.ActionState.LOSE)
        {
            // Reset the battle controller to WAIT
            battleControl.actionState = BattleController.ActionState.WAITING;

            // Reset hero state
            ATB_Timer = 0;
            currentState = HeroState.WAITING;
        }
        else
        {
            currentState = HeroState.IDLE;
        }

        cameraControl.BattleCamReset();
        battleCameraSet = false;
        actionStarted = false;
    }

    private bool MoveTowardTarget(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime));
    }

    private bool MoveTowardStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, (moveSpeed * 1.25f) * Time.deltaTime));
    }

    public void TakeDamage(float _damage)
    {
        hero.CurrentHP -= _damage;

        // Play hit animation
        int hits = 5;
        int hitNumber = Random.Range(0, hits);
        animator.SetTrigger("GetHit" + (hitNumber + 1).ToString() + "Trigger");

        if (hero.CurrentHP <= 0)
        {
            hero.CurrentHP = 0;
            currentState = HeroState.DEAD;
        }

        UpdateHeroPanel();
    }

    void DoDamage()
    {
        float calculatedDamage = hero.CurrentAttack + battleControl.activeAgentList[0].chosenAttack.attackDamage;
        enemyToAttack.GetComponent<EnemyController>().TakeDamage(calculatedDamage);
    }

    void HeroDeath()
    {
        if (!isAlive)
        {
            return;
        }
        else
        {
            // Change tag and remove gameObject from appropriate lists
            this.gameObject.tag = "DeadHero";
            battleControl.heroesInBattle.Remove(this.gameObject);
            battleControl.heroesToManage.Remove(this.gameObject);

            // Set selector, action panel, and enemy select panel to inactive
            selector.SetActive(false);
            battleControl.actionPanel.SetActive(false);
            battleControl.enemySelectPanel.SetActive(false);

            // Remove from active agent list
            if (battleControl.heroesInBattle.Count > 0)
            {
                for (int i = 0; i < battleControl.activeAgentList.Count; i++)
                {
                    if (battleControl.activeAgentList[i].agentGO == this.gameObject)
                    {
                        battleControl.activeAgentList.Remove(battleControl.activeAgentList[i]);
                    }

                    if (battleControl.activeAgentList[i].targetGO == this.gameObject)
                    {
                        battleControl.activeAgentList[i].targetGO = battleControl.heroesInBattle[Random.Range(0, battleControl.heroesInBattle.Count)];
                    }
                }
            }

            // Change model color ... to be replaced by death animation
            animator.SetTrigger("Death1Trigger");
            //this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(105, 105, 105, 255);

            // Reset hero input
            battleControl.actionState = BattleController.ActionState.CHECKFORDEAD;

            isAlive = false;
        }
    }

    void CreateHeroPanel()
    {
        heroPanel = Instantiate(heroPanel) as GameObject;
        panelInfo = heroPanel.GetComponent<HeroPanelInfo>();

        // Add info to hero panel
        panelInfo.heroName.text = name;
        panelInfo.heroHP.text = "HP: " + hero.CurrentHP + " / " + hero.BaseHP;
        panelInfo.heroMP.text = "MP: " + hero.CurrentMP + " / " + hero.BaseMP;

        ATB_Bar = panelInfo.ATB_Bar;
        HP_Bar = panelInfo.HP_Bar;
        MP_Bar = panelInfo.MP_Bar;
        heroPanel.transform.SetParent(heroPanelSpacer, false);
    }

    void UpdateHeroPanel()
    {
        // Update HP bar and text
        float HP_FillPercentage = hero.CurrentHP / hero.BaseHP;
        HP_Bar.transform.localScale = new Vector3(Mathf.Clamp(HP_FillPercentage, 0, 1), ATB_Bar.transform.localScale.y, ATB_Bar.transform.localScale.z);
        panelInfo.heroHP.text = "HP: " + hero.CurrentHP + " / " + hero.BaseHP;

        // Update MP bar and text
        float MP_FillPercentage = hero.CurrentMP / hero.BaseMP;
        MP_Bar.transform.localScale = new Vector3(Mathf.Clamp(MP_FillPercentage, 0, 1), ATB_Bar.transform.localScale.y, ATB_Bar.transform.localScale.z);
        panelInfo.heroMP.text = "MP: " + hero.CurrentMP + " / " + hero.BaseMP;
    }
}

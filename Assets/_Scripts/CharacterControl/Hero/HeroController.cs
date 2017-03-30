using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroController : MonoBehaviour
{
    protected CameraController cameraControl;
    private BattleController battleControl;
    public BaseHero hero;

    IHeroActionControl heroActionControl;

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

    public bool isAlive = true;

    // Hero panel variables
    private HeroPanelInfo panelInfo;
    public GameObject heroPanel;
    private Transform heroPanelSpacer;

    private bool battleCameraSet = false;

    void Awake()
    {
        InitializeStats();

        // Create panel and add info
        heroPanelSpacer = GameObject.Find("BattleCanvas").transform.FindChild("HeroPanel").transform.FindChild("HeroPanelSpacer");
        CreateHeroPanel();

        ATB_Timer = Random.Range(0, 2.5f);
        currentState = HeroState.WAITING;
        battleControl = GameObject.Find("BattleManager").GetComponent<BattleController>();
        cameraControl = GameObject.Find("MainCamera").GetComponent<CameraController>();

        selector.SetActive(false);
        heroActionControl = gameObject.GetComponent<IHeroActionControl>();

        heroActionControl.HeroAwake();
    }

    void Update()
    {
        if (battleControl.startBattle) CheckState();

        heroActionControl.DrawWeapon();
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
                PerformAction();
                break;
            case (HeroState.DEAD):
                HeroDeath();
                break;
        }
    }

    void InitializeStats()
    {
        hero.CurrentHealth = hero.baseHealth;
        hero.CurrentAttackPower = hero.BaseAttackPower;
        hero.CurrentPhysicalDefense = hero.BasePhysicalDefense;
    }

    void UpdateATB()
    {
        if (ATB_Timer >= ATB_MaxDelay)
        {
            currentState = HeroState.ACTIONLIST;
        }
        else
        {
            // TODO: Add Speed stat to ATB fill rate
            // TODO: Add ambush condition
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

    private void PerformAction()
    {
        // Set battle camera type
        if (!battleCameraSet)
        {
            cameraControl.BattleCamInput(transform, enemyToAttack.transform, 1);
            battleCameraSet = true;
        }

        // Perform attack animation
        Vector3 targetPosition = new Vector3(enemyToAttack.transform.position.x - 2f, transform.position.y, enemyToAttack.transform.position.z);
        heroActionControl.AttackInput(battleControl.activeAgentList[0].chosenAttack, targetPosition);
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
            currentState = HeroState.WAITING;
        }
        else
        {
            currentState = HeroState.IDLE;
        }

        cameraControl.BattleCamReset();
        battleCameraSet = false;
    }

    public void TakeDamage(float _damage)
    {
        hero.CurrentHealth -= _damage;

        // Play hit animation
        heroActionControl.HitReaction();

        if (hero.CurrentHealth <= 0)
        {
            hero.CurrentHealth = 0;
            currentState = HeroState.DEAD;
        }

        UpdateHeroPanel();
    }

    public void DoDamage()
    {
        float calculatedDamage = hero.CurrentAttackPower + battleControl.activeAgentList[0].chosenAttack.attackDamage;
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

            // Trigger death animation
            heroActionControl.DeathReaction();

            // Reset hero input
            battleControl.actionState = BattleController.ActionState.CHECKFORDEAD;

            isAlive = false;
        }
    }

    // TODO: Need to setup bar for each class.  Maybe move it to class controllers.
    void CreateHeroPanel()
    {
        heroPanel = Instantiate(heroPanel) as GameObject;
        panelInfo = heroPanel.GetComponent<HeroPanelInfo>();

        // Add info to hero panel
        panelInfo.heroName.text = name;
        panelInfo.heroHP.text = "HP: " + hero.CurrentHealth + " / " + hero.baseHealth;
        //panelInfo.heroMP.text = "MP: " + hero.CurrentMP + " / " + hero.BaseMP;

        ATB_Bar = panelInfo.ATB_Bar;
        HP_Bar = panelInfo.HP_Bar;
        MP_Bar = panelInfo.MP_Bar;
        heroPanel.transform.SetParent(heroPanelSpacer, false);
    }

    public void UpdateHeroPanel()
    // TODO: Modify to accomodate different class info
    {
        // Update HP bar and text
        float HP_FillPercentage = hero.CurrentHealth / hero.baseHealth;
        HP_Bar.transform.localScale = new Vector3(Mathf.Clamp(HP_FillPercentage, 0, 1), ATB_Bar.transform.localScale.y, ATB_Bar.transform.localScale.z);
        panelInfo.heroHP.text = "HP: " + hero.CurrentHealth + " / " + hero.baseHealth;

        // Update MP bar and text
        //float MP_FillPercentage = hero.CurrentMP / hero.BaseMP;
        //MP_Bar.transform.localScale = new Vector3(Mathf.Clamp(MP_FillPercentage, 0, 1), ATB_Bar.transform.localScale.y, ATB_Bar.transform.localScale.z);
        //panelInfo.heroMP.text = "MP: " + hero.CurrentMP + " / " + hero.BaseMP;
    }
}
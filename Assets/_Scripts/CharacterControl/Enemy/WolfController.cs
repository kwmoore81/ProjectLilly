using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WolfController : MonoBehaviour, IEnemyActionControl
{
    protected Animator animator;
    EnemyController enemyControl;
    public GameObject enemyButton;

    // Variables for performing timed actions
    private Vector3 startPosition;
    private float moveSpeed = 15;
    private bool actionStarted = false;

    private bool isAlive = true;

    public void EnemyAwake()
    {
        animator = GetComponentInChildren<Animator>();
        enemyControl = GetComponent<EnemyController>();
        startPosition = transform.position;
        enemyButton.GetComponent<EnemySelectButton>().enemyPrefab = gameObject;

        animator.Play("Wolf Basic Idle", -1, Random.Range(0.0f, 1.0f));
    }

    public void DrawWeapon()
    {
        // Nothing happening here, but the interface needs it.
    }

    void ClawEffect(bool _trailOn)
    {
        //GameObject activeTrail;

        //if (twohandsword.activeSelf == true)
        //{
        //    activeTrail = twohandsword.transform.FindChild("Trail").gameObject;
        //    if (_trailOn)
        //        activeTrail.SetActive(true);
        //    else
        //        activeTrail.SetActive(false);
        //}
    }

    public void Revive()
    {
        //animator.SetTrigger("Revive1Trigger");
    }

    // TODO: Setup RecieveAttack() function
    public void AttackInput(AttackData _chosenAttack, Vector3 _targetPosition)
    {
        StartCoroutine(PerformAttack(_chosenAttack, _targetPosition));
    }

    public void MagicInput(AttackData _chosenAttack, Vector3 _targetPosition)
    {
        StartCoroutine(PerformMagicAttack(_chosenAttack, _targetPosition));
    }

    // TODO: Setup RecieveItemUse() function
    public void ItemUseInput(int _itemID)
    {
        // Probably won't have item use on the wolf, but the interface needs it.
    }

    // TODO: Setup Defend() function
    public void DefendInput()
    {

    }

    public void HitReaction()
    {
        animator.SetTrigger("hit");
    }

    public void InjuredReaction()
    {
        animator.SetBool("injured", true);
    }

    public void DeathReaction()
    {
        animator.SetTrigger("death");
    }

    private IEnumerator PerformAttack(AttackData _chosenAttack, Vector3 _targetPosition)
    {
        _targetPosition = new Vector3(_targetPosition.x + _chosenAttack.targetOffset, _targetPosition.y, _targetPosition.z);

        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        // Set running animation trigger
        animator.SetTrigger("run");
        yield return new WaitForSeconds(.1f);

        if (_chosenAttack.moveDuringAttack)
        {
            while (MoveTowardTarget(_targetPosition))
            {
                yield return null;
            }

            animator.SetTrigger(_chosenAttack.attackAnimation);

            while (MoveTowardTarget(_targetPosition))
            {
                yield return null;
            }
        }
        else
        {
            while (MoveTowardTarget(_targetPosition))
            {
                yield return null;
            }

            animator.SetTrigger(_chosenAttack.attackAnimation);
        }

        yield return new WaitForSeconds(_chosenAttack.damageWaitTime);

        enemyControl.DoDamage();

        yield return new WaitForSeconds(_chosenAttack.attackWaitTime);

        animator.SetTrigger("run");
        yield return new WaitForSeconds(.5f);

        // Move enemy back to starting position
        while (MoveTowardStart(startPosition))
        {
            yield return null;
        }

        animator.SetTrigger("attackTurn");
        //yield return new WaitForSeconds(_chosenAttack.attackWaitTime);
        animator.SetTrigger("idle");

        actionStarted = false;
        enemyControl.EndAction();
    }

    private IEnumerator PerformMagicAttack(AttackData _chosenAttack, Vector3 _targetPosition)
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        animator.SetTrigger(_chosenAttack.attackAnimation);

        yield return new WaitForSeconds(_chosenAttack.attackWaitTime);

        // Shoot spell
        Vector3 relativePosition = _targetPosition - transform.position;
        Vector3 targetHieghtOffset = new Vector3(0, 1.25f, 0);
        Quaternion spellRotation = Quaternion.LookRotation(relativePosition + targetHieghtOffset);
        GameObject tempSpell = Instantiate(_chosenAttack.projectile, _targetPosition, spellRotation) as GameObject;

        yield return new WaitForSeconds(_chosenAttack.damageWaitTime);

        Destroy(tempSpell);

        enemyControl.DoDamage();

        yield return new WaitForSeconds(.5f);

        actionStarted = false;
        enemyControl.EndAction();
    }

    private bool MoveTowardTarget(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime));
    }

    private bool MoveTowardStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime));
    }

    public void HeroDeathAnim()
    {
        animator.SetTrigger("Death1Trigger");
    }
}
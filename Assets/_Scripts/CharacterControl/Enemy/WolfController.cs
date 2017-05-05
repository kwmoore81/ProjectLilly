using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WolfController : MonoBehaviour
{
    protected Animator animator;
    EnemyController enemyControl;

    // Variables for weapon draw delay
    private float weaponDrawTimer = 0.0f;
    private float weaponDrawDelay = .75f;

    // Variables for performing timed actions
    private Vector3 startPosition;
    private float moveSpeed = 20;
    private bool actionStarted = false;

    private bool isAlive = true;

    public void EnemyAwake()
    {
        animator = GetComponentInChildren<Animator>();
        enemyControl = GetComponent<EnemyController>();
        startPosition = transform.position;
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
        animator.SetTrigger("Revive1Trigger");
    }

    // TODO: Setup RecieveAttack() function
    public void AttackInput(int _attackID, Vector3 _targetPosition)
    {
        StartCoroutine(PerformAttack(_targetPosition));
    }

    public void HitReaction()
    {
        // TODO: Add variable hit reaction based on damage done and defend state
        int hits = 5;
        int hitNumber = Random.Range(0, hits);
        animator.SetTrigger("GetHit" + (hitNumber + 1).ToString() + "Trigger");
    }

    public void InjuredAnimation()
    {

    }

    public void DeathReaction()
    {
        animator.SetTrigger("Death1Trigger");
    }

    private IEnumerator PerformAttack(Vector3 _targetPosition)
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        // Move enemy to target
        while (MoveTowardTarget(_targetPosition))
        {
            // Set running animation trigger

            yield return null;
        }

        // Wait for set time, then do damage
        int attackRand = Random.Range(0, 3);

        if (attackRand == 0)
            animator.SetTrigger("Attack4Trigger");
        if (attackRand == 1)
            animator.SetTrigger("Attack5Trigger");
        if (attackRand == 2)
            animator.SetTrigger("Attack6Trigger");

        yield return new WaitForSeconds(0.85f);

        enemyControl.DoDamage();

        // Set turn animation trigger
        // Set run animation trigger

        // Move enemy back to starting position
        while (MoveTowardStart(startPosition))
        {
            // set turn animation trigger

            yield return null;
        }

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
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, (moveSpeed * 1.25f) * Time.deltaTime));
    }

    public void HeroDeathAnim()
    {
        animator.SetTrigger("Death1Trigger");
    }
}
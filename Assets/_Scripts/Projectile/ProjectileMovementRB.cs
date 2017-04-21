using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovementRB : MonoBehaviour
{
    BattleController battleControl;
    Transform target;
    Vector3 targetHeightOffset = new Vector3(0, 1.25f, 0);

    public float speed;

    void Start()
    {
        battleControl = GameObject.Find("BattleManager").GetComponent<BattleController>();
        target = battleControl.activeAgentList[0].targetGO.transform;
    }


    void FixedUpdate()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);

        transform.LookAt(target.position + targetHeightOffset);
        transform.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }
}

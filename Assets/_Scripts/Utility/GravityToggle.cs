using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityToggle : MonoBehaviour {

    public GameObject overWorldMaster;
    private OverworldSceneChanger1 SC1;
    public GameObject wallTrigger;
    private WallDropTrigger wallDropTrigger;
    public float corrutionThreshold = 15;
    Rigidbody rb;
    Collider collider;

    // Use this for initialization
    void Start ()
    {
        SC1 = overWorldMaster.GetComponent<OverworldSceneChanger1>();
        rb = this.GetComponent<Rigidbody>();
        collider = this.GetComponent<Collider>();
        wallDropTrigger = wallTrigger.GetComponent<WallDropTrigger>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (SC1.currentAreaCorruption <= corrutionThreshold & wallDropTrigger.droptrigger == true)
        {
            rb.useGravity = true;
            collider.isTrigger = true;
            rb.isKinematic = false;
        }
	}
}

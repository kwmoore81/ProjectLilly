using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityToggle : MonoBehaviour {

    public GameObject overWorldMaster;
    private OverworldSceneChanger1 SC1;
    public float corrutionThreshold = 15;
    Rigidbody rb;

    // Use this for initialization
    void Start ()
    {
        SC1 = overWorldMaster.GetComponent<OverworldSceneChanger1>();
        rb = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (SC1.currentAreaCorruption <= corrutionThreshold)
        {
            rb.useGravity = true;
        }
	}
}

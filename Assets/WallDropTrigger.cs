using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDropTrigger : MonoBehaviour {
    public GameObject overWorldMaster;
    private OverworldSceneChanger1 SC1;
    public GameObject DataBase;
    private CharacterStatsDB characterStatsDB;
    public bool droptrigger = false;
    public float corrutionThreshold = 15;
    // Use this for initialization
    void Start ()
    {
        SC1 = overWorldMaster.GetComponent<OverworldSceneChanger1>();
        characterStatsDB = DataBase.GetComponent<CharacterStatsDB>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && SC1.currentAreaCorruption <= corrutionThreshold)
        {
            droptrigger = true;
        }
    }
}

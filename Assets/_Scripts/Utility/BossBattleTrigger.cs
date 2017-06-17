using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleTrigger : MonoBehaviour {

    public GameObject overWorldMaster;
    private OverworldSceneChanger1 SC1;
    public GameObject DataBase;
    private CharacterStatsDB characterStatsDB;
    public bool bossTriggered = false;

    // Use this for initialization
    void Start ()
    {
        SC1 = overWorldMaster.GetComponent<OverworldSceneChanger1>();
        characterStatsDB = DataBase.GetComponent<CharacterStatsDB>();
    }
		
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            characterStatsDB.SendData1();
            bossTriggered = true;
            SC1.SceneChange();
        }
    }
}

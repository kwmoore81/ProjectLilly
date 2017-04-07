using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootingController : MonoBehaviour {

    public GameObject loot;
    public Canvas overworldUI;
    private OverworldUIController overworldUIController;
   
    // Use this for initialization
    void Start () {
        overworldUIController = overworldUI.GetComponent<OverworldUIController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        overworldUIController.InteractionPromptOn();
    }

    private void OnTriggerExit(Collider collider)
    {
        overworldUIController.InteractionPromptOff();
    }
}

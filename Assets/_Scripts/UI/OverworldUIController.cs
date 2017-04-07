using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldUIController : MonoBehaviour {

    public GameObject pressF;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void InteractionPromptOn()
    {
        if (pressF.gameObject.activeInHierarchy == false)
        {
            pressF.gameObject.SetActive(true);
        }       
    }

    public void InteractionPromptOff()
    {
        if (pressF.gameObject.activeInHierarchy == true)
        {
            pressF.gameObject.SetActive(false);
        }
    }
}

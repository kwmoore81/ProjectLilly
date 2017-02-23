using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeTest : MonoBehaviour {

    public OverworldSceneChanger1 sceneChanger;
	
	void Start()
    {
        sceneChanger = GetComponent<OverworldSceneChanger1>();
    }

	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Tab))
        {
            sceneChanger.SceneChange();
        }
	}
}

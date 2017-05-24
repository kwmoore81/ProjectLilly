using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeExample : MonoBehaviour {

    public GameObject dataBase;
    Change_Scene change_Scene;

	// Use this for initialization
	void Start ()
    {
        change_Scene = dataBase.GetComponent<Change_Scene>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            change_Scene.Scene("GameOver");
        }

    }
}

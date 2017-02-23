using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFromDataBank1 : MonoBehaviour {

    public GameObject overworld1;
    private OverworldSceneChanger1 SC1;
   
    void Start ()
    {
        SC1 = overworld1.GetComponent<OverworldSceneChanger1>();
        SC1.UpdateFromBank();
    }
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFromDataBank2 : MonoBehaviour {

    public GameObject overworld2;
    private OverWorldSceneChanger2 SC2;

    void Start()
    {
        SC2 = overworld2.GetComponent<OverWorldSceneChanger2>();
        SC2.UpdateFromBank();
    }

}

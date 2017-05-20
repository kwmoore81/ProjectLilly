using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCorruptionOW : MonoBehaviour {

    public Image corruption_Bar;

    public GameObject OverworldMaster;
    private OverworldSceneChanger1 SC1;
    public float currentCorruption;
    private float maxCorruption = 500;
    private float oldCorruption;

	// Use this for initialization
	void Start ()
    {
        InitializeCorruptionMeter();       
        SC1 = OverworldMaster.GetComponent<OverworldSceneChanger1>();
	}
	
	// Update is called once per frame
	void Update () {
        updateCorruption();
	}

    void updateCorruption()
    {
        currentCorruption = SC1.currentAreaCorruption;
       
        if (oldCorruption != currentCorruption)
        {
            InitializeCorruptionMeter();
        }
    }
    private void InitializeCorruptionMeter()
    {
        float corruption_FillPercentage = currentCorruption / maxCorruption;
        corruption_Bar.transform.localScale = new Vector3(Mathf.Clamp(corruption_FillPercentage, 0, 1),
                         corruption_Bar.transform.localScale.y, corruption_Bar.transform.localScale.z);
        oldCorruption = currentCorruption;
    }

}

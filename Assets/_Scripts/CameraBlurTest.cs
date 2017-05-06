using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlurTest : MonoBehaviour {

    public GameObject thirdPersonCamera;
    private v3rdPersonCamera v3rdpersoncamera;

    public float timescale = 1;
    public float maxCameraDistance;
    public float minCameraDistance;
    public int counter = 0;
    public int maxCounter;
    bool transitionStarted = false;
	// Use this for initialization
	void Start () {
        v3rdpersoncamera = thirdPersonCamera.GetComponent<v3rdPersonCamera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transitionStarted == true)
        {
            Time.timeScale = timescale;
            v3rdpersoncamera.defaultDistance = maxCameraDistance;
            counter++;
            if (v3rdpersoncamera.defaultDistance >= maxCameraDistance && counter >= maxCounter)
            {
                v3rdpersoncamera.defaultDistance = minCameraDistance;
                counter = 0;
                transitionStarted = false;
            }
        }
	if (Input.GetKeyDown(KeyCode.M))
        {            
            transitionStarted = true;        
        }
    else if (Input.GetKeyDown(KeyCode.N))
        {
            Time.timeScale = 1;
            v3rdpersoncamera.defaultDistance = 6;
        }	
	}
}

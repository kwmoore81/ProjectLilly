using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour {
   
    public Texture2D screenShot;
    

    // Use this for initialization
    void Start () {
        screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnPostRender()
    {
        //screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();

    }
}

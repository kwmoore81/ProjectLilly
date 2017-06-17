using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogTriggerScript : MonoBehaviour
{

    public GameObject ThirdPersonCamera;
    private VolumetricFog volumetricFog;
    public bool fogActive = false;

    // Use this for initialization
    void Start()
    {
        volumetricFog = ThirdPersonCamera.GetComponent<VolumetricFog>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && fogActive == false)
        {
            fogActive = true;
            volumetricFog.enabled = true;
        }
    }
}

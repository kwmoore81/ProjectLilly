using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptionMeter : MonoBehaviour
{
    public Image corruption_Bar;

    public float currentCorruption;
    private float maxCorruption = 100;

	void Start ()
    {
        currentCorruption = maxCorruption;
	}
	
	void Update ()
    {
        UpdateCorruptionMeter();
	}

    public void RaiseCorruption(int _corruptionChange)
    {
        currentCorruption += _corruptionChange;
    }

    public void LowerCorruption(int _corruptionChange)
    {
        currentCorruption -= _corruptionChange;
    }

    public void UpdateCorruptionMeter()
    {
        float corruption_FillPercentage = currentCorruption / maxCorruption;
        corruption_Bar.transform.localScale = new Vector3(Mathf.Clamp(corruption_FillPercentage, 0, 1), 
                         corruption_Bar.transform.localScale.y, corruption_Bar.transform.localScale.z);
    }
}

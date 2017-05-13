using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBlurTest : MonoBehaviour {

    //public GameObject backGround;
    public Image image;
    public float lerpSpeed = 0.01f;
    public float maxAlpha = 255.0F;
    public float minAlpha = 0.0f;
    float currentAlpha;
    // Use this for initialization
    void Start ()
    {
        image = GetComponent<Image>();
        currentAlpha = image.color.a;
    }
	
	// Update is called once per frame
	void Update ()
    {
       
	if (Input.GetKeyDown(KeyCode.M))
        {        
            StartCoroutine(FadeIn(currentAlpha, lerpSpeed));       
        }
    //else if (Input.GetKeyDown(KeyCode.N))
    //    {
    //        StartCoroutine(FadeOut(1.0f, 0.0f));
    //    }	
	}

    IEnumerator FadeIn(float currentAlpha, float lerpSpeed)
    {
        //Color newColor = new Color(255, 255, 255, 0);
        //image.color = newColor;
        
        //for (float i = 0.0f; i < 1.0f; i += Time.deltaTime / Duration)
        //{
        if (currentAlpha >= maxAlpha)
        {
            Color newColor = new Color(255, 255, 255, Mathf.Lerp(currentAlpha, minAlpha, lerpSpeed));
            image.color = newColor;
            yield return null;
        }

        else if (currentAlpha <= minAlpha)
        {
            Color newColor = new Color(255, 255, 255, Mathf.Lerp(maxAlpha, currentAlpha, lerpSpeed));
            image.color = newColor;
            yield return null;
        }
        //}

    }

    IEnumerator FadeOut(float amount, float Duration)
    {
        //Color newColor = new Color(255, 255, 255, 100);
        //image.color = newColor;
        float alpha = image.color.a;
        for (float i = 1.0f; i > 0.0f; i -= Time.deltaTime / Duration)
        {
            
        }
        yield return null;
    }
}

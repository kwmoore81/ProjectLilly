using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBlurTest : MonoBehaviour {

    //public GameObject backGround;
    public Image image;
    public float lerpSpeed = 1.0f;
    public float maxAlpha = 1.0f;
    public float minAlpha = 0.0f;
    float currentAlpha = 1.0f;
    // Use this for initialization
    void Start ()
    {
        image = GetComponent<Image>();
        //currentAlpha = image.color.a;
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
        Color NewAlpha = new Color(255, 255, 255, 0);
        float Alpha = NewAlpha.a;

     
            for (float i = 0.0f; i < 1.0f; i += Time.deltaTime / lerpSpeed)
            {
                Color newColor = new Color(255, 255, 255, Mathf.Lerp(Alpha, currentAlpha, i));
                image.color = newColor;

                yield return null;
            if (i >= 1.0f)
            {
                for (float j = 0.0f; i < 1.0f; j += Time.deltaTime / lerpSpeed)
                {
                    Color newColorFade = new Color(255, 255, 255, Mathf.Lerp(Alpha, currentAlpha - 1.0f, j));
                    image.color = newColorFade;

                    yield return null;
                }
            }

        }
    }
          

    //IEnumerator FadeOut(float amount, float Duration)
    //{
    //    //Color newColor = new Color(255, 255, 255, 100);
    //    //image.color = newColor;
    //    float alpha = image.color.a;
    //    for (float i = 1.0f; i > 0.0f; i -= Time.deltaTime / Duration)
    //    {
            
    //    }
    //    yield return null;
    //}
}

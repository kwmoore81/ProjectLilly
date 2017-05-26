using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBlurTest : MonoBehaviour {

    //public GameObject backGround;
    private Image image;
    public float lerpSpeed = 1.0f;   
    public float currentAlpha = 1.0f;
    public float RotationSpeed = 10.0f;

    public GameObject thirdPersonController;
    Animator animator;
    Rigidbody rb;

    public GameObject SC1;
    private OverworldSceneChanger1 overWorldSceaneChanger1;
    

    public GameObject thirdPersonCamera;
    private v3rdPersonCamera vthirdPersonCamera;
    private Screenshot screenshotScript;

    // Use this for initialization
    void Start()
    {
        image = GetComponentInChildren<Image>();
        animator = thirdPersonController.GetComponent<Animator>();
        rb = thirdPersonController.GetComponent<Rigidbody>();
        overWorldSceaneChanger1 = SC1.GetComponent<OverworldSceneChanger1>();
        vthirdPersonCamera = thirdPersonCamera.GetComponent<v3rdPersonCamera>();
        screenshotScript = thirdPersonCamera.GetComponent<Screenshot>();

    }

    // Update is called once per frame
    void Update ()
    {
 //       image.transform.Rotate(Vector3.back * (RotationSpeed * Time.deltaTime));    
	//if (Input.GetKeyDown(KeyCode.M))
 //       {        
 //           StartCoroutine(FadeIn(currentAlpha, lerpSpeed));          
 //       }    
	}

    public IEnumerator FadeIn(float currentAlpha, float lerpSpeed)
    {
        image.sprite = Sprite.Create(screenshotScript.screenShot, new Rect(0, 0, screenshotScript.screenShot.width, screenshotScript.screenShot.height), Vector2.zero, 100);

        image.color = new Color(255, 255, 255, 255);
        

        //Alpha = 255;
        //animator.enabled = false;
        //rb.constraints = RigidbodyConstraints.FreezeAll;
        //vthirdPersonCamera.lockCamera = true;

        //for (float i = 0.0f; i < 1.0f; i += Time.deltaTime / lerpSpeed)
        //{
        //    Color newColor = new Color(255, 255, 255, Mathf.Lerp(Alpha, currentAlpha, i));
        //    image.color = newColor;

        //    yield return null;
        //}

        //animator.enabled = true;

        //rb.constraints = RigidbodyConstraints.FreezeRotation;
        //vthirdPersonCamera.lockCamera = false;

        //yield return new WaitForSeconds(0.3f);

        overWorldSceaneChanger1.DelayedSceenChange();

        //Alpha = image.color.a;

        //for (float j = 0.0f; j < 1.0f; j += Time.deltaTime / lerpSpeed)
        //{
        //    //Color newColor = new Color(255, 255, 255, Mathf.Lerp(Alpha, currentAlpha - 1.0f, j));
        //    image.color = newColor;
            
        //    yield return null;
        //}

        //while (image.color.a > 0.0f)
        //{
        //    float alpha = image.color.a;
        //    alpha -= 50 * Time.deltaTime;
        //    Color newColor = new Color(255, 255, 255, alpha);
        //    image.color = newColor;

        //    yield return null;
        //}
        float alpha = image.color.a;
        for (float k = alpha; k > 0; k -= 50 * Time.deltaTime)
        {
            Color newColor = new Color(255, 255, 255, k);
            image.color = newColor;

            yield return null;
        }
        
        image.color = new Color(255, 255, 255, 0);
        yield return null;
    }
  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBlurTest : MonoBehaviour {
   
    private Image image;
    public float lerpSpeed = 2.0f;   
    public float targetAlpha = 0.0f;
    public float delayTime = 0.5f;
     
    public GameObject SC1;
    private OverworldSceneChanger1 overWorldSceaneChanger1;

    public GameObject BossTrigger;
    private BossBattleTrigger bossBattleTrigger;
    
    public GameObject thirdPersonCamera;   
    private Screenshot screenshotScript;
   
    // Use this for initialization
    void Start()
    {
        image = GetComponentInChildren<Image>();               
        overWorldSceaneChanger1 = SC1.GetComponent<OverworldSceneChanger1>();       
        screenshotScript = thirdPersonCamera.GetComponent<Screenshot>();
        bossBattleTrigger = BossTrigger.GetComponent<BossBattleTrigger>();

    }

    public IEnumerator FadeIn(float targetAlpha, float lerpSpeed)
    {
        image.sprite = Sprite.Create(screenshotScript.screenShot, new Rect(0, 0, screenshotScript.screenShot.width, screenshotScript.screenShot.height), Vector2.zero, 100);

        image.color = new Color(255, 255, 255, 255);
        
        yield return new WaitForSeconds(delayTime);

        if (bossBattleTrigger.bossTriggered == false)
        {
            overWorldSceaneChanger1.DelayedSceenChange();
        }
        else
        {
            overWorldSceaneChanger1.BossSceneChange();
        }
                       
            // fade from full to clear in DURATION seconds
            while (targetAlpha < lerpSpeed)
            {
                targetAlpha += Time.deltaTime;  // tick accumulator
                float alpha = Mathf.Lerp(1.0f, 0.0f, targetAlpha / lerpSpeed);

                Color finalColor = image.color;
                finalColor.a = alpha;

                image.color = finalColor;

                yield return null;
            }
        
        image.color = new Color(255, 255, 255, 0);
        yield return null;
    }
  
}

using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

    public Transform canvas;
    public bool inBattle = false;
      
    public void pause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else if (canvas.gameObject.activeInHierarchy == true && inBattle == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}


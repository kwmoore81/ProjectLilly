using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Change_Scene : MonoBehaviour {

    public void Scene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void ChangeSingleScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void ChangeAdditiveScene(string name){
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        Time.timeScale = 0f;
    }

    public void UnloadScene(string name){
        if(name == "pause"){
            GameObject.Find("SceneManager").GetComponent<PauseManager>().SetIsPaused();
        }
        SceneManager.UnloadSceneAsync(name);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

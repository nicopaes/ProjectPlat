using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {
    bool isPaused = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Pause();
        }
    }

    private void Pause()
    {
        if (isPaused == false)
        {
            SceneManager.LoadSceneAsync("pause", LoadSceneMode.Additive);
            Time.timeScale = 0f;
        } else {
            SceneManager.UnloadSceneAsync("pause");
            Time.timeScale = 1f;
        }
    }

    public void SetIsPaused(){
        isPaused = true;
    }
}

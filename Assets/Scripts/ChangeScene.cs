using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    private Animator FadeAnimator;

    private void OnEnable()
    {
        FadeAnimator = GetComponent<Animator>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeSingleScene(string name)
    {
        //FadeAnimator.gameObject.SetActive(true);
        FadeAnimator.SetTrigger("FadeIn");
        //SceneManager.LoadScene(name, LoadSceneMode.Single);

        //certamente não é o ideal fazer isso aqui, mas foi um quick-fix prum bug
        //o bug é que, quando saímos pro menu principal atraves da pause, o Time.deltaTime continua sendo igual a zero.
        if(name == "Mockup")
        {
            Debug.LogWarning("bug fix");
            //PauseManager.GetInstance().IsPaused = false;
            Time.timeScale = 1.0f;
        }
    }

    public void ChangeAdditiveScene(string name)
    {        
        Scene LastScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);//LoadSceneMode.Additive);
        //SceneManager.UnloadSceneAsync(LastScene);

        //SceneManager.LoadScene(name, LoadSceneMode.Single);
        FadeAnimator.SetTrigger("FadeOut");
        //Time.timeScale = 0f;
    }

    public void UnloadScene(string name){
        //rework menu de pausa
        //if(name == "pause"){
        //    GameObject.Find("SceneManager").GetComponent<PauseManager>().SetIsPaused();
        //}
        SceneManager.UnloadSceneAsync(name);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

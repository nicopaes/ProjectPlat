using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    private Animator FadeAnimator;
    private string _targetScene;
    private bool _changeStarted;

    private void OnEnable()
    {
        
        //to fazendo isso no script sceneManagerInstancer, pra evitar que "troque" o ativo no meio de uma animação e tal

        //garante que só tenha um go desse tipo na cena
        // ChangeScene[] arr = GameObject.FindObjectsOfType<ChangeScene>();
        // if(arr.Length > 1)
        // {
        //     for(int i = arr.Length - 1; i >= 1; i--)
        //     {
        //         GameObject.Destroy(arr[i].gameObject);
        //     }
        // }
        
        FadeAnimator = GetComponent<Animator>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeSingleScene_inspector(string name)
    {
        ChangeSingleScene(name, true);
    }

    public void ChangeSingleScene(string name, bool sceneTransition = false)
    {
        //se esse processo já começou, não deixa começar de novo
        if(_changeStarted) return;

        //senão, avisa que já começou
        _changeStarted = true;
        Debug.Log("change to" + name);
        if(!sceneTransition)
        {
            Debug.Log("Trigger fadein Transition!");
            FadeAnimator.SetTrigger("FadeIn");
        }
        else
        {
            Debug.Log("Trigger scene Transition!");
            FadeAnimator.SetTrigger("FadeInSceneTransition");
        }
        _targetScene = name;



        //certamente não é o ideal fazer isso aqui, mas foi um quick-fix prum bug
        //o bug é que, quando saímos pro menu principal atraves da pause, o Time.deltaTime continua sendo igual a zero.
        //rever isso pelo amor de, acho que esse nome Mockup já mudou
        if(name == "Mockup")
        {
            Debug.LogWarning("bug fix");
            //PauseManager.GetInstance().IsPaused = false;
            Time.timeScale = 1.0f;
        }
    }

    public void ChangeAdditiveScene()
    {        
        Scene LastScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(_targetScene, LoadSceneMode.Single);//LoadSceneMode.Additive);
        //SceneManager.UnloadSceneAsync(LastScene);

        //SceneManager.LoadScene(name, LoadSceneMode.Single);
        FadeAnimator.SetTrigger("FadeOut");
        //Time.timeScale = 0f;

        //avisa que já acabou esse processo:
        _changeStarted = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    private GameObject _pauseInterface;

    private bool isPaused;
    public bool IsPaused
    {
        get { return isPaused; }
        //quando propriedade é alterada, muda o estado do jogo para estar pausado ou não pausado
        set
        {
            //pausa
            if (value)
            {
                _pauseInterface.SetActive(true);
                Time.timeScale = 0f;
            }
            //despausa
            else if (!value)
            {
                _pauseInterface.SetActive(false);
                //time scale é sempre 1f o jogo inteiro, certo?
                Time.timeScale = 1f;
            }
            isPaused = value;

        }
    }

    //singleton
    private static PauseManager _instance;

    public static PauseManager GetInstance()
    {
        return _instance;
    }

    
    private void Awake()
    {
        //singleton
        _instance = this;

        _pauseInterface = this.gameObject.transform.Find("PauseInterface").gameObject;
        if(_pauseInterface == null)
        {
            Debug.LogWarning("Interface da pausa não encontrado. Ele esta presente no canvas com o nome \"PauseInteface\"? ");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
        }
    }

    //private void Pause()
    //{
    //    if (isPaused == false)
    //    {
    //        SceneManager.LoadSceneAsync("pause", LoadSceneMode.Additive);
    //        Time.timeScale = 0f;
    //    } else {
    //        SceneManager.UnloadSceneAsync("pause");
    //        Time.timeScale = 1f;
    //    }
    //}


}

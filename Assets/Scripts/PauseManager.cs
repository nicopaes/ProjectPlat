using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    private GameObject _pauseInterface;
    private GameObject _optionMenu;

    private PlayerComponent _player;

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
                _player.BlockPlayerMovement(true);
            }
            //despausa
            else if (!value)
            {
                _pauseInterface.SetActive(false);
                //time scale é sempre 1f o jogo inteiro, certo?
                Time.timeScale = 1f;
                _player.BlockPlayerMovement(false);
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

        _optionMenu = this.gameObject.transform.Find("OptionsPanel").gameObject;
        if(_optionMenu == null)
        {
            Debug.LogWarning("Menu de opções não encontrado. Ele esta presente no canvas com o nome \"OptionsPanel\"? ");
        }

        _player = GameObject.FindObjectOfType<PlayerComponent>();
        if(_player == null)
        {
            Debug.LogWarning("Player component não encontrado.");
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

    //usada para abrir e fechar o menuzinho de opções(dentro do jogo! No menu principal acho que vai ser melhor fazer diferente)
    public void EnableOptionMenu(bool enabled)
    {
        _optionMenu.SetActive(enabled);
    }


}

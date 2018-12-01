using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour {

    private GameObject _background;
    private GameObject _mainPanel;
    private GameObject _optionMenu;
    //o primeiro botão, a ser selecionado, para podermos usar o teclado
    private GameObject _returnButton;

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
                _mainPanel.SetActive(true);
                Time.timeScale = 0f;
                _player.BlockPlayerMovement(true);
                //background da pausa
                _background.SetActive(true);

                //seta primeira opção como a selecionada, para podermos mexer com o teclado
                EventSystem.current.SetSelectedGameObject(_returnButton);
                
            }
            //despausa
            else if (!value)
            {
                _mainPanel.SetActive(false);
                //time scale é sempre 1f o jogo inteiro, certo?
                Time.timeScale = 1f;
                _player.BlockPlayerMovement(false);
                //backgournd da pausa
                _background.SetActive(false);
                _optionMenu.GetComponent<OptionsSetter>().EnableAudioOptionPanel(false);
                _optionMenu.GetComponent<OptionsSetter>().EnableControlOptionPanel(false);
                _optionMenu.SetActive(false);
                
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

        _mainPanel = this.gameObject.transform.Find("MainPanel").gameObject;
        if(_mainPanel == null)
        {
            Debug.LogWarning("Interface da pausa não encontrado. Ele esta presente no canvas com o nome \"PauseInteface\"? ");
        }

        _optionMenu = this.gameObject.transform.Find("OptionPanel").gameObject;
        if(_optionMenu == null)
        {
            Debug.LogWarning("Menu de opções não encontrado. Ele esta presente no canvas com o nome \"OptionsPanel\"? ");
        }

        _player = GameObject.FindObjectOfType<PlayerComponent>();
        if(_player == null)
        {
            Debug.LogWarning("Player component não encontrado.");
        }

        _background = this.gameObject.transform.Find("Background").gameObject;
        if(_background == null)
        {
            Debug.LogWarning("Background não encontrado.Ele esta presente no canvas com o nome \"Background\"? ");
        }

        
        _returnButton = _mainPanel.transform.Find("ReturnButton").gameObject;
        if(_background == null)
        {
            Debug.LogWarning("Background não encontrado.Ele esta presente no _mainPanel com o nome \"ReturnButton\"? ");
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
        _mainPanel.SetActive(!enabled);

         if(enabled)
        {
            //option panel
            //seta o botão que será selecionado primeiro, pra conseguirmos usar com o teclado

            //medinho de bugar algo
            Transform t = null;
            GameObject go = null;
            t = _optionMenu.transform.Find("GeneralOptions/AudiosOptionsButton");
            if(t != null) go = t.gameObject;
            if(go != null)
            {
                EventSystem.current.SetSelectedGameObject(go);
            }
            
        }
        // else
        // {
        //     //main panel
        //     //seta o botão que será selecionado primeiro, pra conseguirmos usar com o teclado
        //     //bizarramente, isso não é usado, mas sim o optionsSetter
        //     //medinho de bugar algo
        //     Transform t = null;
        //     GameObject go = null;
        //     t = _mainPanel.transform.Find("ReturnButton");
        //     if(t != null) go = t.gameObject;
        //     if(go != null)
        //     {
        //         EventSystem.current.SetSelectedGameObject(go);
        //     }
        // }
    }


}

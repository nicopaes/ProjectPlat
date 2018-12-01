using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsSetter : MonoBehaviour {

	private GameObject _generalOptionsPanel;
	private GameObject _controlOptionsPanel;
	private GameObject _audioOptionsPanel;
	//os primeiros objetos a serem selecionados em cada sub-seção, para que possamos usar o teclado
	private GameObject _generalFirstSelected;
	private GameObject _controlFirstSelected;
	private GameObject _audioFirstSelected;

	private GameObject _mainPanel;
	
	
	// Use this for initialization
	void Awake () {

		_generalOptionsPanel = transform.Find("GeneralOptions").gameObject;
		if(_generalOptionsPanel == null)
        {
            Debug.LogWarning("Painel geral opções não encontrado. Ele esta presente no painel de opções com o nome \"GeneralOptions\"? ");
        }

		_controlOptionsPanel = transform.Find("ControlOptions").gameObject;
		if(_controlOptionsPanel == null)
        {
            Debug.LogWarning("Painel de opções de controles não encontrado. Ele esta presente no painel de opções com o nome \"ControlOptions\"? ");
        }

		_audioOptionsPanel = transform.Find("AudioOptions").gameObject;
		if(_audioOptionsPanel == null)
        {
            Debug.LogWarning("Painel de opções de aúdio não encontrado. Ele esta presente no painel de opções com o nome \"AudioOptions\"? ");
        }

		_mainPanel = transform.parent.Find("MainPanel").gameObject;
		if(_mainPanel == null)
        {
            Debug.LogWarning("MainPanel não encontrado. Ele esta presente no canvas com o nome \"MainPanel\"? ");
        }

		//o primeiro a ser selectionado, para podermos usar o mouse, na seção control é o botão voltar
		_controlFirstSelected = _controlOptionsPanel.transform.Find("ReturnButton").gameObject;
		if(_controlFirstSelected == null)
        {
            Debug.LogWarning("_controlFirstSelected não encontrado. Ele esta presente no _controlOptionsPanel com o nome \"ReturnButton\"? ");
        }

		_generalFirstSelected = _generalOptionsPanel.transform.Find("AudiosOptionsButton").gameObject;
		if(_generalFirstSelected == null)
        {
            Debug.LogWarning("_generalFirstSelected não encontrado. Ele esta presente no _generalOptionsPanel com o nome \"AudiosOptionsButton\"? ");
        }

		_audioFirstSelected = _audioOptionsPanel.transform.Find("Slider").gameObject;
		if(_audioFirstSelected == null)
        {
            Debug.LogWarning("_audioFirstSelected não encontrado. Ele esta presente no _audioOptionsPanel com o nome \"Slider\"? ");
        }




	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnableControlOptionPanel(bool enabled)
	{
		//meio que tenho que checar null pq senão pode dar problemas de alguem tentar pausar em transições de cena
		if(_generalOptionsPanel != null)
		{
			_generalOptionsPanel.SetActive(!enabled);
		} 
		if(_controlOptionsPanel != null)
		{
			_controlOptionsPanel.SetActive(enabled);
		} 
		//seleciona o primeiro botão, pra podermos usar o teclado
		if(_generalOptionsPanel != null && _controlOptionsPanel != null)
		{
			if(enabled)
			{
				//control
				EventSystem.current.SetSelectedGameObject(_controlFirstSelected);

			}
			else
			{
				//general
				EventSystem.current.SetSelectedGameObject(_generalFirstSelected);
			}
		}
	}

	public void EnableAudioOptionPanel(bool enabled)
	{
		
		if(_generalOptionsPanel != null)
		{
			_generalOptionsPanel.SetActive(!enabled);
		} 
		if(_audioOptionsPanel != null)
		{
			_audioOptionsPanel.SetActive(enabled);
		}

		//seleciona o primeiro botão, pra podermos usar o teclado
		if(_generalOptionsPanel != null && _audioOptionsPanel != null)
		{
			if(enabled)
			{
				//audio
				EventSystem.current.SetSelectedGameObject(_audioFirstSelected);

			}
			else
			{
				//general
				EventSystem.current.SetSelectedGameObject(_generalFirstSelected);
			}
		}


	}

	public void ReturnToMainPanel()
	{
		if(_mainPanel != null)
		{
			_mainPanel.SetActive(true);
			//main panel
			//seta o botão que será selecionado primeiro, pra conseguirmos usar com o teclado
			//medinho de bugar algo
			Transform t = null;
			GameObject go = null;
			t = _mainPanel.transform.Find("ReturnButton");
			if(t != null) go = t.gameObject;
			if(go != null)
			{
				EventSystem.current.SetSelectedGameObject(go);
			}
		}
		this.gameObject.SetActive(false);
	}


}

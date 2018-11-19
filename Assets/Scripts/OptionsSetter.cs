using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsSetter : MonoBehaviour {

	private GameObject _generalOptionsPanel;
	private GameObject _controlOptionsPanel;
	private GameObject _audioOptionsPanel;

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

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnableControlOptionPanel(bool enabled)
	{
		//meio que tenho que checar null pq senão pode dar problemas de alguem tentar pausar em transições de cena
		if(_generalOptionsPanel != null) _generalOptionsPanel.SetActive(!enabled);
		if(_controlOptionsPanel != null) _controlOptionsPanel.SetActive(enabled);
	}

	public void EnableAudioOptionPanel(bool enabled)
	{
		
		if(_generalOptionsPanel != null) _generalOptionsPanel.SetActive(!enabled);
		if(_audioOptionsPanel != null) _audioOptionsPanel.SetActive(enabled);
	}

	public void ReturnToMainPanel()
	{
		if(_mainPanel != null) _mainPanel.SetActive(true);
		this.gameObject.SetActive(false);
	}


}

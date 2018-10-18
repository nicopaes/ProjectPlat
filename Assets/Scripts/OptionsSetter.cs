using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsSetter : MonoBehaviour {

	private GameObject _generalOptionsPanel;
	private GameObject _controlOptionsPanel;
	private GameObject _audioOptionsPanel;
	
	
	// Use this for initialization
	void Start () {
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

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnableControlOptionPanel(bool enabled)
	{
		
		_generalOptionsPanel.SetActive(!enabled);
		_controlOptionsPanel.SetActive(enabled);
	}

	public void EnableAudioOptionPanel(bool enabled)
	{
		
		_generalOptionsPanel.SetActive(!enabled);
		_audioOptionsPanel.SetActive(enabled);
	}




}

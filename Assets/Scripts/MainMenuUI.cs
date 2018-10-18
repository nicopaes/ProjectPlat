using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour {

	private GameObject _mainPanel;
	private GameObject _optionsPanel;
	
	// Use this for initialization
	void Start () {
		_mainPanel = transform.Find("MainPanel").gameObject;
		if(_mainPanel == null)
        {
            Debug.LogWarning("Main panel não encontrado. Ele esta presente no canvas com o nome \"MainPanel\"? ");
        }

		_optionsPanel = transform.Find("OptionPanel").gameObject;
		if(_optionsPanel == null)
        {
            Debug.LogWarning("Option panel não encontrado. Ele esta presente no canvas com o nome \"OptionPanel\"? ");
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnableOptionPanel(bool enabled)
	{
		_optionsPanel.SetActive(enabled);
		_mainPanel.SetActive(!enabled);
	}

	

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPauseMainPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BackToMainMenu()
	{
		Time.timeScale = 1.0f;
		GameObject.FindObjectOfType<ChangeScene>().ChangeSingleScene("menu", true);
	}

	public void Reiniciar()
	{
		Time.timeScale = 1.0f;
		//isso faz com que o trigger usado para o animator seja o mesmo da morte, não o de passar de level
		GameObject.FindObjectOfType<PlayerComponent>().gameObject.SetActive(false);
	}
}

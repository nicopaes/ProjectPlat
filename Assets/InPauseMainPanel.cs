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
		GameObject.FindObjectOfType<ChangeScene>().ChangeSingleScene("menu");
	}

	public void Reiniciar()
	{
		Time.timeScale = 1.0f;
		GameObject.FindObjectOfType<PlayerComponent>().gameObject.SetActive(false);
	}
}

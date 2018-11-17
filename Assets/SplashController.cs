using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.anyKeyDown)
		{
			GameObject.Find("Canvas").GetComponent<MainMenuUI>().EnterMainPanel();
			this.gameObject.SetActive(false);
		}
	}
}

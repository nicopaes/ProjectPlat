using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Credits_ReturnButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable()
	{
		Cursor.visible = true;
		EventSystem.current.SetSelectedGameObject(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

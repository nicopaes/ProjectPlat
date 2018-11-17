using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerInstancer : MonoBehaviour {

	
	public GameObject SceneManagerPrefab;
	void Awake()
	{
		var sm = GameObject.FindObjectOfType<ChangeScene>();
        if(sm == null)
        {
            GameObject.Instantiate(SceneManagerPrefab);
        }
	}
	
}

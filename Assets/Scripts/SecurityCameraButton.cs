using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraButton : ReactiveComponent {

    private GameObject FOVCamera;

	private void OnEnable()
	{
        FOVCamera = GameObject.FindWithTag("Security Camera").transform.Find("FOV").gameObject;
	}

	public override void Reaction()
    {
        if (FOVCamera.activeSelf){
            FOVCamera.SetActive(true);
        } else {
            FOVCamera.SetActive(false);
        }
    }
}

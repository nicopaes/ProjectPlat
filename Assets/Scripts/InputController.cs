using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerComponent))]
public class InputController : MonoBehaviour 
{
	PlayerComponent playerComp;
	void OnEnable()
	{
		playerComp = GetComponent<PlayerComponent>();
	}
	void Update()
	{
		Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		playerComp.SetDirectionalInput(directionalInput);		
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			playerComp.OnJumpInputDown();
		}
		if(Input.GetKeyUp(KeyCode.Space))
		{
			playerComp.OnJumpInputUp();
		}
	}

}

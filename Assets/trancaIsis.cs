using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trancaIsis : MonoBehaviour {

    private PlayerComponent _player;

    // Use this for initialization
	
	private void OnEnable()
	{
        _player = GameObject.FindObjectOfType<PlayerComponent>();
        _player.BlockPlayerMovement(true);
	}

    private void OnDisable()
    {
        _player.BlockPlayerMovement(false);
    }

	// Update is called once per frame
	void Update () {
		
	}
}

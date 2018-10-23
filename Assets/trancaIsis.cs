using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trancaIsis : MonoBehaviour {

    private PlayerComponent _player;

    // Use this for initialization
	void Start () {
        _player = GameObject.FindObjectOfType<PlayerComponent>();
	}

	private void OnEnable()
	{
        Debug.Log("ON ENABLW bloq");
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

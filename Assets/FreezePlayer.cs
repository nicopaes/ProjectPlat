using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePlayer : MonoBehaviour {

    public GameObject Player;

	// Use this for initialization
	void Start () {
        Debug.Log("teje parado");
        Player.GetComponent<InputController>().enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

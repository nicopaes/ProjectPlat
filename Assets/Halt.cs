using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halt : MonoBehaviour {

    public Animator PatrolWalk;

	// Use this for initialization
	void Start () {
        PatrolWalk.SetBool("idle", true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

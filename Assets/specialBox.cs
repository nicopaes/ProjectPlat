using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class specialBox : MonoBehaviour {

	public GameObject SpecialElevator;

	public float attachGravityScale;
	public float normalGravityScale;
	private Rigidbody2D _rb;
	// Use this for initialization
	void Start () {
		_rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject == SpecialElevator)
		{
			Debug.Log("Enter. gravityScale:" + attachGravityScale);
			_rb.gravityScale = attachGravityScale;
		}
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if(coll.gameObject == SpecialElevator)
		{
			Debug.Log("Exit. gravityScale:" + normalGravityScale);
			_rb.gravityScale = normalGravityScale;
		}
	}
}

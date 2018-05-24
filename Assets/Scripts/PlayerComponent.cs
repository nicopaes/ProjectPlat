using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class PlayerComponent : MonoBehaviour {

	[Header("DEBUG")]
	public bool debug;
	[Space]
	public float jumpHeight = 4;
	public float timeToJumpApex = 0.5f;
	[Space]
	public float moveSpeed = 5;

	[Header("Debug Private Stuff")]
	[SerializeField]
	private float gravity;
	[SerializeField]
	private float jumpVelocity;
	Vector3 velocity;
	Controller2D controller;

	void OnEnable()
	{
		controller = GetComponent<Controller2D>();

		gravity = -(2*jumpHeight) / Mathf.Pow(timeToJumpApex,2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}
	void Update()
	{
		if(debug) 
		{
			gravity = -(2*jumpHeight) / Mathf.Pow(timeToJumpApex,2);
			jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		}
		if(controller.collisionsInf.above || controller.collisionsInf.below)
		{
			velocity.y = 0;
		}
		if(Input.GetKeyDown(KeyCode.Space) && controller.collisionsInf.below)
		{
			velocity.y = jumpVelocity;
		}
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

		velocity.x = input.x * moveSpeed;
		velocity.y += gravity * Time.deltaTime;

		controller.Move(velocity * Time.deltaTime);
	}
}

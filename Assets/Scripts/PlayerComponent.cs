using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

[RequireComponent (typeof (Controller2D))]
public class PlayerComponent : MonoBehaviour {

    public delegate void playerAction();
    public static event playerAction ActionButton;

    [Header("Debug?")]
	public bool debug;

	[Header("Movement Variables")]
	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	public float fallMultipler = 2.5f;
	[Space]
	public float accelerationTimeAirborne = .2f;	
	public float accelerationTimeGrounded = .1f;
	[Range(0.1f,50f)]
	public float moveSpeed = 6;
	[Header("MELEE ATTACK")]
	public MeleeAttack meleeAtt;
	public float activeAttackTime = 0.5f;


    [Space]
    [Header("PRIVATE DEBUG SHIT")]
	[SerializeField]
	float gravity;
	[SerializeField]
	float maxJumpVelocity;
	[SerializeField]
	float minJumpVelocity;
	[SerializeField]
	[Space]
	public float velocityXSmoothing;

    [SerializeField]
	Vector3 velocity;

	Controller2D controller;
    public Animator anim;

	Vector2 directionalInput;
    [SerializeField]
    [Space]

    [HideInInspector]
    public GameObject nearBox = null;

	private Transform spawnPoint;

    [HideInInspector]
    public bool isOnDialogTrigger = false;
    private bool holdingBox = false;

    private GameObject originalBoxParent;

	void Start() {
		controller = GetComponent<Controller2D> ();
        anim = GetComponentInChildren<Animator> ();

        spawnPoint = GameObject.FindWithTag("Respawn").transform;

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
	}

	void OnEnable()
	{
		//transform.parent.position = spawnPoint.position;
		
        //desnecessário após rework no que acontece quando o player morre (ass. Krauss)
        //transform.localPosition = Vector3.zero;
	}

	void Update() {

        //se jogo está pausado, não faz nada
        if (Time.timeScale == 0.0f) return;

        RecalculatePhysics(debug);
		CalculateVelocity ();

		controller.Move (velocity * Time.deltaTime);
		if (controller.collisionsInf.above || controller.collisionsInf.below) {
            velocity.y = 0;
            anim.SetBool("onFloor", true);
        }

        if (!controller.collisionsInf.below)
        {
            anim.SetBool("onFloor", false);
        }


        //Entregando valores ao animator
        anim.SetFloat("velH", velocity.x);
    }

	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}

	public void OnJumpInputDown() 
	{		
		if (controller.collisionsInf.below) {
			velocity.y = maxJumpVelocity;
		}
	}

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}

	void CalculateVelocity() {
         
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisionsInf.below)?accelerationTimeGrounded:accelerationTimeAirborne);
        if (velocity.y < 0f)
		{
			velocity.y += gravity * fallMultipler * Time.deltaTime;
		}
		else
		{			
			velocity.y += gravity * Time.deltaTime;
		}
	}
    void RecalculatePhysics(bool debug)
    {
        if (debug)
        {
            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        }
    }
    public void OnActionDown()
    {
        isOnDialogTrigger = true; 

        ActionButton();
		//StartCoroutine(DoMeleeAttack(activeAttackTime));
		
    }

    public void OnActionUp()
    {
        isOnDialogTrigger = false;
        //StartCoroutine(DoMeleeAttack(activeAttackTime));
    }


    //REMOÇÃO DA MECANICA DE GRAB
    //// Gruda o player colado a caixa e desfreeza a posição x da caixa
    //public void GrabBox(){

    //    if(nearBox){
    //        nearBox.GetComponent<PushObject>().checkBox(this.gameObject);
    //    }
    //}

    /*IEnumerator DoMeleeAttack(float activeTime)
	{
		meleeAtt.startCheckingCollision();
		Debug.Log("ATTACK");
		yield return new WaitForSeconds(activeTime);
		meleeAtt.stopCheckingCollision();
		Debug.Log("STOP ATTACK");
	}*/
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour {

    [Tooltip("Speed the enemy will perform the patrol")]
    public float PatrolSpeed;
    [Tooltip ("Wait time before going to the next point in path")]
    public float WaitTime;
    [Tooltip("Allow the enemy to turn around as soon as he gets in the current point")]
    public bool allowTurningOnPoint = true;

    // Points the enemy will patrol
    public Transform[] points;

    [HideInInspector] // CPause the patrol to chase target
    public bool isChasingTarget = false;
    public Animator patrolAnim;

    // Current point the enemy is folllowing
    private int destPoint = 0;
    private int currentDestPoint = 0;
    private float timeWaiting;
    private GameObject FOV;

    // Check if the enemy turned around
    private bool turnAround = false;
    // Check if stoped on the point
    private bool stopped = false;

	void Start () {

        timeWaiting = WaitTime;
        patrolAnim = GetComponentInChildren<Animator>();

        // Set the next point the enemy will follow
        if (!isChasingTarget) {
            GotoPoint();
        }

        // Find the FOV to turn with the enemy
        FOV = this.transform.Find("FOV").gameObject;
	}
	
	// Update is called once per frame
	void Update () {

        if (!isChasingTarget) {
            GotoPoint();
            patrolAnim.SetBool("hasTarget", false);

            // Choose the next destination point when the agent gets
            // close to the current one.
            if (Vector2.Distance(transform.position, points[currentDestPoint].position) < 0.1f)
            {
                waitTimeToContinue();
            }
        } else patrolAnim.SetBool("hasTarget", true);

    }

    void waitTimeToContinue(){

        if (timeWaiting <= 0)
        {
            timeWaiting = WaitTime;

            stopped = false;
            patrolAnim.SetBool("idle", false);

            // If not allowed the enemy will turn as soon he starts to
            // follow the next point
            if (!allowTurningOnPoint)
            {
                TurnEnemyAround();
            }
            currentDestPoint = destPoint;
            turnAround = false;
        }
        else
        {
            stopped = true;
            patrolAnim.SetBool("idle", true);

            // If allowed the enemy will turn as soon as he gets
            // in the current point
            if (allowTurningOnPoint){
                TurnEnemyAround();
            }
            timeWaiting -= Time.deltaTime;
        }
    }

    // Turn around the enemy when he gets in the point
    void TurnEnemyAround(){

        if (!turnAround)
        {
            turnAround = true;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            currentDestPoint = destPoint;
            destPoint = (destPoint + 1) % points.Length;

            if (points[destPoint].position.x > transform.position.x)
            {
                
                //estamos rotacionando pela scale pra não estragar visualmente o patrulha e o campo de visão
                transform.localScale = new Vector3(- Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                FOV.transform.localScale = new Vector3( - Mathf.Abs(FOV.transform.localScale.x), FOV.transform.localScale.y, FOV.transform.localScale.z);


                //transform.rotation = Quaternion.Euler(0, 180, 0);
                //FOV.transform.rotation = Quaternion.Euler(0, 0, 235);
            }
            else
            {
                //estamos rotacionando pela scale pra não estragar visualmente o patrulha e o campo de visão
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                FOV.transform.localScale = new Vector3(Mathf.Abs(FOV.transform.localScale.x), FOV.transform.localScale.y, FOV.transform.localScale.z);

                //transform.rotation = Quaternion.Euler(0, 0, 0);
                //FOV.transform.rotation = Quaternion.Euler(0, 0, 125);
            }
        }
    }

    void GotoPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        if (!stopped && points.Length > 0) {
            // Move towars the current point
            transform.position = Vector2.MoveTowards(transform.position, points[destPoint].position, PatrolSpeed * Time.deltaTime);
        }

    }

}

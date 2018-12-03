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
    private EnemyChase _enemyChase;
    private PlayerComponent _player;

	void Start () {

        timeWaiting = WaitTime;
        patrolAnim = GetComponentInChildren<Animator>();

        // Set the next point the enemy will follow
        if (!isChasingTarget) {
            GotoPoint();
        }

        // Find the FOV to turn with the enemy
        FOV = this.transform.Find("FOV").gameObject;
        _enemyChase = GetComponent<EnemyChase>();
        _player = GameObject.FindObjectOfType<PlayerComponent>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!isChasingTarget) {
            GotoPoint();
            patrolAnim.SetBool("hasTarget", false);
            patrolAnim.SetBool("idle", false);

            // Choose the next destination point when the agent gets
            // close to the current one.
            if (Vector2.Distance(transform.position, points[currentDestPoint].position) < 0.1f)
            {
                waitTimeToContinue();
            }
        } 
        //senão, se eu ainda estou perseguindo o inimigo, mas ele está muito perto de mim horizontalmente, 
        //seto no animator para idle porque vou acabar por parar de andar
        //isso é um caso comum quando o player se esconde atras de objetos o patrol estava perseguindo ele
        else if(isChasingTarget && _player.IsHidden && _enemyChase.HorizontalDistanceToTarget() < 0.1f)
        {
            //patrulha: "cade o player???"
            patrolAnim.SetBool("hasTarget", false);
            patrolAnim.SetBool("idle", true);
        }
        //meio que repetindo o raycast já feito no script EnemyChase, mas é uma redundância tolerável
        //se tenho mais chão para andar ainda, sigo persiguindo o player
        else if(Physics2D.Raycast(transform.position, Vector2.down, 2*this.GetComponent<Collider2D>().bounds.extents.y, LayerMask.GetMask("Obstacle")))
        {
            patrolAnim.SetBool("idle", false);
            patrolAnim.SetBool("hasTarget", true);
        }
        //senão, eu parei por não ter mais chão, e tenho que ficar idle
        else
        {
            patrolAnim.SetBool("idle", true);
            patrolAnim.SetBool("hasTarget", false);
        } 

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

        if (!stopped) {
            // Move towars the current point
            Debug.Log("Move towars the current point");
            transform.position = Vector2.MoveTowards(transform.position, points[destPoint].position, PatrolSpeed * Time.deltaTime);

            //fez-se necessário ajustar a rotação por aqui pra consertar um bug quando ele transiciona do chase para o patrol
            //estamos rotacionando pela scale pra não estragar visualmente o patrulha e o campo de visão
            int flip = points[destPoint].position.x < transform.position.x ? 1 : -1;
            transform.localScale = new Vector3(flip * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            if(FOV != null && FOV.transform != null)
            {
                FOV.transform.localScale = new Vector3(flip * Mathf.Abs(FOV.transform.localScale.x), FOV.transform.localScale.y, FOV.transform.localScale.z);
            }
            
        }

    }

}

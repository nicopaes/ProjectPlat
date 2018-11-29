using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour {

    public float ChaseSpeed;
    [Tooltip ("Time the enemy will take till give up to chase target")]
    public float ChaseTime;

    private Transform target;

    private float currentChaseTime = 0;

    // Activate or deactivate the chase
    private bool keepChasing = false;

    private EnemyPatrol pausePatrol;

	// Use this for initialization
	void Start () {

        target = GameObject.FindObjectOfType<PlayerComponent>().transform;
        pausePatrol = this.GetComponent<EnemyPatrol>();
	}
	
	// Update is called once per frame
	void Update () {

        if (keepChasing){
            Debug.Log("Following Target");
            
            Vector2 desiredPos = new Vector2(target.position.x, transform.position.y);
            //antes de se mover, checa se há mais chão
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2*this.GetComponent<Collider2D>().bounds.extents.y, LayerMask.GetMask("Obstacle"));
            if(hit)
            {
                transform.position = Vector2.MoveTowards(transform.position, desiredPos, ChaseSpeed * Time.deltaTime);
            }
            
            currentChaseTime += Time.deltaTime;
        }

        if (currentChaseTime >= ChaseTime){
            keepChasing = false;
            pausePatrol.isChasingTarget = false;
            currentChaseTime = 0;
        }
	}

    public void StartChasingTarget() {
        keepChasing = true;
        pausePatrol.isChasingTarget = true;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Debug.Log("to tocando com trigger");
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
        }
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour {

    public float ChaseSpeed;
    [Tooltip ("Time the enemy will take till give up to chase target")]
    public float ChaseTime;

    [SerializeField]
    private Transform target;

    private float currentChaseTime = 0;

    // Activate or deactivate the chase
    [SerializeField]
    private bool keepChasing = false;

    [SerializeField]
    private EnemyPatrol pausePatrol;

	// Use this for initialization
	void Start ()
    {
        target = GameObject.FindWithTag("Player").transform;
        pausePatrol = this.GetComponent<EnemyPatrol>();
	}
	
	// Update is called once per frame
	void Update () {

        if (keepChasing){
            Debug.Log("Following Target");
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), ChaseSpeed * Time.deltaTime);

            currentChaseTime += Time.deltaTime;
        }

        if (currentChaseTime >= ChaseTime)
        {
            keepChasing = false;
            //pausePatrol.isChasingTarget = false;
            pausePatrol.enabled = true;
            currentChaseTime = 0;
            //this.enabled = false;
        }
	}

    public void StartChasingTarget()
    {
        keepChasing = true;
        //pausePatrol.isChasingTarget = true;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Debug.Log("to tocando com trigger");
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            pausePatrol.enabled = true;
            this.enabled = false;
        }
	}

}

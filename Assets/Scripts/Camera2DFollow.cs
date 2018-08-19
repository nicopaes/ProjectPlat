using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class Camera2DFollow : MonoBehaviour {

    // Usa o hitbox do player para a camera seguir
	public Transform target;

    // Tempo para a camera voltar a centralizar no target
	public float damping = 1;

    // Camera segue a frente do target e quando ele para a camera lentamente centraliza no target
	public float lookAheadFactor = 3;
	public float lookAheadReturnSpeed = 0.5f;

    // Limite para a camera estar a frente do target enquanto ele se move
	public float lookAheadMoveThreshold = 0.1f;   

    // Limite mínimo de distancia entre o target e a altura da camera quando o target pula 
	public float yPosRestriction = -1;
	
    // Distancia original z da camera em relação ao player 
	float offsetZ;

    // Ultima posição e velocidade do target 
	Vector3 lastTargetPosition;
	Vector3 currentVelocity;

    // posição a frente do target
	Vector3 lookAheadPos;

    // Tempo que a função utiliza para ficar procurando o player como Target
	float nextTimeToSearch = 0;
	
	// Use this for initialization
	void Start () {

        // Pega as posições iniciais 
		lastTargetPosition = target.position;
		offsetZ = (transform.position - target.position).z;

        // Verifica se o target possui o PlayerComponent
		Assert.IsTrue(target.GetComponent<PlayerComponent>());
	}
	
	// Update is called once per frame
	void Update () {

        // Se não tiver um target definido procura o player
		if (target == null) {
			FindPlayer ();
			return;
		}

		// only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (target.position - lastTargetPosition).x;
	    bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        // Se o target mudou de posição ou direção, altera a posição da camera a frente do target 
		if (updateLookAheadTarget) {
			lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
		} else {
            // Se o target está parado ajeita a posição da camera lentamente para o centro do target
			lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);	
		}
		
        // Posição atual da camera a frente do target
		Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;

        // Faz a transição da posição da camera para frente do target lentamente
		Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

        // Definite um limite para a camera seguir o player quando ele pula
		newPos = new Vector3 (newPos.x, Mathf.Clamp (newPos.y, yPosRestriction, Mathf.Infinity), newPos.z);

        // Troca a posição da camera
		transform.position = newPos;
		
        // Salva a ultima posição da camera
		lastTargetPosition = target.position;		
	}

    // Fica procurando o player a partir de sua tag durante determinado tempo no nextTimeToSearch
	void FindPlayer () {
		if (nextTimeToSearch <= Time.time) {
			GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
			if (searchResult != null)
				target = searchResult.transform;
			nextTimeToSearch = Time.time + 0.5f;
		}
	}
}

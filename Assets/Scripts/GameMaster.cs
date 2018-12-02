using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameMaster : MonoBehaviour 
{
	public static GameMaster gm;
	public GameObject Player;
    public GameObject Dangers;
    public GameObject speed;

    [Tooltip("Usado para ter persistencia de informações entre as cenas")]
    public GameObject dontDestroyOnLoadPrefab;

    //public int spawnDelay;
	
	[Header("REVIEW THIS")]
	[Space]
	Vector3 spawnPosition;
	public Transform spawnPoint;

	private bool _respawning;   // Verifica se o player está no processo de respawnar

    private float speedNormal;

    [Tooltip("Usado para setar a camera inicial da cena")]
    public CinemachineVirtualCamera initialCamera;

	void Start () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();
		}

        //se não houver um objeto DontDestroyOnLoad na cena, instancia um
        var dont = GameObject.FindObjectOfType<PersistentInfo>();
        if(dont == null)
        {
            GameObject.Instantiate(dontDestroyOnLoadPrefab);
        }

        speedNormal = speed.GetComponent<PlayerComponent>().moveSpeed;

        if(initialCamera != null)
        {
            //seta pra primeira prioridade
            initialCamera.Priority = 50;
            initialCamera.MoveToTopOfPrioritySubqueue();
        }
	}
	void Update()
	{

        //cheat da tecla l, deixar desabilitado pra build final
        // if (Input.GetKey("l"))
        // {
        //     Dangers.SetActive(false);
        //     speed.GetComponent<PlayerComponent>().moveSpeed = 20f;

        // }
        // else { Dangers.SetActive(true);
        //     speed.GetComponent<PlayerComponent>().moveSpeed = speedNormal;
        // }


		if(!Player.activeInHierarchy)
		{
            //         // Se o player não está ativo e não começou a respawnar, ele começa a fazer a animação de respawn
            //if(!_respawning) 
            //{
            //	Debug.Log("Respawning");
            //	StartCoroutine(RespawnPlayerWithDelay(0.5f));
            //}

            Player.GetComponent<PlayerComponent>().KillPlayer();

            

		}
	}

    // Quando o player é tocado por algum perigo ele realiza o respawn
	/*public void RespawnPlayer () {
		
		Debug.Log (spawnPosition + " SpawnPosition");
		Debug.Log ("TO DO: Add Spawn Particles");
	}*/



    public void Exit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour 
{
	public static GameMaster gm;
	public GameObject Player;
    public GameObject Dangers;
    public GameObject speed;

    private ResetOnDeath[] resetableObjects;

    //public int spawnDelay;
	
	[Header("REVIEW THIS")]
	[Space]
	Vector3 spawnPosition;
	public Transform spawnPoint;

	private bool _respawning;   // Verifica se o player está no processo de respawnar

	void Start () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();
		}

        resetableObjects = GameObject.FindObjectsOfType<ResetOnDeath>();

    }
	void Update()
	{

        if (Input.GetKey("l"))
        {
            Dangers.SetActive(false);
            speed.GetComponent<PlayerComponent>().moveSpeed = 20f;

        }
        else { Dangers.SetActive(true);
            speed.GetComponent<PlayerComponent>().moveSpeed = 6.5f;
        }


		if(!Player.activeInHierarchy)
		{
            // Se o player não está ativo e não começou a respawnar, ele começa a fazer a animação de respawn
			if(!_respawning) 
			{
				Debug.Log("Respawning");
				StartCoroutine(RespawnPlayerWithDelay(0.5f));
			}
		}
	}

    // Quando o player é tocado por algum perigo ele realiza o respawn
	/*public void RespawnPlayer () {
		
		Debug.Log (spawnPosition + " SpawnPosition");
		Debug.Log ("TO DO: Add Spawn Particles");
	}*/

    
    //certamente esse não é o melhor lugar para esse código.
        
    // Realiza o respawn do player com um delay
	public IEnumerator RespawnPlayerWithDelay(float delay)
	{
		_respawning = true;
		yield return new WaitForSeconds(delay);
		Player.SetActive(true);
        //principalmente esse aqui, que está mandando resetar todos os objetos resetáveis da cena(ass. Krauss)
        foreach (ResetOnDeath r in resetableObjects)
        {
            //checa só para o caso de o objeto ter sido destruído
            if (r != null)
            {
                r.ResetObject();
            }           
            
        }
		_respawning = false;
	}



    public void Exit()
    {
        Application.Quit();
    }
}

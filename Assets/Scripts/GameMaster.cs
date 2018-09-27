using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour 
{
	public static GameMaster gm;
	public GameObject Player;

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
	}
	void Update()
	{
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

    // Realiza o respawn do player com um delay
	public IEnumerator RespawnPlayerWithDelay(float delay)
	{
		_respawning = true;
		yield return new WaitForSeconds(delay);
		Player.SetActive(true);
		_respawning = false;
	}



    public void Exit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;
	Vector3 spawnPosition;
	public Transform playerPrefab;
	public Transform spawnPoint;

	void Start () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();
			spawnPosition.Set (spawnPoint.position.x, spawnPoint.position.y, 0);
		}
	}

	//public int spawnDelay;

	public void RespawnPlayer () {
		playerPrefab.transform.position = spawnPosition;
		for (int i = 0; i < playerPrefab.transform.childCount; i++) {
			playerPrefab.transform.GetChild (i).transform.position = spawnPosition;
		}
		Debug.Log (playerPrefab.transform.position + " PlayerPrefab");
		Debug.Log (spawnPosition + " SpawnPosition");
		Debug.Log ("TO DO: Add Spawn Particles");
	}

	/*
	public static void KillPlayer (PlayerComponent player) {
		Destroy (player.gameObject);
		gm.RespawnPlayer ();
	}
	*/

}

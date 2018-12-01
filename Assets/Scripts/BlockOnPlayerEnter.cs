using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HidePlayer))]
public class BlockOnPlayerEnter : MonoBehaviour {


	[Header("Script responsável por bloquear o player dentro da caixa. Essa ação é meio que irreversível")]
	private HidePlayer hidePlayer;
	
	// Use this for initialization
	void Start () {
		hidePlayer = GetComponent<HidePlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		//essa gambiarra é final, e basicamente significa que uma vez que o player entrar nessa caixa ele jamais conseguirá sair
		//ainda bem que o jogo termina logo depois disso
		if(hidePlayer.IsPlayerHiddenHere())
		{
			GameObject.FindObjectOfType<PlayerComponent>().BlockPlayerMovement(true);
		}
	}
}

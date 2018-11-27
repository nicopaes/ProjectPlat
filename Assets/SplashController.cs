using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//no splash, qualquer tecla:
		if(Input.anyKeyDown)
		{
			// abre o menu principal
			//GameObject.Find("Canvas").GetComponent<MainMenuUI>().EnableOptionPanel(false);
			//this.gameObject.SetActive(false);

			//começa o jogo (scene 1)
			GameObject.FindObjectOfType<ChangeScene>().ChangeSingleScene("Setor 1 e 2", true);

		}
	}
}

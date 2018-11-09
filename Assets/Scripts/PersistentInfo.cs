using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentInfo : MonoBehaviour {

	
	public List<string> Registry;
	// Use this for initialization
	void Awake () {
		
		//faz o gameObject não ser destruido ao mudar de cena
		Object.DontDestroyOnLoad(this.gameObject);
		
		Registry = new List<string>();
	}

	//obs: estou considerando que, para quem usa esta classe, o problema de tocar somente uma vez na mesma cena já está resolvido
	//isso aqui vai servir pra detectar se já foi visto em uma cena anterior(outra vida), só isso
	
	
}

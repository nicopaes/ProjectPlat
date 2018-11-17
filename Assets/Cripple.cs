using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cripple : MonoBehaviour {


    public GameObject Guarda;
    //public GameObject Graphics;

	// Use this for initialization
	void Start () {
        Guarda.GetComponent<EnemyPatrol>().enabled = false;
      //  Graphics.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

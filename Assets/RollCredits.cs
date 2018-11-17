using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RollCredits : MonoBehaviour {

    public int SceneNumber = 5;
    public GameObject CreditsSong;


	// Use this for initialization
	void Start () {

        //CreditsSong.SetActive(true);
        SceneManager.LoadScene(SceneNumber);

	}
	
}

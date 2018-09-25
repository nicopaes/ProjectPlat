using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggereventofinal : MonoBehaviour {

    public GameObject Player;
    public GameObject Guarda;
    public GameObject Alarme;
    public GameObject Platforma;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Player.transform.position.x > 40f)
        {
            if (Player.transform.position.y > 20f)
            {
                Debug.Log("Foi");
                Guarda.SetActive(true);
                Platforma.SetActive(false);
            }

        }


        if (Guarda.transform.position.x < 45f)
        {
            if (Guarda.transform.position.y < 20f)
            {
                Debug.Log("eta porra");
                Alarme.SetActive(true);


            }

        }

    }
}

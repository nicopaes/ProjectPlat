using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggereventofinal : MonoBehaviour {

    public GameObject Player;
    public GameObject Guarda;
    public GameObject Alarme;
    public GameObject Platforma;
    public GameObject Passos;

	// Use this for initialization
	void Start () {
		
	}

    IEnumerator Wait ()
    {
        Debug.Log("wait");
        Passos.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Guarda.SetActive(true);
        Platforma.SetActive(false);

    }

    // Update is called once per frame
    void Update () {

        if (Player.transform.position.x > 48f)
        {
            if (Player.transform.position.y > 20f)
            {
                Debug.Log("Foi");
                StartCoroutine(Wait());

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

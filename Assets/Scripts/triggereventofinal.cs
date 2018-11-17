using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggereventofinal : MonoBehaviour {

    public GameObject Player;
    public GameObject Guarda;
    public GameObject Alarme;
    public GameObject Platforma;
    public GameObject Passos;
    public float waittime;

	// Use this for initialization
	void Start () {
		
	}

    IEnumerator Wait ()
    {
        //Debug.Log("wait");
        Passos.SetActive(true);
        yield return new WaitForSeconds(waittime);
        Guarda.SetActive(true);
        Platforma.SetActive(false);

    }

    // Update is called once per frame
    void Update () {

        Debug.Log("x =" + Guarda.transform.position.x + "y =" + Guarda.transform.position.y);
        //Debug.Log("y =" + Guarda.transform.position.y);


        if (Player.transform.position.x > 48f)
        {
            if (Player.transform.position.y > 20f)
            {
                //Debug.Log("Foi");
                StartCoroutine(Wait());

            }

        }


        if (Guarda.transform.position.x < 43.5f)
        {
            if (Guarda.transform.position.y < 26f)
            {
                Debug.Log("eta porra");
                Alarme.SetActive(true);


            }

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventOnEnter : MonoBehaviour {

	public UnityEvent Event;
    public bool FadeAudio;
    public GameObject Fader;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator WaitALittle()
    {
        yield return new WaitForSeconds(0.35f);
    }


	public void TriggerChangeScene(string name)
	{
        WaitALittle();

        GameObject.FindObjectOfType<ChangeScene>().ChangeSingleScene(name, true);
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            if (FadeAudio)
            {
                Fader.SetActive(true);
            }
            //aciona evento
			Event.Invoke();
			
			// //se player está interagindo OU eu o dialogo deve começar por proximidade,
            // if(collision.GetComponent<PlayerComponent>().isOnDialogTrigger || StartOnApproach){

            //     ChooseCamera();
            //     activateDialog();

            // }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventOnEnter : MonoBehaviour {

	public UnityEvent Event;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TriggerChangeScene(string name)
	{
		GameObject.FindObjectOfType<ChangeScene>().ChangeSingleScene(name);
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){

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

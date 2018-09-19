using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTalk : MonoBehaviour {

    public GameObject DialogObject;
    public int DialogNumber;

    private bool talkActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){

            if(collision.GetComponent<PlayerComponent>().isOnDialogTrigger){

                activateDialog();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerComponent>().isOnDialogTrigger)
            {
                activateDialog();
            }
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    void activateDialog()
    {
        if (!talkActivated)
        {
            talkActivated = true;
            DialogObject.GetComponent<ManageText>().startDialog(DialogNumber);
        }
    }

}

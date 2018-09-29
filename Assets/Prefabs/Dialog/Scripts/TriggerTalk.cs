using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTalk : MonoBehaviour {

    public GameObject DialogObject;
    public int DialogNumber;

    public GameObject DialogCamera;

    private bool talkActivated = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){

            if(collision.GetComponent<PlayerComponent>().isOnDialogTrigger){

                ChooseCamera();
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
                ChooseCamera();
                activateDialog();
            }
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DialogCamera.SetActive(false);
    }

    void activateDialog()
    {
        if (!talkActivated)
        {
            talkActivated = true;
            DialogObject.GetComponent<ManageText>().startDialog(DialogNumber);
        }
    }

    public void ChooseCamera()
    {
        DialogCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 50;
        DialogCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().MoveToTopOfPrioritySubqueue();

    }

}

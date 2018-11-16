using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBehaviour : MonoBehaviour
{

    public GameObject ThisCamera;
   // public GameObject MockEnemy;
    //public GameObject PlayerHitbox;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ThisCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 50;
            ThisCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().MoveToTopOfPrioritySubqueue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ThisCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 1;
        //ThisCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().MoveToTopOfPrioritySubqueue();
    }



}


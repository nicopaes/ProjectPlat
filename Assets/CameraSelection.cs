using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelection : MonoBehaviour {

    public GameObject ThisCamera;



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            Debug.Log("Selected");
            ThisCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 50;
            ThisCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().MoveToTopOfPrioritySubqueue();

        }
    }

}

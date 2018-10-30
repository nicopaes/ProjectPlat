using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelection : MonoBehaviour {

    public GameObject ThisCamera;

    public Animator LookAhead;
    public GameObject AlternativeAnim;
    public bool IsAnimated;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            Debug.Log("Selected");
            ThisCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 50;
            ThisCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().MoveToTopOfPrioritySubqueue();
            if (IsAnimated)
            {
                if (ThisCamera.GetComponent<Animator>())
                {
                    ThisCamera.GetComponent<Animator>().enabled = true;
                    Debug.Log("existe um animator na minha camera, ou seja, ela faz um look ahead");
                }
                if (AlternativeAnim.GetComponent<Animator>())
                {
                    AlternativeAnim.GetComponent<Animator>().enabled = true;
                    Debug.Log("existe um animator na minha camera, ou seja, ela faz um look ahead");
                }
            }
        }
    }

}

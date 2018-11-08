using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelection : MonoBehaviour {

    public GameObject ThisCamera;

    public Animator LookAhead;
    public GameObject AlternativeAnim;
    public bool IsAnimated;

    private Animator tcAnim;
    private Animator altAnim;
    
    private StateMachineBehaviour[] smb;
    private StateMachineBehaviour[] altsmb;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            Debug.Log("Selected");
            ThisCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 50;
            ThisCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().MoveToTopOfPrioritySubqueue();
            if (IsAnimated)
            {
                if(ThisCamera)
                {
                    tcAnim = ThisCamera.GetComponent<Animator>();
                    //Debug.LogWarning(tcAnim);
                    if (tcAnim != null)
                    {
                        tcAnim.enabled = true;
                        Debug.Log("existe um animator na minha camera, ou seja, ela faz um look ahead");

                        //desativa o animator quando a animação tiver 'acabado':
                        StartCoroutine(MovementBlocker(tcAnim));

                    }
                }
                if(AlternativeAnim)
                {
                    altAnim = AlternativeAnim.GetComponent<Animator>();
                    if (altAnim != null)
                    {
                        altAnim.enabled = true;
                        Debug.Log("existe um animator na minha camera, ou seja, ela faz um look ahead");
                        StartCoroutine(MovementBlocker(altAnim));
                    }
                }
            }
        }
    }

    IEnumerator  MovementBlocker(Animator anim)
    {
        GameObject.FindObjectOfType<PlayerComponent>().BlockPlayerMovement(true);
        while(true)
        {
            if(anim.GetBool("NotEnded"))
            {
                GameObject.FindObjectOfType<PlayerComponent>().BlockPlayerMovement(true);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                Debug.LogWarning("end coroutine");
                GameObject.FindObjectOfType<PlayerComponent>().BlockPlayerMovement(false);
                //disable animator?
                yield break;
            }
        }
        
    }

}

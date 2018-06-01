using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ButtonComponent : InteractiveComponent
{
    public ReactiveComponent attachedReactionObject;

    [SerializeField]
    private bool playerPresence;
    private Collider2D col2D;


    private void OnEnable()
    {
        col2D = GetComponent<Collider2D>();
        PlayerComponent.ActionButton += Action;
    }
    private void OnDisable()
    {
        PlayerComponent.ActionButton -= Action;
    }

    public override void Action()
    {
        //base.Action();
        if (playerPresence)
        {
            Debug.Log("DoorOpen");
            attachedReactionObject.Reaction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hero");
        if (collision.CompareTag("Player"))
        {
            playerPresence = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPresence = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(attachedReactionObject.transform.position, 0.1f);
        //Gizmos.color = Color.white;
        //Gizmos.DrawLine(transform.position, attachedReactionObject.transform.position);
    }
}

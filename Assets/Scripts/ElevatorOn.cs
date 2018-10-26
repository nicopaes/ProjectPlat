using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorOn : MonoBehaviour
{

    public Animator camAnimator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("aloalo");
        camAnimator.SetTrigger("elevatorOn");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogControl : MonoBehaviour {

    private StateMachine stateMachine = new StateMachine();

    private void Start()
    {
        Begin beginState = this.GetComponent<Begin>();
        this.stateMachine.ChangeState(beginState);
        this.stateMachine.ExecuteStateUpdate();
    }

    private void Update()
    {
        //this.stateMachine.ExecuteStateUpdate();
    }


}

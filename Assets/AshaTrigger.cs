using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshaTrigger : MonoBehaviour {

	public Animator anim;

	void TriggerAnimation (){
		anim.SetTrigger("dose");
	}
}

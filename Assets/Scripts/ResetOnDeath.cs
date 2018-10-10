using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//qualquer objeto com esse componente terá seus valores de (position, rotation, scale) resetados quando o player morre
//a função ResetObject é chamada na classe GameMaster

public class ResetOnDeath : MonoBehaviour {

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Vector3 _initialScale;
    
    // Use this for initialization
	void Start () {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _initialScale = transform.localScale;

    }
	
    public void ResetObject()
    {
        transform.SetPositionAndRotation(_initialPosition, _initialRotation);
        transform.localScale = _initialScale;
        this.gameObject.SetActive(true);
    }
}

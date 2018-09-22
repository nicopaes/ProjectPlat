using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour {

    [SerializeField]
    private float _boxSpeed;
    [HideInInspector]
    public bool holdingBox = false;
    private GameObject originalBoxParent;

    private float originalWeight = 1;

    void Update()
    {
        _boxSpeed = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x);
    }

    public void checkBox(GameObject obj)
    {

        if (holdingBox) {
            leaveBox();
        } else {
            holdBox(obj);
        } 

    }

    public void holdBox(GameObject obj){
        
        originalBoxParent = this.transform.parent.gameObject;
        originalWeight = this.GetComponent<Rigidbody2D>().mass;
        this.GetComponent<Rigidbody2D>().mass = originalWeight / 2;
        holdingBox = true;
        Debug.Log("Empurra a caixinha");
        this.transform.parent = obj.transform;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        //nearBox.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        //nearBox.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        // Gruda na caixa e desfreeza o x
    }

    public void leaveBox()
    {
        holdingBox = false;
        Debug.Log("solta a caixinha");
        this.GetComponent<Rigidbody2D>().mass = originalWeight;
        this.transform.parent = originalBoxParent.transform;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;


        //this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;

        // Solta da caixa e freeza o x
    }
}

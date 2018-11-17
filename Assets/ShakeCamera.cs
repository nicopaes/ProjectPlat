using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ShakeCamera : MonoBehaviour {

    public float Magnitude;
    public float Roughness;
    public float FadeInTime;
    public float FadeOutTime;

    // Use this for initialization
    void Start () {
        CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, FadeInTime, FadeOutTime);
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown("n"))
        {
            //Debug.Log("Im in me mums car");
            CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, FadeInTime, FadeOutTime);
        }

	}
}

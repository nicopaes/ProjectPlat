using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BoxAudioControl : MonoBehaviour {

	public AudioSource audioSource;
	//[Tooltip("O volume desejado pra quando a caixa ser arrastada")]
	private float normalVolume;
	private Rigidbody2D rb;

	

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		normalVolume = audioSource.volume;

	}
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs(rb.velocity.x) > 0)
		{
			audioSource.volume = normalVolume;	
		}
		else
		{
			audioSource.volume = 0.0f;
		}
		
	}
}

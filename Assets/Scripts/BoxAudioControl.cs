using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BoxAudioControl : MonoBehaviour {

	public AudioSource audioSource;
	[Tooltip("O valor a ser subtraido do volume da caixa, até chegar a 0.")]
	public float decrement;
	//[Tooltip("O volume desejado pra quando a caixa ser arrastada")]
	private float normalVolume;
	private Rigidbody2D rb;

	

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D>();
		normalVolume = audioSource.volume;
		audioSource.volume = 0.0f;

	}
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs(rb.velocity.x) > 0)
		{
			audioSource.volume = normalVolume;	
		}
		else
		{
			audioSource.volume = Mathf.Max(audioSource.volume - decrement, 0.0f);
		}
		
	}
}

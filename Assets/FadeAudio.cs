using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FadeAudio : MonoBehaviour {

    public AudioMixer Mixer;
    private float OGVolume;
    private float CurrentVolume;
    public float DecaySpeed;


    public bool MuteAll = false;

	// Use this for initialization
    void Start () {
        OGVolume = GetMasterLevel();
        CurrentVolume = OGVolume;
        Debug.Log(OGVolume);
        //FadeSounds();
	}

    public float GetMasterLevel()
    {
        float value;
        bool result = Mixer.GetFloat("MusicVol", out value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0f;
        }
    }

    public void FadeSounds ()
    {
        Mixer.SetFloat("MusicVol", CurrentVolume);
        CurrentVolume -= Time.deltaTime*DecaySpeed;
        Debug.Log(CurrentVolume);
        //Mixer.SetFloat("MusicVol", CurrentVolume);


        if (CurrentVolume < -32)
        {
            MuteAll = false;
            return; }
    }



    private void Update()
    {
        Debug.Log(CurrentVolume);
        if(MuteAll)
        {
            FadeSounds();
        }
    }

}

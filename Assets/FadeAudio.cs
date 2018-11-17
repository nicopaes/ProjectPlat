using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FadeAudio : MonoBehaviour {

    public AudioMixer Mixer;
    private float OGVolume;

    public bool MuteAll = false;

	// Use this for initialization
	void Start () {
        OGVolume = GetMasterLevel();
        Debug.Log(OGVolume);
	}

    public float GetMasterLevel()
    {
        float value;
        bool result = Mixer.GetFloat("masterVol", out value);
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


    }



    private void Update()
    {
        if(MuteAll)
        {
            FadeSounds();
        }
    }

}

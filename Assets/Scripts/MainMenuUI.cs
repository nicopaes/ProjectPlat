using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuUI : MonoBehaviour {

	private GameObject _mainPanel;
	private GameObject _optionsPanel;
    public Transform AudioListener;

    public AudioMixer MusicVol;
    public AudioMixer SFXVol;


    [Range(0.0f,1.0f)]
    public float SfxVol;

    public AudioClip select;

	
	// Use this for initialization
	void Start () {
		_mainPanel = transform.Find("MainPanel").gameObject;
		if(_mainPanel == null)
        {
            Debug.LogWarning("Main panel não encontrado. Ele esta presente no canvas com o nome \"MainPanel\"? ");
        }

		_optionsPanel = transform.Find("OptionPanel").gameObject;
		if(_optionsPanel == null)
        {
            Debug.LogWarning("Option panel não encontrado. Ele esta presente no canvas com o nome \"OptionPanel\"? ");
        }

	}
	

    public void SetVolume (float volume)
    {
        Debug.Log(volume);
        MusicVol.SetFloat("MusicVol", volume);
        if(volume < -29.5f)
        {
            MusicVol.SetFloat("MusicVol", -80.0f);
        }

    }

    public void SetVolumeSFX(float volume)
    {
        Debug.Log(volume);
        MusicVol.SetFloat("SFXVol", volume);
        if (volume < -29.5f)
        {
            MusicVol.SetFloat("SFXVol", -80.0f);
        }

    }

    public void PlaySound (AudioClip sfx)

    {
        Debug.Log("barulho");
        //AudioSource.PlayClipAtPoint(sfx, AudioListener.position, SfxVol);
        PlayClipAt(sfx, AudioListener.position);

    }


    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio"); // create the temp object
        tempGO.transform.position = pos; // set its position
        AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
        aSource.clip = clip; // define the clip
        //aSource.outputAudioMixerGroup = SfxVol;
        aSource.Play(); // start the sound
        Destroy(tempGO, clip.length); // destroy object after clip duration
        return aSource; // return the AudioSource reference
    }


    public void EnableOptionPanel(bool enabled)
	{
		_optionsPanel.SetActive(enabled);
		_mainPanel.SetActive(!enabled);
	}

	

}

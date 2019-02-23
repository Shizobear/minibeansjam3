using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour {

	// Use this for initialization
	public AudioMixer audioMixer;
	public void SetVolume(float value) {
		
		audioMixer.SetFloat("Volume", value);

	}
}

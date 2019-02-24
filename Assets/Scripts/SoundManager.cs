using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{


    private GameObject sound_manager_gameobject;
    [SerializeField]
    private AudioMixer mixer;
    private AudioSource audio_source;
    private static SoundManager instance;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            this.enabled = false;

    }
    private void Start()
    {
        audio_source = this.GetComponent<AudioSource>();
        audio_source.loop = true;
    }

    public static SoundManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("Sound Manager");
            instance = go.AddComponent<SoundManager>();
        }

        return instance;
    }

    public void PlayOneShot(AudioClip _clip)
    {
        audio_source.PlayOneShot(_clip);
    }

    public void SetBGM(AudioClip _clip)
    {
        audio_source.clip = _clip;
    }




}
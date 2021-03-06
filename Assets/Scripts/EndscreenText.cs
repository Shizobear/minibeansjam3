﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndscreenText : MonoBehaviour
{

    // Use this for initialization
    private Fish player;
    [SerializeField]
    private TextMeshProUGUI valueText;
    [SerializeField]
    private TextMeshProUGUI weightText;
    private float weight, value;
    [SerializeField]
    private AudioClip win_sound;
    void Start()
    {
        player = Fish.GetReference();
        weight = player.GetWeight() * 1000f;
        value = (weight / 1000f * 12 - 3) / 2;
        value = Mathf.Round(value * 100f) / 100f;
        SoundManager.GetInstance().PlayOneShot(win_sound);

    }

    // Update is called once per frame
    void Update()
    {

        weightText.SetText(weight.ToString() + " g");
        valueText.SetText(value.ToString() + " €");

    }
}

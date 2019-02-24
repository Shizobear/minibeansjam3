using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class WeightText : MonoBehaviour
{

    private Fish player;
    private Text text_element;
    private string score_prefix = "Weight: ";
    private string score_suffix = "kg";
    // Use this for initialization
    void Start()
    {
        player = Fish.GetReference();
        text_element = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text_element.text = score_prefix + player.GetWeight() + score_suffix;
    }
}

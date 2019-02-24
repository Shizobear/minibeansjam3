using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndscreenText : MonoBehaviour {

	// Use this for initialization
	private Fish player;
	[SerializeField]
    private TextMeshProUGUI valueText;
	[SerializeField]
	private TextMeshProUGUI weightText;
	private float weight, value;

	void Awake () {
		player = Fish.GetReference();
	}

	void Start()
	{
		weight = player.GetWeight() * 1000f;
		value = weight / 3 * 8 - 5;
		value = Mathf.Round(value * 100f) * 100f;
	}
	
	// Update is called once per frame
	void Update () {

		weightText.SetText(weight.ToString() + " g");
		valueText.SetText(value.ToString() + " €");

	}
}

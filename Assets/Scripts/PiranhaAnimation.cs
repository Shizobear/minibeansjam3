using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaAnimation : MonoBehaviour {

	private Animator anim;
	private Piranha piranha;
	private Vector3 direction;
	void Start () {
		anim = this.GetComponent<Animator>();
		piranha = this.GetComponent<Piranha>();
	}
	
	// Update is called once per frame
	void Update () {
		direction = piranha.GetCurrentDirection();
		anim.SetFloat("directionX", direction.x);
		anim.SetFloat("directionY", direction.y);

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnimation : MonoBehaviour {

	// Use this for initialization
	private Animator anim;
	private iAnimatibleDirection player;
	private Vector3 direction;
	void Start () {
		anim = this.GetComponent<Animator>();
        player = this.GetComponent<iAnimatibleDirection>();
    }
	
	// Update is called once per frame
	void Update () {
		direction = player.GetCurrentDirection();
		anim.SetFloat("directionX", direction.x);
		anim.SetFloat("directionY", direction.y);

	}
}

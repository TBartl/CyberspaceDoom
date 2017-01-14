using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour {

	public enum modes {
		pistol,
		shotgun,
		lazer
	}

	Animator anim;

	void Awake() {
		anim = GetComponent<Animator>();
	}

	void Update() {
		if (Input.GetButton("Fire1")) anim.SetTrigger("fire");
	}
}

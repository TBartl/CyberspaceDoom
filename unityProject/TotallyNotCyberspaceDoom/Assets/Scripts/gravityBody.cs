﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityBody : MonoBehaviour {

	public gravityAttractor planet;
	public float gravity = -10f;

	Rigidbody rigi;

	void Start () {
		rigi = gameObject.GetComponent<Rigidbody>();
		rigi.useGravity = false;
		rigi.constraints = RigidbodyConstraints.FreezeRotation;
	}
	
	void FixedUpdate() {
		planet.GravPull(transform, gravity);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastVelocity : MonoBehaviour {
	[HideInInspector] public Vector3 lastVelocity;
	Rigidbody rb;

	void Awake() {
		rb = GetComponent<Rigidbody>();

	}
	
	void FixedUpdate () {
		lastVelocity = rb.velocity;
	}
}

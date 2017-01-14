using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityBody : MonoBehaviour {

	public gravityAttractor planet;
	Rigidbody rigi;

	void Start () {
		rigi = gameObject.GetComponent<Rigidbody>();
		rigi.useGravity = false;
		rigi.constraints = RigidbodyConstraints.FreezeRotation;
	}
	
	void FixedUpdate() {
		planet.GravPull(transform);
	}
}

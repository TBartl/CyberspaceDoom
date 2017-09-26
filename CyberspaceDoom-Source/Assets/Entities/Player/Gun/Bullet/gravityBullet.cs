using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityBullet : MonoBehaviour {

	public gravityAttractor planet;
	public float smolGravity = -2f;
	Rigidbody rigi;

	void Start () {
		rigi = gameObject.GetComponent<Rigidbody>();
		rigi.useGravity = false;
		rigi.constraints = RigidbodyConstraints.FreezeRotation;
	}

	void FixedUpdate() {
		planet.GravPull(transform, smolGravity);
	}
}

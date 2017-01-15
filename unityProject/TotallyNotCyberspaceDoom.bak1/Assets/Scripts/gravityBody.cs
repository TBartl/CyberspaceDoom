using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityBody : MonoBehaviour {
	public float gravity = -10f;

	Rigidbody rigi;

	void Start () {
		rigi = gameObject.GetComponent<Rigidbody>();
		rigi.useGravity = false;
		rigi.constraints = RigidbodyConstraints.FreezeRotation;
	}
	
	void FixedUpdate() {
		Vector3 targetDir = (transform.position).normalized;
		Vector3 bodyUp = transform.up;
		
		rigi.AddForce(bodyUp * gravity);
		transform.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * transform.rotation;
		rigi.AddForce(targetDir * gravity);
	}
}

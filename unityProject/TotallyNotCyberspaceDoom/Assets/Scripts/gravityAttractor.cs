using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class gravityAttractor : MonoBehaviour {

	public float gravity = -10f;


	public void GravPull(Transform body) {
		Vector3 targetDir = (body.position - transform.position).normalized;
		Vector3 bodyUp = body.up;

		Rigidbody rigi = body.GetComponent<Rigidbody>();
		rigi.AddForce(bodyUp * gravity);
		body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
		rigi.AddForce(targetDir * gravity);
	}
}

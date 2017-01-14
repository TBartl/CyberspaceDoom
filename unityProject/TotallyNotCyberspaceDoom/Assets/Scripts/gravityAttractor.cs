using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class gravityAttractor : MonoBehaviour {


	public void GravPull(Transform body, float grav) {
		Vector3 targetDir = (body.position - transform.position).normalized;
		Vector3 bodyUp = body.up;

		Rigidbody rigi = body.GetComponent<Rigidbody>();
		rigi.AddForce(bodyUp * grav);
		body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
		rigi.AddForce(targetDir * grav);
	}
}

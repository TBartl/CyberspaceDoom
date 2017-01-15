using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monolith : MonoBehaviour {

	bool monolithActivated = false;
	MeshRenderer mr;

	void Awake() {
		mr = this.GetComponent<MeshRenderer>();
	}

	void OnCollisionEnter(Collision coll) {

		if (!monolithActivated &&
			(coll.collider.tag == "Player" || coll.collider.tag == "Bullet") && 
			transform.childCount == 0) {

			Debug.Log("Doot");
			mr.material = mr.materials[1];
			Gate.s.MonolithActivated();
			monolithActivated = true;
		}
	}
}

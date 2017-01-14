using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	int health = 3;
	public float invincibilityFrames = 1f;
	bool canGetHit = true;
	public List<MeshRenderer> hearts;

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Enemy" && canGetHit) {
			health -= 1;
			if (health >= 0) {
				hearts[health].enabled = false;
			}
			StartCoroutine(Invincibility());
		}
	}
	
	IEnumerator Invincibility() {
		canGetHit = false;
		yield return new WaitForSeconds(invincibilityFrames);
		canGetHit = true;
	}
}

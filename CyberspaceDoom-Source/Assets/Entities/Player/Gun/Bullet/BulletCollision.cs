using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour {

	public float timeTillDeactivate;
	public GameObject explosionPrefab;

	void OnEnable() {
		StartCoroutine(CountDown());
	}

	IEnumerator CountDown() {
		yield return new WaitForSecondsRealtime(timeTillDeactivate);
		HitSomething();
	}

	void OnCollisionEnter(Collision collision) {
		HitSomething();
	}

	public void HitSomething() {
        Destroy(this.gameObject);
		GameObject explosion = (GameObject)Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
		Destroy(explosion, 2);

	}
}

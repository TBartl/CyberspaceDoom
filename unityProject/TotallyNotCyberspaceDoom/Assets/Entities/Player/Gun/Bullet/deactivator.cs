using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactivator : MonoBehaviour {

	public float timeTillDeactivate;
	public GameObject explosionPrefab;

	void OnEnable() {
		StartCoroutine(CountDown());
	}

	IEnumerator CountDown() {
		yield return new WaitForSecondsRealtime(timeTillDeactivate);
		Deactivate();
	}

	void OnCollisionEnter(Collision collision) {
		Deactivate();
	}

	void Deactivate() {
		
		if (!gameObject.activeSelf)
			return;
		gameObject.SetActive(false);

		GameObject explosion = (GameObject)Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
		Destroy(explosion, 2);

	}
}

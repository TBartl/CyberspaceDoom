using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactivator : MonoBehaviour {

	public float timeTillDeactivate;

	void OnEnable() {
		StartCoroutine(CountDown());
	}

	IEnumerator CountDown() {
		yield return new WaitForSecondsRealtime(timeTillDeactivate);
		gameObject.SetActive(false);
	}
}

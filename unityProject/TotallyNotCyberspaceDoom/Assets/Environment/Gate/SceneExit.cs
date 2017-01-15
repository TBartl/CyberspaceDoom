using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneExit : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && gameObject.activeSelf) {
			Debug.Log("Go to the next level");
		}
	}
}

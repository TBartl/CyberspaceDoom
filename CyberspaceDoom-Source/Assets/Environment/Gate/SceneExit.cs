using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && gameObject.activeSelf) {
			int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
			SceneManager.LoadScene(nextSceneIndex);
		}
	}
}

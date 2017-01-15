using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && gameObject.activeSelf) {
			Debug.Log("Go to the next level");
			int nextScenceIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
			SceneManager.LoadScene(nextScenceIndex);
			player.WorldChange();
			player.instance.SetGun();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueToNextScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Continue());
	}

    IEnumerator Continue() {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}

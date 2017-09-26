using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stayAlive : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {
	public Material litMaterial;
	MeshRenderer mr;
	List<Monolith> monoliths;
	public static Gate s;
	int monolithsActivated = 0;

	void Awake() {
		mr = this.GetComponent<MeshRenderer>();
		s = this;

		monoliths = new List<Monolith>();
		List<GameObject> monolithGOs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monolith"));
		foreach (GameObject monolith in monolithGOs) {
			monoliths.Add(monolith.GetComponent<Monolith>());
		}
		while (monoliths.Count > 2) {
			Monolith m = monoliths[Random.Range(0,monoliths.Count)];
			monoliths.Remove(m);
			Destroy(m.gameObject);
		}
	}

	// Debug key in the literal worst possible place because I'm lazy
	void Update() {
		if (Input.GetKeyDown(KeyCode.P)) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false; ;
		}
	}

	public void MonolithActivated() {
		Light(monolithsActivated);
		monolithsActivated += 1;
		if (monolithsActivated == 2)
			transform.GetChild(0).gameObject.SetActive(true);

	}

	void Light(int i) {
		Material[] mats = mr.materials;
		if (i == 0)
			mats[2] = litMaterial;
		else if (i == 1)
			mats[3] = litMaterial;
		mr.materials = mats;

	}
}

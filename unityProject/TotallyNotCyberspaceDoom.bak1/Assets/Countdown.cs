using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
	public float remainingTime = 60f;
	public static Countdown s;
	Text text;

	void Awake() {
		s = this;
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		remainingTime -= Time.deltaTime;
		text.text = Mathf.RoundToInt(remainingTime).ToString();

	}
}

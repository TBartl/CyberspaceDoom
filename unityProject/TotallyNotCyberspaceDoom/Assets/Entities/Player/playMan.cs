using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMan : MonoBehaviour {

	public static playMan instance = null;

	public bool onRain;
	public bool onSnow;
	public bool onDark;

	public Canvas startMenu;

	void Start () {
		onRain = false;
		onSnow = false;
		onDark = false; 
	}

	void Awake() {
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);
		DontDestroyOnLoad(gameObject);

		Application.targetFrameRate = 80;
	}


	public void StartRain() {
		onRain = true;
//		AkSoundEngine.SetState("Planets", "Rain");
		AkSoundEngine.SetSwitch("Ambiences", "Ice", gameObject);

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		startMenu.enabled = false;
	}

	public void StartSnow() {
		onSnow = true;
		AkSoundEngine.SetState("Planets", "Ice");
	}

	public void StartDark() {
		onDark = true;
		AkSoundEngine.SetState("Planets", "Shadow");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMan : MonoBehaviour {

	public static playMan instance = null;

	public bool onRain;
	public bool onSnow;
	public bool onDark;

	public Canvas startMenu;

	AkInitSettings initSettings;
	AkPlatformInitSettings platformInitSettings;

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
//		AkSoundEngine.SetState("Planets", "Ice");
		AkSoundEngine.PostEvent("RainAmbience", gameObject);

//		AkSoundEngine.SetSwitch("Ambiences", "Ice", gameObject);


		startMenu.enabled = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		player.started = true;
	}

	public void StartSnow() {
		onSnow = true;
//		AkSoundEngine.SetState("Planets", "Ice");
		AkSoundEngine.PostEvent("IceAmbience", gameObject);
	}

	public void StartDark() {
		onDark = true;
//		AkSoundEngine.SetState("Planets", "Shadow");
		AkSoundEngine.PostEvent("ShadowAmbience", gameObject);
	}
}

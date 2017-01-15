using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMan : MonoBehaviour {

	public static playMan instance = null;

	public Transform mcPlayer;
	public Transform spawnPoint;

	public bool onRain;
	public bool onSnow;
	public bool onDark;

	public bool paused;

	public Canvas startMenu;
	public GameObject mainPanel;
	public GameObject creditsPanel;
	public GameObject pausePanel;
	UnityStandardAssets.ImageEffects.BlurOptimized blur;


	Camera mainCam;

	AkInitSettings initSettings;
	AkPlatformInitSettings platformInitSettings;


	void Awake() {
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);
		DontDestroyOnLoad(gameObject);

		Application.targetFrameRate = 80;

		mainCam = Camera.main;
		blur = mainCam.GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>();

		paused = true;
		mainPanel.SetActive(true);
		creditsPanel.SetActive(false);
		pausePanel.SetActive(false);
	}

	void Start () {
		onRain = false;
		onSnow = false;
		onDark = false;

		spawnPoint = GameObject.FindGameObjectWithTag("spawn").transform;
	}

	public void StartGame() {
		mainPanel.SetActive(false);
		blur.enabled = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		player.started = true;
		paused = false;
	}
		
	public void StartRain() {
		onRain = true;
		AkSoundEngine.SetState("States", "RainPlanet");
		AkSoundEngine.PostEvent("PlayMusic", gameObject);
		AkSoundEngine.PostEvent("RainAmbience", gameObject);

//		AkSoundEngine.SetSwitch("Ambiences", "Ice", gameObject);
		mcPlayer.position = Vector3.up * 10f;
	}

	public void StartSnow() {
		onSnow = true;
		mcPlayer.position = Vector3.up * 10f;
		AkSoundEngine.SetState("States", "IcePlanet");
		AkSoundEngine.PostEvent("IceAmbience", gameObject);
	}

	public void StartDark() {
		onDark = true;
		mcPlayer.position = Vector3.up * 10f;
		AkSoundEngine.SetState("States", "ShadowPlanet");
//		AkSoundEngine.PostEvent("PlayMusic", gameObject);
		AkSoundEngine.PostEvent("ShadowAmbience", gameObject);
	}

	void Pause() {
		if (paused) {
			Time.timeScale = 0f;
			pausePanel.SetActive(true);
			blur.enabled = true;

			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else {
			Time.timeScale = 1f;
			pausePanel.SetActive(false);
			mainPanel.SetActive(false);
			blur.enabled = false;

			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	public void TogglePause() {
		if (!paused) paused = true;
		else paused = false;
		Pause();

	}

	public void EndGame() {
		Application.Quit();
	}

	public void ToCredits() {
		creditsPanel.SetActive(true);
		mainPanel.SetActive(false);
	}

	public void FromCredits() {
		creditsPanel.SetActive(false);
		mainPanel.SetActive(true);
	}
}

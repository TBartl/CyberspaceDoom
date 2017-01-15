using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(Rigidbody))]
public class player : MonoBehaviour {

	public static bool started;
	public gun gunScript;

	public float mouseSensitivityX = 3.5f;
	public float mouseSensitivityY = 3.5f;
	public float speed = 15f;
	public float jumpForce = 550f;
	public LayerMask groundMask;

	CapsuleCollider capColl;
	Transform camTrans;
	float vertRot;
	Vector3 moveAmount;
	Vector3 smoothMovment;
	Rigidbody rigi;
	bool onGround = true;

	public static Player rewiredPlayer;

	void Start() {
		DontDestroyOnLoad(gameObject);
		started = false;

		camTrans = Camera.main.transform;
		rigi = gameObject.GetComponent<Rigidbody>();
		capColl = gameObject.GetComponent<CapsuleCollider>();
		rewiredPlayer = ReInput.players.GetPlayer(0);
	}

	void Update() {
		if (started) {
			if (!playMan.instance.paused) {
				LookingUpdate();
				MovingUpdate();
				JumpingUpdate();
			}
			if (rewiredPlayer.GetButtonDown("Pause")) playMan.instance.TogglePause();
		}
	}

	// fix this later with controller support
	void LookingUpdate() {
		transform.Rotate(Vector3.up * rewiredPlayer.GetAxis("CamHorz") * mouseSensitivityX);
		vertRot += rewiredPlayer.GetAxis("CamVert") * mouseSensitivityY;
		vertRot = Mathf.Clamp(vertRot, -60f, 60f);
		camTrans.localEulerAngles = Vector3.left * vertRot;
	}

	void MovingUpdate() {
		Vector3 moveDir = new Vector3(rewiredPlayer.GetAxis("Horz"), 0, rewiredPlayer.GetAxisRaw("Vert")).normalized;
		Vector3 targetMoveAmount = moveDir * speed;
		// the smaller this last value is the better
		moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMovment, 0.15f);

//		if (moveDir != Vector3.zero) AkSoundEngine.PostEvent("Footsteps", gameObject);

	}

	void JumpingUpdate() {
		if (rewiredPlayer.GetButtonDown("Jump") && onGround) rigi.AddForce(transform.up * jumpForce);
		onGround = false;
		Ray groundRay = new Ray(transform.position, -transform.up);
		RaycastHit groundHit;
		if (Physics.Raycast(groundRay, out groundHit, (capColl.height/2) + 0.1f, groundMask)) onGround = true;
	}

	void FixedUpdate() {
		rigi.MovePosition(rigi.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
	}


	//change worlds
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "progress") {
			if (playMan.instance.onRain) {
				SceneManager.LoadScene("final_snow");
				playMan.instance.StartSnow();
				playMan.instance.onRain = false;
			}
			else if (playMan.instance.onSnow) {
				SceneManager.LoadScene("final_dark");
				playMan.instance.StartDark();
				playMan.instance.onSnow = false;
			}
			gunScript.Setup();
		}
	}



}

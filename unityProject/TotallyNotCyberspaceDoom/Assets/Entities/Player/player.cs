using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class player : MonoBehaviour {

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

	void Start() {
		camTrans = Camera.main.transform;
		rigi = gameObject.GetComponent<Rigidbody>();
		capColl = gameObject.GetComponent<CapsuleCollider>();
	}

	void Update() {
		LookingUpdate();
		MovingUpdate();
		JumpingUpdate();
	}

	// fix this later with controller support
	void LookingUpdate() {
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
		vertRot += Input.GetAxis("Mouse Y") * mouseSensitivityY;
		vertRot = Mathf.Clamp(vertRot, -60f, 60f);
		camTrans.localEulerAngles = Vector3.left * vertRot;
	}

	void MovingUpdate() {
		Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
		Vector3 targetMoveAmount = moveDir * speed;
		// the smaller this last value is the better
		moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMovment, 0.15f);  
	}

	void JumpingUpdate() {
		if (Input.GetButtonDown("Jump") && onGround) rigi.AddForce(transform.up * jumpForce);
		onGround = false;
		Ray groundRay = new Ray(transform.position, -transform.up);
		RaycastHit groundHit;
		if (Physics.Raycast(groundRay, out groundHit, (capColl.height/2) + 0.1f, groundMask)) onGround = true;
	}

	void FixedUpdate() {
		rigi.MovePosition(rigi.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(Rigidbody))]
public class Player : MonoBehaviour {

    public Vector2 sensitivity = new Vector2(3.5f, 3.5f);
    
	Transform cameraTransform;
	float vertRot;

    public float acceleration = 1f;
    public float maxMoveSpeed = 15f;
    Rigidbody rigid;

	void Start() {
		cameraTransform = Camera.main.transform;
		rigid = gameObject.GetComponent<Rigidbody>();
	}

	void Update() {
		LookingUpdate();
	}

	// fix this later with controller support
	void LookingUpdate() {
		transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * sensitivity.x);
		vertRot += Input.GetAxisRaw("Mouse Y") * sensitivity.y;
		vertRot = Mathf.Clamp(vertRot, -60f, 60f);
		cameraTransform.localEulerAngles = Vector3.left * vertRot;
	}
    

	void FixedUpdate() {
        Vector3 moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized);
        if (Vector3.Dot(moveDirection, rigid.velocity.normalized) * rigid.velocity.magnitude < maxMoveSpeed)
            rigid.velocity += moveDirection * acceleration * Time.deltaTime;
	}
}

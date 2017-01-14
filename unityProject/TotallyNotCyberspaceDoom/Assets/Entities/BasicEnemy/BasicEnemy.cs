using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {
	public Transform mesh;
	Rigidbody rigid;
	Transform targetPlayer;
	bool chasing = false;
	public float aggroRadius = 10f;
	public float speed = 4f;
	public float rotationSpeed = 20f;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody>();
		targetPlayer = GameObject.Find("player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		CheckPlayerClose();
	}

	void CheckPlayerClose() {
		if (chasing != InRangeOfPlayer())
			chasing = !chasing;

		else {
			float dist = Vector3.Distance(targetPlayer.position, transform.position);
			Vector3 newDirection = targetPlayer.position - transform.position + Mathf.Sqrt(dist) * targetPlayer.up;
			rigid.velocity = newDirection.normalized * speed;

			transform.rotation = Quaternion.Lerp(
				transform.rotation,
				Quaternion.LookRotation((targetPlayer.position - transform.position).normalized, transform.position.normalized),
				Time.deltaTime * rotationSpeed);

		}
	}

	bool InRangeOfPlayer() {
		return Vector3.Distance(transform.position, targetPlayer.position) <= aggroRadius;
	}
}

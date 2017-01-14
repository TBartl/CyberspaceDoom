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
		chasing = false;
		StartCoroutine(Roam());
	}
	
	// Update is called once per frame
	void Update () {
		CheckPlayerClose();
		transform.rotation = Quaternion.Lerp(
			transform.rotation,
			Quaternion.LookRotation((targetPlayer.position - transform.position).normalized, transform.position.normalized),
			Time.deltaTime * rotationSpeed);
	}

	void CheckPlayerClose() {
		if (chasing) {
			if (!InRangeOfPlayer()) {
				StopCoroutine(Chase());
				StartCoroutine(Roam());
			}
		}
		else {
			if (InRangeOfPlayer()) {
				StopCoroutine(Roam());
				StartCoroutine(Chase());
			}
			
		}
	}

	bool InRangeOfPlayer() {
		return Vector3.Distance(transform.position, targetPlayer.position) <= aggroRadius;
	}

	IEnumerator Roam() {
		chasing = false;
		yield return null;
	}
	IEnumerator Chase() {
		chasing = true;
		while (true) {
			Vector3 newVelocity = (targetPlayer.position - transform.position).normalized * speed;
			rigid.velocity = newVelocity;
			yield return null;
		}
	}
}

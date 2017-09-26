using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	protected Rigidbody rigid;
	protected Transform targetPlayer;
	public float aggroRadius = 10f;
	public bool chasing = false;

	// Use this for initialization
	void Start() {
		rigid = GetComponent<Rigidbody>();
		targetPlayer = GameObject.Find("Player").transform;
	}

	// Update is called once per frame
	void Update () {
		CheckPlayerClose();
	}

	void CheckPlayerClose() {
		if (!chasing) {
			if (InRangeOfPlayer())
				chasing = true;
		}
		else {
			ChasePlayer();
		}
	}
	
	protected virtual void ChasePlayer() {
	}

	bool InRangeOfPlayer() {
		return (Vector3.Distance(transform.position, targetPlayer.position) <= aggroRadius);
	}
}

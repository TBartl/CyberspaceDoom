using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy {
	public Transform mesh;
	public float speed = 4f;
	public float rotationSpeed = 20f;

	
	protected override void ChasePlayer() {
		float dist = Vector3.Distance(targetPlayer.position, transform.position);
		Vector3 newDirection = targetPlayer.position - transform.position + Mathf.Sqrt(dist) * targetPlayer.up;
		rigid.velocity = newDirection.normalized * speed;

		transform.rotation = Quaternion.Lerp(
			transform.rotation,
			Quaternion.LookRotation((targetPlayer.position - transform.position).normalized, transform.position.normalized),
			Time.deltaTime * rotationSpeed);
	}

	
}

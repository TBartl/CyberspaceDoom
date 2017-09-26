using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperEnemy : Enemy {
	public float maxTimeToJump = 2f;
	public float jumpStrength = 10f;
	bool canJump = true;
	Animator anim;

	void Awake() {
		anim = this.GetComponent<Animator>();
	}

	protected override void ChasePlayer() {
		anim.SetFloat("Velocity", Vector3.Dot(rigid.velocity.normalized, transform.up));
		if (canJump) {
			Vector3 direction = Vector3.zero;
			direction += Vector3.up * 1.8f;
			direction += (targetPlayer.position - this.transform.position).normalized;
			direction = direction.normalized;
			rigid.velocity = direction * jumpStrength;
			transform.rotation = Quaternion.LookRotation((targetPlayer.position - transform.position).normalized, transform.position.normalized);
			anim.SetBool("Jumping", true);
			StartCoroutine(JustJumped());
		}
	}

    IEnumerator MaintainVelocityForABit(Vector3 velocity) {
        for (float t = 0; t < .2f; t += Time.deltaTime) {
            rigid.velocity = velocity;
            yield return null;
        }
    }

	IEnumerator JustJumped() {
		canJump = false;
		yield return new WaitForSeconds(maxTimeToJump);
		canJump = true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour {
	SkinnedMeshRenderer mr;
	Color originalColor;
	public float knockbackDist = .5f;


	// Use this for initialization
	void Start () {
		mr = GetComponentInChildren<SkinnedMeshRenderer>();
		originalColor = mr.material.GetColor("_Vertex_color");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Bullet") {
			StartCoroutine(FlashRed());
			StartCoroutine(Knockback(collision.collider.GetComponent<LastVelocity>().lastVelocity.normalized));

		}
	}

	IEnumerator FlashRed() {
		mr.material.SetColor("_Vertex_color", Color.red);
		yield return new WaitForSeconds(.2f);
		mr.material.SetColor("_Vertex_color", originalColor);
	}

	IEnumerator Knockback(Vector3 direction) {
		float totalTime = .3f;
		for (float t = 0; t < totalTime; t+= Time.deltaTime) {
			this.transform.position += direction * knockbackDist * Time.deltaTime / totalTime;
			yield return null;
		}
	}
}

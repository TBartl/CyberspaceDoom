using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour {
	Renderer mr;
	Color originalColor;
	public int health;
	public float knockbackDist = .5f;
	Enemy e;


	// Use this for initialization
	void Start () {
		mr = GetComponentInChildren<Renderer>();
		originalColor = mr.material.GetColor("_Vertex_color");
		e = this.GetComponent<Enemy>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Bullet") {
			health -= 1;
			if (e)
				e.chasing = true;
			if (health <= 0) {
				Destroy(this.gameObject);
				return;
			}
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

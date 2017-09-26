using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	int health = 3;
	public float invincibilityFrames = 1f;
	bool canGetHit = true;
	public List<MeshRenderer> hearts;
	public SpriteRenderer redFlashSr;
	Vector2 redFlashRange = new Vector2(0, .4f);
	public GameObject gun;

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Enemy" && canGetHit) {
			health -= 1;
			if (health >= 0) {
				hearts[health].enabled = false;
			}
			StartCoroutine(Invincibility());
			StartCoroutine(FlashRed());
			if (health == 0) {
				StartCoroutine(RestartLevel());

			}
		}
	}
	
	IEnumerator Invincibility() {
		canGetHit = false;
		yield return new WaitForSeconds(invincibilityFrames);
		canGetHit = true;
	}
	IEnumerator FlashRed() {
		Color c = Color.red;
		c.a = redFlashRange.y;
		redFlashSr.color = c;

		float maxT = invincibilityFrames;
		for (float t = 0; t < maxT; t += Time.deltaTime) {
			c.a = Mathf.Lerp(redFlashRange.y, redFlashRange.x, t/maxT);
			redFlashSr.color = c;
			yield return null;
		}

		c.a = redFlashRange.x;
		redFlashSr.color = c;

	}

	IEnumerator RestartLevel() {
		Destroy(gun);
		Destroy(this.GetComponent<Player>());
		yield return new WaitForSeconds(.8f);
		int sceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(sceneIndex);

	}
}

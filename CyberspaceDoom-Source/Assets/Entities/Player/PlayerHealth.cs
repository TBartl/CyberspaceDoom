using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	int health = 3;
	public float invincibilityFrames = 1f;
	bool canGetHit = true;
	public List<MeshRenderer> hearts;

	public SpriteRenderer heartPlane;
	public GameObject gun;
	public player playerMove;

	public Color lowOpacity;
	public Color highOpacity;

	void Update() {
		if (Input.GetKeyDown(KeyCode.H)) {
			int nextScenceIndex = 0;
			SceneManager.LoadScene(nextScenceIndex);
			player.WorldChange();
			player.instance.SetGun();
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Enemy" && canGetHit) {
			health -= 1;
			if (health >= 0) {
				hearts[health].enabled = false;
			}
			StartCoroutine(TakeDamage());

			StartCoroutine(Invincibility());
			if (health == 0) {
				StartCoroutine(Die());
			}
		}
	}

	IEnumerator Invincibility() {
		canGetHit = false;
		yield return new WaitForSeconds(invincibilityFrames);
		canGetHit = true;
	}

	IEnumerator TakeDamage() {
		heartPlane.color = highOpacity;
		for (float t = 0; t < invincibilityFrames; t += Time.deltaTime) {
			heartPlane.color = Color.Lerp(highOpacity, lowOpacity, t / invincibilityFrames);
			yield return null;
		}
		heartPlane.color = lowOpacity;
	}

	IEnumerator Die() {
		playerMove.enabled = false;
		gun.SetActive(false);
		yield return new WaitForSeconds(2);

		int nextScenceIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(nextScenceIndex);
		player.WorldChange();
		player.instance.SetGun();
		hearts[health].enabled = false;

		playerMove.enabled = true;
		gun.SetActive(true);
	}
	
	public void ResetHealth() {
		health = 3;
		for (int i = 0; i <3; i++) {
			hearts[health].enabled = true;
		}
	}
}

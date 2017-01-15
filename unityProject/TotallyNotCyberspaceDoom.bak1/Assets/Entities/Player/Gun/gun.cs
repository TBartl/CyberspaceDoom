using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour {

	public enum modes {
		pistol,
		shotgun,
		lazer
	}

	Animator anim;
	public Transform muzzleOpening;
	public float bulletForce;
	public float timeBetweenShots;
	float tempShotTime;

	public GameObject bullet;
	public GameObject[] bullets;
	public int pooledBullets;
	List<GameObject> bulletList;

	public float shotgunRecoil = .1f;
	public float shotgunReloadTime = 2f;
	bool canShootShotgun = true;
	public Transform chargeBar;

	void Awake() {
		anim = GetComponent<Animator>();
		tempShotTime = timeBetweenShots;
		PoolBullets();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update() {
		tempShotTime -= Time.deltaTime;
		if (Input.GetButton("Fire1") && tempShotTime <= 0) {
			anim.SetTrigger("fire");
			GrabBullet(false);
			tempShotTime = timeBetweenShots;
		}
		if (Input.GetButtonDown("Fire2") && canShootShotgun) {
			anim.SetTrigger("fire");
			//Temporary shotgun lol
			for (int i = 0; i < 20; i++) {
				GrabBullet(true);
			}

			// Should probably do rocket jumping with an explosion when a bullet hits the ground
			// But this is easier for now
			//if (Vector3.Dot(muzzleOpening.forward, muzzleOpening.transform.position.normalized) < -.7f) {
				transform.root.GetComponent<Rigidbody>().velocity += (-muzzleOpening.forward * 20);
			//}

			StartCoroutine(ReloadShotgun());
		}
	}

	void PoolBullets() {
		bulletList = new List<GameObject>();
		for (int i = 0; i < pooledBullets; ++i) {
			GameObject pomf = Instantiate(bullets[0]) as GameObject;
			GameObject desu = Instantiate(bullets[1]) as GameObject;
			GameObject loli = Instantiate(bullets[2]) as GameObject;
			GameObject nani = Instantiate(bullets[3]) as GameObject;
			GameObject onii = Instantiate(bullets[4]) as GameObject;
			GameObject chan = Instantiate(bullets[5]) as GameObject;

			pomf.SetActive(false);
			desu.SetActive(false);
			loli.SetActive(false);
			nani.SetActive(false);
			onii.SetActive(false);
			chan.SetActive(false);

			bulletList.Add(pomf);
			bulletList.Add(desu);
			bulletList.Add(loli);
			bulletList.Add(nani);
			bulletList.Add(onii);
			bulletList.Add(chan);
		}
	}

	void GrabBullet(bool useRecoil) {
		for (int i = 0; i < bulletList.Count; ++i) {
			if (bulletList[i] != null && !bulletList[i].activeInHierarchy) {
				bulletList[i].transform.position = muzzleOpening.position;
				bulletList[i].transform.rotation = muzzleOpening.rotation;
				bulletList[i].GetComponent<deactivator>().timeTillDeactivate = 3;
				bulletList[i].SetActive(true);

				//fire
				Rigidbody fam = bulletList[i].GetComponent<Rigidbody>();
				fam.velocity = Vector3.zero;
				Vector3 direction = muzzleOpening.forward;
				if (useRecoil)
					direction = (direction + Random.insideUnitSphere * shotgunRecoil).normalized;
				fam.AddForce(direction * bulletForce);
				break;
			}
		}
	}

	IEnumerator ReloadShotgun() {
		canShootShotgun = false;
		for (float t = 0; t < shotgunReloadTime; t += Time.deltaTime) {
			chargeBar.localScale = new Vector3(1, 1, t / shotgunReloadTime);
			yield return null;
		}
		chargeBar.localScale = Vector3.one;
		canShootShotgun = true;

	}
}

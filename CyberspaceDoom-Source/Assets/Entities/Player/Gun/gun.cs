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

	public GameObject shotgunBullet;
	public GameObject[] shotgunBullets;
	List<GameObject> shotgunBulletList;

	public Transform magazine; // just an empty gameObject to hold the prefabs

	public GameObject muzzleFlash;


	public float shotgunRecoil = .1f;
	public float shotgunReloadTime = 2f;
	bool canShootShotgun = true;
	public Transform chargeBar;

	void Awake() {
		anim = GetComponent<Animator>();
		tempShotTime = timeBetweenShots;
		Setup();
	}

	public void Setup() {
		PoolBullets();
		PoolShotgunBullets();
	}

	void Update() {
		if (player.started && !playMan.instance.paused) {
			tempShotTime -= Time.deltaTime;
			if (Input.GetButton("Shoot1") && tempShotTime <= 0) {
				anim.SetTrigger("fire");
				GrabBullet();
				StartCoroutine(FlashMuzzle());
				AkSoundEngine.PostEvent("Pistol", gameObject);
				tempShotTime = timeBetweenShots;
			}

			if (Input.GetButtonDown("Shoot2") && canShootShotgun) {
				anim.SetTrigger("fire");
				//Temporary shotgun lol
				for (int i = 0; i < 20; i++) {
					GrabSlug();
				}

				StartCoroutine(FlashMuzzle());

				// Should probably do rocket jumping with an explosion when a bullet hits the ground
				// But this is easier for now
				//if (Vector3.Dot(muzzleOpening.forward, muzzleOpening.transform.position.normalized) < -.7f) {
				transform.root.GetComponent<Rigidbody>().velocity += (-muzzleOpening.forward * 20);
				//}

				AkSoundEngine.PostEvent("Shotgun", gameObject);

				StartCoroutine(ReloadShotgun());
			}
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

			pomf.transform.SetParent(magazine);
			desu.transform.SetParent(magazine);
			loli.transform.SetParent(magazine);
			nani.transform.SetParent(magazine);
			onii.transform.SetParent(magazine);
			chan.transform.SetParent(magazine);

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

	void PoolShotgunBullets() {
		shotgunBulletList = new List<GameObject>();
		for (int i = 0; i < pooledBullets; ++i) {
			GameObject its = Instantiate(shotgunBullets[0]) as GameObject;
			GameObject lit = Instantiate(shotgunBullets[1]) as GameObject;
			GameObject fam = Instantiate(shotgunBullets[2]) as GameObject;

			its.transform.SetParent(magazine);
			lit.transform.SetParent(magazine);
			fam.transform.SetParent(magazine);

			its.SetActive(false);
			lit.SetActive(false);
			fam.SetActive(false);

			shotgunBulletList.Add(its);
			shotgunBulletList.Add(lit);
			shotgunBulletList.Add(fam);
		}
	}

	void GrabBullet() {
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
				fam.AddForce(direction * bulletForce);
				break;
			}
		}
	}

	void GrabSlug() {
		for (int i = 0; i < shotgunBulletList.Count; ++i) {
			if (shotgunBulletList[i] != null && !shotgunBulletList[i].activeInHierarchy) {
				shotgunBulletList[i].transform.position = muzzleOpening.position;
				shotgunBulletList[i].transform.rotation = muzzleOpening.rotation;
				shotgunBulletList[i].GetComponent<deactivator>().timeTillDeactivate = 1.5f;
				shotgunBulletList[i].SetActive(true);

				//fire
				Rigidbody fam = shotgunBulletList[i].GetComponent<Rigidbody>();
				fam.velocity = Vector3.zero;
				Vector3 direction = muzzleOpening.forward;
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


	IEnumerator FlashMuzzle() {
		muzzleFlash.SetActive(true);
		yield return new WaitForSecondsRealtime(0.1f);
		muzzleFlash.SetActive(false);
	}
}
